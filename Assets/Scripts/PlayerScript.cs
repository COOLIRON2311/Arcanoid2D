using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    SpriteRenderer bg;
    const int maxLevel = 30;
    [Range(1, maxLevel)]
    public int level = 1;
    public float ballVelocityMul = 0.02f;
    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject yellowPrefab;
    public GameObject ballPrefab;
    public AudioClip pointSound;
    public GameDataScript gameData;
    int balls;
    int blocks;

    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contactFilter = new ContactFilter2D();
    static bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        if (!gameStarted)
        {
            gameStarted = true;
            if (gameData.resetOnStart)
                gameData.Reset();
        }
        bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        level = gameData.level;
        SetMusic();
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if (mouse_pos.x < -7.1 || 7.1 < mouse_pos.x)
        //     return;
        var pos = transform.position;
        pos.x = mouse_pos.x;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.M))
        {
            gameData.bgm = !gameData.bgm;
            SetMusic();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gameData.sfx = ! gameData.sfx;
        }

        // reset balls if they are stuck
        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (var b in GameObject.FindGameObjectsWithTag("Ball"))
            {
                Destroy(b);
            }
            ResetBalls();
        }
    }

    void CreateBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;

        for (int i = 0; i < count; i++)
        {
            bool found = false;
            for (int k = 0; k < 20; k++)
            {
                var obj = Instantiate(prefab, new Vector3((Random.value * 2 - 1) * xMax, Random.value * yMax, 0), Quaternion.identity);
                if (obj.GetComponent<Collider2D>().OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
                {
                    found = true;
                    break;
                }
                Destroy(obj);
            }
            if (found)
                blocks++;

        }
    }

    IEnumerator BallDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (balls == 0)
        {
            if (gameData.balls > 0)
                CreateBalls();
            else
            {
                gameData.Reset();
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    IEnumerator BlockDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (blocks == 0)
        {
            // print("Level complete!");
            if (level < maxLevel)
                gameData.level++;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void BallDestroyed()
    {
        balls--;
        gameData.balls--;
        StartCoroutine(BallDestroyedCoroutine());
    }

    public void BlockDestroyed(int points)
    {
        blocks--;
        gameData.points += points;
        if (gameData.sfx)
            SoundMaster.instance.sfx.PlayOneShot(pointSound);
        StartCoroutine(BlockDestroyedCoroutine());
    }

    void CreateBalls()
    {
        int count = 2;
        if (gameData.balls == 1)
            count = 1;
        balls = count;
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + level * ballVelocityMul;
        }
    }

    public void ResetBalls()
    {
        for (int i = 0; i < balls; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + level * ballVelocityMul;
        }
    }

    void SetBackGround()
    {
        bg.sprite = Resources.Load(level.ToString("d2"), typeof(Sprite)) as Sprite;
    }

    void SetMusic()
    {
        if (gameData.bgm)
            SoundMaster.instance.bgm.Play();
        else
            SoundMaster.instance.bgm.Stop();
    }

    void StartLevel()
    {
        blocks = 0;
        SetBackGround();
        var yMax = Camera.main.orthographicSize * 0.8f;
        var xMax = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;
        CreateBlocks(bluePrefab, xMax, yMax, level, 8);
        CreateBlocks(redPrefab, xMax, yMax, 1 + level, 10);
        CreateBlocks(greenPrefab, xMax, yMax, 1 + level, 12);
        CreateBlocks(yellowPrefab, xMax, yMax, 2 + level, 15);
        CreateBalls();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5, 4, Screen.width - 10, 100),
            string.Format(
                "<color=yellow><size=30>Level <b>{0}</b>  Balls <b>{1}</b>" +
                "  Score <b>{2}</b></size></color>",
                gameData.level, gameData.balls, gameData.points
            )
        );
    }
}

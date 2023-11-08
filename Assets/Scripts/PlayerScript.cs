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
    public GameObject extraBluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject yellowPrefab;
    public GameObject ballPrefab;
	public GameObject menu;
	public AudioClip pointSound;
    GameDataScript gameData;
    int balls;
    int blocks;

    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contactFilter = new ContactFilter2D();
    static bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        gameData = GameDataObject.instance.GameData;
        if (!gameStarted)
        {
            gameStarted = true;
            if (gameData.restoreOnStart)
                gameData.Load();
        }
        bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        level = gameData.level;
        SetMusic();
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // if (mouse_pos.x < -7.1 || 7.1 < mouse_pos.x)
            //     return;
            var pos = transform.position;
            pos.x = mouse_pos.x;
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            gameData.bgm = !gameData.bgm;
            SetMusic();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            gameData.sfx = !gameData.sfx;
        }

        // reset balls if they are stuck
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (var b in GameObject.FindGameObjectsWithTag("Ball"))
            {
                Destroy(b);
            }
            ResetBalls();
        }

        // pause
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale > 0)
            { // pause game
				menu.SetActive(true);
				Time.timeScale = 0;
				Cursor.visible = true;
				SoundMaster.instance.bgm.Pause();
            }
            else
            { // resume game
				menu.SetActive(false);
				Cursor.visible = false;
				Time.timeScale = 1;
                SoundMaster.instance.bgm.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            gameData.Reset();
            SceneManager.LoadScene("MainScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    int CreateBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        int blocks_created = 0;
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
                blocks_created++;
        }
        return blocks_created;
    }

    void CreateExtraBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;
        for (int i = 0; i < count; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var obj = Instantiate(prefab, new Vector3((Random.value * 2 - 1) * xMax, Random.value * yMax, 0), Quaternion.identity);
                var s = obj.GetComponent<ExtraBlockScript>();

                if (obj.GetComponent<Collider2D>().OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
                {
                    var (x1, y1, x2, y2) = (Random.value, Random.value, Random.value, Random.value);
                    s.p1 = new Vector2(-x1 * xMax, (y1 - 0.5f) * yMax);
                    s.p2 = new Vector2(x2 * xMax, (y2 - 0.5f) * yMax);
                    break;
                }
                Destroy(obj);
            }
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

    IEnumerator BlockDestroyedCoroutine2()
    {
        const int times = 5;
        for (int i = 0; i < times; i++)
        {
            yield return new WaitForSeconds(0.2f);
            SoundMaster.instance.sfx.PlayOneShot(pointSound);
        }
    }

    public void BallDestroyed()
    {
        balls--;
        gameData.balls--;
        StartCoroutine(BallDestroyedCoroutine());
    }

    int requiredPointsToBall
    {
        get { return 400 + (level - 1) * 20; }
    }

    public void BlockDestroyed(int points, bool registerDestroyed)
    {
        if (registerDestroyed)
            blocks--;
        gameData.points += points;
        gameData.pointsToBall += points;
        if (gameData.pointsToBall >= requiredPointsToBall)
        {
            gameData.balls++;
            gameData.pointsToBall -= requiredPointsToBall;
            if (gameData.sfx)
                StartCoroutine(BlockDestroyedCoroutine2());
        }
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
        CreateBlocks(bluePrefab, xMax, yMax, level / 2, 8);
        CreateExtraBlocks(extraBluePrefab, xMax, yMax, level / 2, 8);
        blocks += CreateBlocks(redPrefab, xMax, yMax, 1 + level, 10);
        blocks += CreateBlocks(greenPrefab, xMax, yMax, 1 + level, 12);
        blocks += CreateBlocks(yellowPrefab, xMax, yMax, 2 + level, 15);
        CreateBalls();
    }

    string OnOff(bool boolVal)
    {
        return boolVal ? "on" : "off";
    }

    void OnGUI()
    {
        if (Time.timeScale > 0)
        {
            GUI.Label(new Rect(5, 4, Screen.width - 10, 100),
            string.Format(
                "<color=yellow><size=30>Level <b>{0}</b>  Balls <b>{1}</b>" +
                "  Score <b>{2}</b></size></color>",
                gameData.level, gameData.balls, gameData.points
                )
            );

            GUIStyle style = new GUIStyle { alignment = TextAnchor.UpperRight };
            GUI.Label(new Rect(5, 14, Screen.width - 10, 100), string.Format(
              "<color=yellow><size=20><color=white>Space</color>-pause {0}" +
              " <color=white>N</color>-new" +
              " <color=white>J</color>-jump" +
              " <color=white>M</color>-music {1}" +
              " <color=white>S</color>-sound {2}" +
              " <color=white>Esc</color>-exit</size></color>",
              OnOff(Time.timeScale > 0),
              OnOff(!gameData.bgm),
              OnOff(!gameData.sfx)
            ), style);
        }
        else
        {
            GUI.Label(new Rect(5, 4, Screen.width - 10, 100),
                "<color=yellow><size=30>Game <b>Paused</b></size></color>"
            );
        }

    }

    void OnApplicationQuit()
    {
        gameData.Save();
    }
}

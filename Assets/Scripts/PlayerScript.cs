using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    SpriteRenderer bg;
    const int maxLevel = 30;
    [Range(1, maxLevel)]
    public int level = 1;
    [Range(1, 10)]
    public int InitialBalls = 2;
    public float ballVelocityMul = 0.02f;
    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject yellowPrefab;
    public GameObject ballPrefab;
    int balls;

    static Collider2D[] colliders = new Collider2D[50];
    static ContactFilter2D contactFilter = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        bg = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        balls = InitialBalls;
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
    }

    void CreateBlocks(GameObject prefab, float xMax, float yMax, int count, int maxCount)
    {
        if (count > maxCount)
            count = maxCount;

        for (int i = 0; i < count; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var obj = Instantiate(prefab, new Vector3((Random.value * 2 - 1) * xMax, Random.value * yMax, 0), Quaternion.identity);
                if (obj.GetComponent<Collider2D>().OverlapCollider(contactFilter.NoFilter(), colliders) == 0)
                    break;
                Destroy(obj);
            }
        }
    }

    IEnumerator BallDestroyedCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        balls--;
        if (balls == 0)
            CreateBalls();
    }
    public void BallDestroyed()
    {
        StartCoroutine(BallDestroyedCoroutine());
    }

    void CreateBalls()
    {
        balls = InitialBalls;
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

    void StartLevel()
    {
        SetBackGround();
        var yMax = Camera.main.orthographicSize * 0.8f;
        var xMax = Camera.main.orthographicSize * Camera.main.aspect * 0.85f;
        CreateBlocks(bluePrefab, xMax, yMax, level, 8);
        CreateBlocks(redPrefab, xMax, yMax, 1 + level, 10);
        CreateBlocks(greenPrefab, xMax, yMax, 1 + level, 12);
        CreateBlocks(yellowPrefab, xMax, yMax, 2 + level, 15);
        CreateBalls();
    }
}

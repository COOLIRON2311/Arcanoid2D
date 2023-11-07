using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBase : MonoBehaviour
{
    public float bonusSpeed = 2f;
    Rigidbody2D rb2d;
    GameDataScript gameData;
    protected PlayerScript playerScript;
    public GameObject ballPrefab;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameData = GameDataObject.instance.GameData;
        StartCoroutine(SmoothMovementDown());
    }

    public void SetPlayerScript(PlayerScript playerScript)
    {
        this.playerScript = playerScript;
    }

    IEnumerator SmoothMovementDown()
    {
        while (true)
        {
            var curr = rb2d.position;
            curr.y -= bonusSpeed * Time.deltaTime;
            rb2d.position = curr;
            yield return null;
        }
    }

    protected virtual void BonusActivate()
    {
        gameData.points += 100;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BonusActivate();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallScript : MonoBehaviour
{
    public Vector2 ballInitialForce;
    GameObject playerObj;
    PlayerScript ps;
    Rigidbody2D rb;
    float dx; // delta x
    public AudioClip hitSound;
    public AudioClip loseSound;
    GameDataScript gameData;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        ps = playerObj.GetComponent<PlayerScript>();
        dx = transform.position.x;
        gameData = GameDataObject.instance.GameData;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic)
        {
            if (Input.GetButton("Fire1"))
            {
                rb.isKinematic = false;
                rb.AddForce(ballInitialForce);
            }
            else
            {
                var pos = transform.position;
                pos.x = playerObj.transform.position.x + dx;
                transform.position = pos;
            }
        } // change ball trajectory if it became stable
        else if (Input.GetKeyDown(KeyCode.J))
        {
            var v = rb.velocity;
            if (Random.Range(0, 2) == 0)
                v.Set(v.x - 0.1f, v.y + 0.1f);
            else
                v.Set(v.x + 0.1f, v.y - 0.1f);
            rb.velocity = v;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameData.sfx)
		{
			SoundMaster.instance.sfx.volume = gameData.sfxdValue;
			SoundMaster.instance.sfx.PlayOneShot(loseSound);
		}
        ps.RemoveBallFromList(gameObject);
        Destroy(gameObject);
        ps.BallDestroyed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameData.sfx)
		{
			SoundMaster.instance.sfx.volume = gameData.sfxdValue;
			SoundMaster.instance.sfx.PlayOneShot(hitSound);
		}
    }
}

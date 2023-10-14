using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallScript : MonoBehaviour
{
    public Vector2 ballInitialForce;
    GameObject playerObj;
    PlayerScript ps;
    Rigidbody2D rb;
    float dx; // delta x
    AudioSource audioSrc;
    public AudioClip hitSound;
    public AudioClip loseSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        ps = playerObj.GetComponent<PlayerScript>();
        dx = transform.position.x;
        audioSrc = Camera.main.GetComponent<AudioSource>();
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
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        audioSrc.PlayOneShot(loseSound);
        Destroy(gameObject);
        ps.BallDestroyed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSrc.PlayOneShot(hitSound);
    }
}

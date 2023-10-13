using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallScript : MonoBehaviour
{
    public Vector2 ballInitialForce;
    public GameObject playerObj;
    public Rigidbody2D rb;
    float dx; // delta x
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        // playerObj = GameObject.FindGameObjectWithTag("Player");
        dx = transform.position.x;
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
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public GameObject textObj;
    public bool registerDestroyed = true;
    Text textComp;
    public int hitsToDestroy;
    public int points;
    PlayerScript ps;
    // Start is called before the first frame update
    void Start()
    {
        if (textObj != null)
        {
            textComp = textObj.GetComponent<Text>();
            textComp.text = hitsToDestroy.ToString();
        }
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitsToDestroy--;
        if (hitsToDestroy == 0)
        {
            // print(points);
            Destroy(gameObject);
            ps.BlockDestroyed(points, registerDestroyed);
        }
        else if (textComp != null)
            textComp.text = hitsToDestroy.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public GameObject textObj;
    public Text textComp;
    public int hitsToDestroy;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        if (textObj != null)
        {
            // textComp = textObj.GetComponent<Text>();
            textComp.text = hitsToDestroy.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitsToDestroy--;
        if (hitsToDestroy == 0)
        {
            // print(points);
            Destroy(gameObject);
        }
        else if (textComp != null)
            textComp.text = hitsToDestroy.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

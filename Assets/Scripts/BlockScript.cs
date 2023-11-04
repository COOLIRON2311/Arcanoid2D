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
    public GameObject[] bonuses;
    // Start is called before the first frame update
    protected virtual void Start()
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
            ps.BlockDestroyed(points, registerDestroyed);
            Destroy(gameObject);
            if (bonuses.Length > 0)
            {
                var bonus = bonuses[Random.Range(0, bonuses.Length - 1)];
                var obj = Instantiate(bonus, gameObject.transform.position, Quaternion.identity);
                obj.GetComponent<BonusBase>().SetPlayerScript(ps);
            }
        }
        else if (textComp != null)
            textComp.text = hitsToDestroy.ToString();
    }
}

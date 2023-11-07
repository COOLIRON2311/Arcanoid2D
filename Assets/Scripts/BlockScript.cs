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

    public GameObject fastBonus;
    public GameObject slowBonus;
    public GameObject ballBonus;
    public GameObject plusTwoBonus;
    public GameObject plusTenBonus;
    List<GameObject> bonuses = new List<GameObject>();

    public int fastBonusP;
    public int slowBonusP;
    public int ballBonusP;
    public int plusTwoBonusP;
    public int plusTenBonusP;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (textObj != null)
        {
            textComp = textObj.GetComponent<Text>();
            textComp.text = hitsToDestroy.ToString();
        }
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        for (int i = 0; i < fastBonusP; i++)
            bonuses.Add(fastBonus);
        for (int i = 0; i < slowBonusP; i++)
            bonuses.Add(slowBonus);
        for (int i = 0; i < ballBonusP; i++)
            bonuses.Add(ballBonus);
        for (int i = 0; i < plusTwoBonusP; i++)
            bonuses.Add(plusTwoBonus);
        for (int i = 0; i < plusTenBonusP; i++)
            bonuses.Add(plusTenBonus);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hitsToDestroy--;
        if (hitsToDestroy == 0)
        {
            // print(points);
            ps.BlockDestroyed(points, registerDestroyed);
            Destroy(gameObject);
            if (bonuses.Count > 0)
            {
                var bonus = bonuses[Random.Range(0, bonuses.Count)];
                var obj = Instantiate(bonus, gameObject.transform.position, Quaternion.identity);
                obj.GetComponent<BonusBase>().SetPlayerScript(ps);
            }
        }
        else if (textComp != null)
            textComp.text = hitsToDestroy.ToString();
    }
}

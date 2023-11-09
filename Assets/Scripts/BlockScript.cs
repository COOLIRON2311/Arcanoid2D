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

    public static int pointsBonusP = 0;
    public static int fastBonusP = 0;
    public static int slowBonusP = 0;
    public static int ballBonusP = 0;
    public static int plusTwoBonusP = 0;
    public static int plusTenBonusP = 0;
    public GameObject pointsBonus;
    public GameObject fastBonus;
    public GameObject slowBonus;
    public GameObject ballBonus;
    public GameObject plusTwoBonus;
    public GameObject plusTenBonus;
    List<GameObject> bonuses = new List<GameObject>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (textObj != null)
        {
            textComp = textObj.GetComponent<Text>();
            textComp.text = hitsToDestroy.ToString();
        }
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        for (int i = 0; i < pointsBonusP; i++)
            if (pointsBonus != null)
                bonuses.Add(pointsBonus);
        for (int i = 0; i < fastBonusP; i++)
            if (fastBonus != null)
                bonuses.Add(fastBonus);
        for (int i = 0; i < slowBonusP; i++)
            if (slowBonus != null)
                bonuses.Add(slowBonus);
        for (int i = 0; i < ballBonusP; i++)
            if (ballBonus != null)
                bonuses.Add(ballBonus);
        for (int i = 0; i < plusTwoBonusP; i++)
            if (plusTwoBonus != null)
                bonuses.Add(plusTwoBonus);
        for (int i = 0; i < plusTenBonusP; i++)
            if (plusTenBonus != null)
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

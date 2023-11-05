using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBonusScript : BonusBase
{
    protected override void BonusActivate()
    {
        foreach (var ballObject in playerScript.currentBalls)
        {
            var rb2d = ballObject.GetComponent<Rigidbody2D>();
            rb2d.velocity *= 0.9f;
        }
    }
}

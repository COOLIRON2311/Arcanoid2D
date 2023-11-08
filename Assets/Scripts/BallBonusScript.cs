using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBonusScript : BonusBase
{
    protected override void BonusActivate()
    {
        //playerScript.balls++;
        playerScript.gameData.balls++;
    }
}

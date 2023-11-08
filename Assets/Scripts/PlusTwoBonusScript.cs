using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusTwoBonusScript : BonusBase
{
    protected override void BonusActivate()
    {
        playerScript.balls += 2;
        playerScript.gameData.balls += 2;
        for (int i = 0; i < 2; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + playerScript.level * playerScript.ballVelocityMul;
            playerScript.currentBalls.Add(obj);
        }
    }
}

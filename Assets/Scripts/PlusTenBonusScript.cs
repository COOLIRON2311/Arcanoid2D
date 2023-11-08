using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusTenBonusScript : BonusBase
{
    protected override void BonusActivate()
    {
        playerScript.balls += 10;
        playerScript.gameData.balls += 10;
        for (int i = 0; i < 10; i++)
        {
            var obj = Instantiate(ballPrefab);
            var ball = obj.GetComponent<BallScript>();
            ball.ballInitialForce += new Vector2(10 * i, 0);
            ball.ballInitialForce *= 1 + playerScript.level * playerScript.ballVelocityMul;
            playerScript.currentBalls.Add(obj);
        }
    }
}

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
            ball.ballInitialForce += new Vector2(Random.Range(0, 10), Random.Range(0, 10));
            ball.ballInitialForce *= 1 + playerScript.level * playerScript.ballVelocityMul;

            var pos = ball.transform.position;
            pos.x = Random.Range(20f, Screen.width - 20);
            pos.y = Random.Range(20f, Screen.height - 50);
            pos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, pos.z));
            pos.z = 0;
            ball.transform.position = pos;

            var rb = ball.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(ball.ballInitialForce);

            playerScript.currentBalls.Add(obj);
        }
    }
}

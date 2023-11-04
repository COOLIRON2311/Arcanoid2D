using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBlockScript : BlockScript
{
    public Vector2 p1;
    public Vector2 p2;
    bool direction;
    float speed;
    public Vector2 Position
    {
        get => (Vector2)transform.position;
        set => transform.position = value;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (Random.value > 0.5)
        {
            Position = p1;
            direction = true;
        }
        else
        {
            Position = p2;
            direction = false;
        }
        speed = Random.value + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (Position == p1 || Position == p2)
            direction = !direction;
        if (direction) // to p1
            Position = Vector2.MoveTowards(Position, p1, step);
        else // to p2
            Position = Vector2.MoveTowards(Position, p2, step);
    }
}

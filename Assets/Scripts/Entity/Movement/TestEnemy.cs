using UnityEngine;
using System.Collections;

public class TestEnemy : Enemy {
    Vector2 dir;

    public override void Move()
    {
        rb.AddForce(dir*stats.moveSpeed);
    }
    public override void Init()
    {
        base.Init();
        if (Random.Range(0f, 1f) > .5f)
        {
            dir = Vector2.right;

        }
        else
        {
            dir = Vector2.left;
        }
    }

    public override void Update ()
    {
        base.Update();

        Move();
    }


}

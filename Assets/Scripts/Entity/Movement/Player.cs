using UnityEngine;
using System.Collections;

public class Player : Entity {

    public string horizontalInput;

    public static Player player;

    [Range(0.1f,0.5f)]
    public float smoothingAmount;

    public void Awake()
    {
        base.Init();
         
        if (player == null)
            player = this;
        else
        {
            if (player != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public override void Move()
    {
        if (stunLocked)
            return;

        Vector2 newVel = new Vector2(Input.GetAxisRaw(horizontalInput)*stats.moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.Lerp(rb.velocity, newVel,smoothingAmount);
    }

    public override void Update()
    {
        base.Update();
        Move();
    }
}

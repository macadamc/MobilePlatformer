using UnityEngine;
using System.Collections;

public class Player : Entity {

    public string horizontalInput;
    public string verticalInput;

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
        //must use CnControls.CnInputManager for cross platform input. (same for keyboard and touch controls) 
        Vector2 newVel = new Vector2(CnControls.CnInputManager.GetAxisRaw(horizontalInput)*stats.moveSpeed, rb.velocity.y);

        rb.velocity = Vector2.Lerp(rb.velocity, newVel,smoothingAmount);
    }

    public override void Update()
    {
        base.Update();
        Move();

        if(CnControls.CnInputManager.GetButtonDown("Jump"))
        {
            base.Jump();
        }

        if(CnControls.CnInputManager.GetButtonUp("Jump"))
        {
            base.JumpCancel();
        }

    }
}

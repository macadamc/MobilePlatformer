using UnityEngine;
using System.Collections;

public class Player : Entity {

    public string horizontalInput;
    public string verticalInput;

    public static Player player;

    [Range(0.1f,0.5f)]
    public float smoothingAmount;

    public bool doubleJump;

    public override void Init()
    {
        base.Init();
         
        //makes the player a 'singleton'
        //
        //only one instance will ever be there at a time.
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
        if (stunLocked || GameManager.GM.gamePaused)
            return;

        //must use 'CnControls.CnInputManager' for cross platform input. (same for keyboard and touch controls) 
        Vector2 newVel = new Vector2(CnControls.CnInputManager.GetAxisRaw(horizontalInput)*stats.moveSpeed, rb.velocity.y);

        //lerp the velocity so movement feels smoother.
        //could set direcly to newVel for a 'snappier' feel.
        rb.velocity = Vector2.Lerp(rb.velocity, newVel,smoothingAmount);
    }

    public override void Update()
    {
        base.Update();

        if (stunLocked || GameManager.GM.gamePaused)
            return;

        Move();
        if(CnControls.CnInputManager.GetAxis("Horizontal") > 0)
        {
            rend.flipX = false;
        }
        if (CnControls.CnInputManager.GetAxis("Horizontal") < 0)
        {
            rend.flipX = true;
        }

        //Jump code
        //
        //Cancels jump when jump button is released in mid-air. Allowing for variable jump heights;
        if (CnControls.CnInputManager.GetButtonUp("Jump"))
        {
            base.JumpCancel();
        }

        if (onGround)
        {
            //basic jump
            if (CnControls.CnInputManager.GetButtonDown("Jump"))
            {
                base.Jump();
            }
        }
        else
        {
            if (CnControls.CnInputManager.GetButtonDown("Jump"))
            {
                if (doubleJump)
                {
                    base.Jump();
                    doubleJump = false;
                }
            }
        }

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (stunLocked || GameManager.GM.gamePaused)
            return;

        if (onGround)
            doubleJump = true;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Entity : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer rend;

    int hp;
    public Stats stats;

    float stunLockTimer;
    public bool stunLocked;

    [HideInInspector]
    public bool onGround;
    [HideInInspector]
    public bool midGround;
    [HideInInspector]
    public bool leftGround;
    [HideInInspector]
    public bool rightGround;
    [HideInInspector]
    public bool leftWall;
    [HideInInspector]
    public bool rightWall;

    Vector2 oldVel;
    float oldGrav;
    bool paused;

    public LayerMask obsLayer;

    public virtual void Update()
    {
        if(GameManager.GM.gamePaused)
        {
            if(!paused)
            {
                oldVel = rb.velocity;
                oldGrav = rb.gravityScale;
                rb.gravityScale = 0f;
                paused = true;
            }
            rb.velocity = Vector2.zero;

            return;
        }
        else
        {
            if (paused)
            {
                rb.velocity = oldVel;
                rb.gravityScale = oldGrav;
                paused = false;
            }
        }

        if (stunLockTimer > 0)
        {
            stunLockTimer -= Time.deltaTime;
            stunLocked = true;
        }
        else
        {
            stunLocked = false;
        }

    }

    public virtual void Move () {}

    public virtual void Jump()
    {
        Vector2 newVel = new Vector2(rb.velocity.x, stats.jumpStr);
        rb.velocity = newVel;
    }

    public virtual void JumpCancel()
    {
        Vector2 newVel = rb.velocity;
        if(newVel.y > 0)
        {
            newVel.y /= 2;   
        }
        rb.velocity = newVel;
    }

    public virtual void Destroy () {}

    public virtual void Init ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    public virtual void StunLock(float stunTimeToAdd)
    {
        stunLockTimer += stunTimeToAdd;
    }

    public virtual void CheckForObs()
    {

        Vector3 leftWallVector = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);
        Vector3 rightWallVector = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);

        leftWall = Physics2D.Raycast(transform.position, Vector2.left, 0.55f, obsLayer);
        rightWall = Physics2D.Raycast(transform.position, Vector2.right, 0.55f, obsLayer);
        midGround = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, obsLayer);
        leftGround = Physics2D.Raycast(leftWallVector, Vector2.down, 0.55f, obsLayer);
        rightGround = Physics2D.Raycast(rightWallVector, Vector2.down, 0.55f, obsLayer);

        if (midGround || rightGround || leftGround)
            onGround = true;
        else
            onGround = false;
    }

    public virtual void FixedUpdate()
    {
        CheckForObs();
    }

    void Awake()
    {
        Init();
    }
}

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

    public virtual void Update()
    {
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

    public virtual void Destroy () {}

    public virtual void Init ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public virtual void StunLock(float stunTimeToAdd)
    {
        stunLockTimer += stunTimeToAdd;
    }

    void Awake()
    {
        Init();
    }
}

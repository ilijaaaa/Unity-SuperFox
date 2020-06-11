using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHight = 15f;
    [SerializeField] private LayerMask ground;

    protected Rigidbody2D rb;
    private Collider2D coll;

    private bool facingLeft = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    { 
        if (anim.GetBool("Jumping")) //regulise skok zabe
        {
            if (rb.velocity.y < 1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling")) //regulise pad zabe
        {
            anim.SetBool("Falling", false);
        }    
    }

    private void Move()  // regulise nacin kretanja zabe
    {
        if (facingLeft)
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}

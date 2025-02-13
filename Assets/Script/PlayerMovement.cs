using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField]float speed = 1f;
    [SerializeField]float jumpHeight = 3f;
    float direction = 0;
    bool isGrounded = false;
    //bool isFacingRight = true;

    bool canDash = true; 
    bool isDashing = false;
    float dashingPower = 15f;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;

    //Animator anim; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            Move(direction);
        }

        // if ((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
        // {
        //     Flip();
        // }
    }

    void OnMove(InputValue value)
    {
        float v = value.Get<float>();
        direction = v;
    }

    void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        //anim.SetBool("isRunning", dir != 0);

    }

    void OnJump()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (Vector2.Angle(collision.GetContact(i).normal,Vector2.up) < 45f)
                {
                    isGrounded = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(direction * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void OnDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    // private void Flip()
    // {
    //     isFacingRight = !isFacingRight;
    //     Vector3 newLocalScale = transform.localScale;
    //     newLocalScale.x *= -1f;
    //     transform.localScale = newLocalScale;
    // }
}


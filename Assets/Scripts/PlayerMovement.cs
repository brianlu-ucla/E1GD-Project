using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Input = UnityEngine.Windows.Input;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float jumpTime = 0.5f;
    [SerializeField] float dashingPower = 15f;
    float direction = 0;
    bool isGrounded = false;
    bool isFacingRight = true;
    bool canDash = true; 
    bool isDashing = false;
    float dashingTime = 0.2f;
    float dashingCooldown = 1f;
    float jumpTimer = 0f;
    bool isJumping = false;
    int jumpCount = 0;
    Animator anim; 

    // New field for storing the original speed value
    private float baseSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        baseSpeed = speed;  // store the initial speed
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            rb.linearVelocity = Vector2.up * jumpHeight * (jumpTimer / jumpTime);
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                isJumping = false;
                rb.linearVelocity = Vector2.down * jumpHeight * (jumpTimer / jumpTime);
            }
        }
        else if (!isGrounded)
        {
            isJumping = false;
        }

        if (!isDashing)
        {
            Move(direction);
        }

        if ((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
        {
            Flip();
        }
        
        if (rb.linearVelocity.y > 0.001)
        {
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", true);
        }
        else if (rb.linearVelocity.y < -0.001)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        else 
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
    }

    void OnMove(InputValue value)
    {
        float v = value.Get<float>();
        direction = v;
    }

    void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        anim.SetBool("isRunning", dir != 0);
    }

    void OnJump(InputValue value)
    {
        bool isPressed = value.isPressed;
        if (isGrounded && isPressed)
        {
            jumpCount++;
            Debug.Log("# of Jumps: " + jumpCount);
            isJumping = true;
            jumpTimer = jumpTime;
        }
        else if (isJumping)
        {
            isJumping = false;
        }
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;
    }

    public void BoostSpeed(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        speed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
    }
}


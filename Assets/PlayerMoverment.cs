using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private float dirX;
    private bool isGrounded;
    private int jumps;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private int maxJumps = 2;

    private enum MovementState { idle, isPlayerRun, isPlayerJump, isDoubleJump }
    private MovementState state = MovementState.idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || jumps < maxJumps)
            {
                Jump();
            }
        }

        UpdateAnimationState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
            jumps = 0;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        jumps++;
    }

    private void UpdateAnimationState()
    {
        MovementState  state ;

        if (dirX > 0f || dirX < 0f)
        {
            state = MovementState.isPlayerRun;
            sprite.flipX = dirX < 0f; // Flip sprite when moving left
        }
        else
        {
            state = MovementState.idle;
        }

        if (!isGrounded)
        {
            if (rb.velocity.y > 0.1f)
            {
                state = MovementState.isPlayerJump;
                if (jumps > 1 && rb.velocity.y > 0.2f)
                {
                    state = MovementState.isDoubleJump;
                }
            }
            else if (rb.velocity.y < -0.1f)
            {
                state = MovementState.idle;
            }
            
        }

        animator.SetInteger("state", (int)state);
    }
}

public class PlantBullet : MonoBehaviour
{
    public float bulletSpeed = 5f;

    private Rigidbody2D rb;
    private bool movingRight = true; // Theo dõi hướng di chuyển

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Di chuyển theo hướng hiện tại
        if (movingRight)
        {
            rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-bulletSpeed, rb.velocity.y);
        }
    }

    // Xử lý khi đối tượng va chạm với đối tượng khác
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Đảo ngược hướng di chuyển
            FlipDirection();
        }
    }

    // Hàm để đảo ngược hướng di chuyển
    private void FlipDirection()
    {
        movingRight = !movingRight;

        // Đảo ngược hình ảnh của đối tượng (nếu cần)
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}


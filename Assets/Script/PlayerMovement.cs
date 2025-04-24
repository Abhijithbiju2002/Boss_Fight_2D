using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public float jumpForce = 15f;
    private float moveX;
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        //player movemnt aniamtion
        if (Mathf.Abs(moveX) > 0.01f)
        {
            animator.SetBool("isRunning", true);

        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Idel");
        }
        //JUMP PLAYER
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);

        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.05f);

        }
        animator.SetBool("isJumping", !IsGrounded());
        PlayerFlip();

    }
    void PlayerFlip()
    {
        if (moveX > 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveX < -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    //private void OnDrawGizmosSelected()
    //{
    //    if (groundCheck != null)
    //   {
    //       Gizmos.color = Color.green;
    //      Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    //  }
    // }
}

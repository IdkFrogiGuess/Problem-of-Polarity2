using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float MoveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    public bool InvertMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Read input and invert if requested
        MoveInput = Input.GetAxis("Horizontal");
        if (InvertMovement)
        {
            MoveInput = -MoveInput;
        }

        // Apply horizontal movement each physics update
        rb.linearVelocity = new Vector2(MoveInput * speed, rb.linearVelocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (MoveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (MoveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (feetPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}

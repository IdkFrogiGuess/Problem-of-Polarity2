using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public float MoveInput;

    public Animator animator;
    private bool isPushing;
    private bool isGrounded;
    public Transform feetPos;
    public Transform Hand;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    private AudioSource SFX;
    public AudioClip jump;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool IsJumping;

    public bool InvertMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SFX = GetComponent<AudioSource>();
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



        if (isGrounded && rb.linearVelocityY <= 0f)
        {
            animator.SetBool("Jumping", false);
        }
        

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);


        isPushing = Physics2D.OverlapCircle(Hand.position, checkRadius, whatIsWall);

        if (MoveInput > 0)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsPushing", false);
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (isPushing && rb.linearVelocityX >= 0)
            {
                animator.SetBool("IsPushing", true);
            }
            
            
        }
        else if (MoveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsPushing", false);
            if (isPushing && rb.linearVelocityX <= 0)
            {
                animator.SetBool("IsPushing", true);
            }
        }
        if (MoveInput == 0)
        {
            animator.SetBool("IsRunning", false);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            IsJumping = true;
            jumpTimeCounter = jumpTime;
            rb.linearVelocity = Vector2.up * jumpForce;

            //Play Jump Sound Effect
            SFX.PlayOneShot(jump);


            if (Input.GetKey(KeyCode.Space) && IsJumping)
            {
                if (jumpTimeCounter > 0.1)
                {
                    rb.linearVelocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                    animator.SetBool("Jumping", true);
                }
                else
                {

                    IsJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                IsJumping = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (feetPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}

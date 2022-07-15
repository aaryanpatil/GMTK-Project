
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    [Header("Horizontal Movement")]

    [SerializeField] float runSpeed = 10f;
    [SerializeField] private float smoothInputSpeed = 0.2f;
    
    Vector2 moveInput;
    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] int jumpCount = 0;
    [SerializeField] int maxJumps = 2;



    Rigidbody2D rb2d; 
    BoxCollider2D boxCollider2D;


    private void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();   
        boxCollider2D = GetComponent<BoxCollider2D>();   
    }

    private void FixedUpdate() 
    {
        Run();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        jumpCount++;
        if (value.isPressed && jumpCount <= maxJumps)
        {
            if(rb2d.velocity.y < 0)
                {
                    rb2d.AddForce(new Vector2(0f, jumpForce - rb2d.velocity.y), ForceMode2D.Impulse);
                }
                else if(rb2d.velocity.y > 0)
                {
                    rb2d.AddForce(new Vector2(0f, jumpForce - rb2d.velocity.y), ForceMode2D.Impulse);
                }
                else
                {
                    rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                }
        }
    }

    void Run()
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, moveInput, ref smoothInputVelocity, smoothInputSpeed);
        Vector2 playerVelocity = new Vector2(currentInputVector.x * runSpeed *  Time.fixedDeltaTime, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;     
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        } 
    }
}
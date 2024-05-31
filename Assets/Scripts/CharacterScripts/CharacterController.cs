using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 50;
    public float JumpForce = 2000.0f; // Adjust as needed
    public new Rigidbody2D rigidbody;
    private float horizontalInput = 0.0f;
    private bool isGrounded = false;

    public StoryManager storyManager;
    private SpriteRenderer spriteRenderer; // Reference to SpriteRenderer component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;
        horizontalInput = direction.x;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); 
        }
        if (transform.position.x < -115.4f)
        {
            transform.position = new Vector3(-2.1f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 115f)
        {
            transform.position = new Vector3(-2.1f, transform.position.y, transform.position.z);

        }
    }



    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void FixedUpdate()
    {

        Vector2 movement = new Vector2(horizontalInput, 0.0f);
        rigidbody.velocity = new Vector2(Mathf.Clamp(movement.x*100, -MovementSpeed, MovementSpeed), rigidbody.velocity.y);

        float horizontalVelocity = rigidbody.velocity.x;

        if (horizontalVelocity < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalVelocity > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
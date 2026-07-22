using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    // References to other components
    private Collider2D _collider;
    private Rigidbody2D _rb2d;

    // InputAction Variables
    public InputAction move;
    public InputAction jump;

    // Movement Variables
    public float speed;
    public float jumpHeight;
    public bool isOnGround;
    public float horizontalInput;
    public float verticalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Before first frame, define collider and rigidbody variables
    // Bind input actions to function calls
    void Start()
    {
        _collider = this.gameObject.GetComponent<Collider2D>();
        _rb2d = this.gameObject.GetComponent<Rigidbody2D>();

        // move.performed += OnMovePerformed;
        // jump.performed += OnJumpPerformed;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
        {
            horizontalInput = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalInput = 1f;
        }
        else
        {
            verticalInput = 0f;
        }
    }
    void FixedUpdate()
    {
        Vector2 moveVector;
        if (verticalInput!=0 && isOnGround)
        {
            moveVector = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, verticalInput * jumpHeight * Time.fixedDeltaTime);
        } else
        {
            moveVector = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, 0f);
        }
        verticalInput = 0f;
        _rb2d.linearVelocity = moveVector;
    }
    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveVector = ctx.ReadValue<Vector2>() * speed;
        _rb2d.AddForce(new Vector2(moveVector.x, _rb2d.linearVelocity.y), ForceMode2D.Impulse);
    }
    void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (!isOnGround) {return;}
        isOnGround = false;
        _rb2d.linearVelocity = new Vector2(_rb2d.linearVelocity.x, 1 * jumpHeight * Time.deltaTime);
    }
    /// <summary>
    /// If the player touches an object marked as "ground", set the flag to true
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }
    

    
}

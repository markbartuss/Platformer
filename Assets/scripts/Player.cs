using Unity.Jobs;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputSystem_Actions controls;
    Animator animator;

    private AudioManager audioManager;
    public AudioClip jump;

    private Vector2 moveInput;
    private float climbInput;
    public float moveSpeed = 5f;
    //jump
    public float jumpForce = 7f;
    public float groundRadius = 0.2f;
    private bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    //climb
    public float climbSpeed = 4f;
    private bool isClimbing;

    //Collectable
    public int fishes = 6;

    //second level teleport
    public Transform levelTeleport;
    public Transform playerPos;

    //oil
    public float slipperyMultiplier = 15f;
    public bool isSlippery = false;

    //menu
    public GameObject menu;
    public GameObject quest;

    bool isTp = false;
    public bool door = false;

    private Rigidbody2D rb;
    void Awake()
    {
        controls = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();
        jump = audioManager.jump;

        fishes = 6;
        slipperyMultiplier = 15f;
    }
    void FixedUpdate()
    {
        

        if (isClimbing)
        {
            // Disable gravity while climbing
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(moveInput.x * 2, climbInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = 2.5f;
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }

            // Check if player is touching ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    }

    void OnEnable()
    {
        controls.Player.Enable();
        //moving player
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        //animator.Play("run"); // plays the Run animation clip
     
        //jump assigned 
        controls.Player.Jump.performed += OnJump;
        //climb
        controls.Player.Climb.performed += ctx => climbInput = ctx.ReadValue<float>();
        controls.Player.Climb.canceled += ctx => climbInput = 0f;
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        
        
        if (isSlippery)
        {
            // Add force instead of setting velocity → sliding effect
            rb.AddForce(moveInput * moveSpeed * slipperyMultiplier);
        }
        else
        {
            transform.Translate(moveInput * Time.deltaTime * moveSpeed);
        }
        
        if (fishes == 3 && isTp == false)
        {
            LevelUpdate();
            isTp = true;
        }

        Finish();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioManager.PlaySFX(jump);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            // climb only when w or s pressed
            if (climbInput != 0)   
            {
                isClimbing = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
    void LevelUpdate()
    {
            rb.position = levelTeleport.position;                
    }
    
    //GAME FINISH 
    void Finish()
    {
        if (door == true && fishes == 0)
        {
            menu.SetActive(true);
            quest.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}

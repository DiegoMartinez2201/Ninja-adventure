using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float runSpeed = 5f;
    public float crouchSpeed = 2.5f; // Velocidad reducida al agacharse
    public float jumpSpeed = 8f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Salto Mejorado")]
    public bool betterJump = true;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Agacharse")]
    public float crouchScaleY = 0.5f;
    private float originalScaleY;
    private bool isCrouching = false;
    public LayerMask groundLayer;

    [Header("Componentes")]
    private Rigidbody2D rb2D;
    private BoxCollider2D col;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        originalScaleY = transform.localScale.y;
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;

        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // El Input de Salto y Agachado se procesa en Update para mayor precisión
        HandleJumpInput();
        HandleCrouchInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        ApplyBetterJump();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // d, right, a, left

        // Determinar velocidad actual (si está agachado, va más lento)
        float currentSpeed = isCrouching ? crouchSpeed : runSpeed;

        if (horizontal != 0)
        {
            rb2D.linearVelocity = new Vector2(horizontal * currentSpeed, rb2D.linearVelocity.y);
            spriteRenderer.flipX = horizontal < 0;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
            animator.SetBool("Run", false);
        }

        // Actualizar estados de suelo y animaciones
        if (CheckGround.isGrounded)
        {
            jumpsRemaining = maxJumps;
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
    }

    void HandleJumpInput()
    {
        // No permitir saltar si hay un techo encima mientras se está agachado
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
            jumpsRemaining--;
        }
    }

    void HandleCrouchInput()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Crouch();
        }
        else
        {
            TryStandUp();
        }
    }

    void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            animator.SetBool("Crouch", true); // Asegúrate de tener este parámetro en tu Animator

            // Reducir tamaño visual
            transform.localScale = new Vector3(transform.localScale.x, crouchScaleY, transform.localScale.z);

            // Ajustar collider
            col.size = new Vector2(originalColliderSize.x, originalColliderSize.y * crouchScaleY);
            col.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y * crouchScaleY);
        }
    }

    void TryStandUp()
    {
        if (isCrouching)
        {
            // Raycast para detectar si hay algo sobre la cabeza (usando la altura original)
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, originalColliderSize.y, groundLayer);

            if (hit.collider == null)
            {
                isCrouching = false;
                animator.SetBool("Crouch", false);

                // Volver a escala y collider original
                transform.localScale = new Vector3(transform.localScale.x, originalScaleY, transform.localScale.z);
                col.size = originalColliderSize;
                col.offset = originalColliderOffset;
            }
        }
    }

    void ApplyBetterJump()
    {
        if (betterJump)
        {
            if (rb2D.linearVelocity.y < 0)
            {
                rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb2D.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
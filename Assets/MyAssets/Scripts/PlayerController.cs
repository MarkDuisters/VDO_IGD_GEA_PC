using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Initialisation")]
    [SerializeField] float playerMass = 80f;
    [SerializeField] float collisionSweepDistance = 1f;
    enum CameraMode { FirstPerson, ThirdPerson }
    [SerializeField] CameraMode cameraMode = CameraMode.FirstPerson;
    Camera mainCam => Camera.main;

    [Header("Movement settings")]
    Vector3 moveDirection;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runMultiplier = 4;
    [SerializeField] float currentSpeed = 0;
    [SerializeField] float jumpMultiplier = 4;
    [SerializeField] int maxJumpCount = 1;
    bool isMoving = false;
    int jumpCount = 0;
    [Header("Ground and Slope Detection")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float maxSlopeAngle = 45f;
    bool isGrounded = true;
    bool isJump = false;
    [Header("Stairs detection")]
    [SerializeField] float stepHeight = 0.5f;

    Rigidbody rb => GetComponent<Rigidbody>();

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateRotation();
    }

    void UpdateRotation()
    {

        switch (cameraMode)
        {
            case CameraMode.FirstPerson:
                Vector3 camForward = mainCam.transform.forward;
                camForward.y = 0;
                transform.forward = camForward.normalized;
                break;
            case CameraMode.ThirdPerson:
                if (isMoving) transform.forward = moveDirection.normalized;
                break;
        }
    }

    void FixedUpdate()
    {
        //Rotation word uitgevoerd in de normale Update() omdat deze altijd moet reageren op de input.
        //En we zijn ook niet bezig met een rigidbody waarde voor de rotatie.
        Movement();
        Jump();
    }

    void Movement()
    {
        Vector3 velocity = rb.linearVelocity;
        RaycastHit hit;
        isGrounded = GroundDetection(out hit);
        //Reken de angle uit om te zien of we mogen bewegen of niet.
        //We willen niet dat de speler omhoog kan lopen op een te steile helling.
        float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);


        if (isGrounded)
        {
            if (slopeAngle <= maxSlopeAngle)
            {
                //Copy onze movedirection naar moveDir
                Vector3 moveDir = CalculateMoveDirection();
                //We projecteren deze zodat hij
                //parallel loopt met de slope waar we op zitten. Zo behouden we onze velocity.
                moveDir = Vector3.ProjectOnPlane(moveDir, hit.normal);
                velocity.x = moveDir.x;
                velocity.z = moveDir.z;
                // DetectStair();
            }
            else
            {
                Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal);
                velocity += slopeDirection * currentSpeed;
            }
        }
        rb.linearVelocity = velocity;
    }

    void Jump()
    {
        //Spring enkel als we een ground valid collider raken en we nog niet het max aantal jumps hebben bereikt.
        if (isJump && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector3.up * jumpMultiplier, ForceMode.VelocityChange);
            jumpCount++;
        }

    }

    void Initialize()
    {
        rb.mass = playerMass;
        currentSpeed = walkSpeed;
        isJump = false;
    }

    Vector3 CalculateMoveDirection()
    {
        moveDirection = TransformToCameraSpace(new Vector3(moveInput.x, 0, moveInput.y));
        Vector3 newDirection = moveDirection * currentSpeed;
        //  newDirection.y = rb.linearVelocity.y;

        return newDirection;
    }

    Vector3 TransformToCameraSpace(Vector3 input)
    {
        Vector3 camX, camZ;
        camX = mainCam.transform.right * input.x;
        camZ = mainCam.transform.forward * input.z;
        Vector3 finalDirection = camX + camZ;
        finalDirection.y = 0;
        return finalDirection;
    }

    /// <summary>
    /// Detecteer of we op de grond staan. We sturen ook de hit waarden terug voor slope detection.
    ///     
    bool GroundDetection(out RaycastHit hit)
    {
        CapsuleCollider playerCollider = GetComponent<CapsuleCollider>();
        float radius = playerCollider.radius;
        float distance = playerCollider.height * 0.5f;


        if (Physics.SphereCast(transform.position, radius, Vector3.down, out hit, distance, groundMask))
        {
            jumpCount = 0;
            return true;
        }
        return false;
    }

    /*  void DetectStair()
      {
          CapsuleCollider capsule = GetComponent<CapsuleCollider>();
          float radius = capsule.radius * 0.9f; // slightly smaller to avoid clipping
          float halfHeight = (capsule.height * 0.5f) - radius;

          Vector3 bottom = transform.position - Vector3.up * halfHeight;
          Vector3 top = transform.position + Vector3.up * halfHeight;

          // Forward capsule cast to detect obstacle in front within step distance
          if (Physics.CapsuleCast(bottom, top, radius, transform.forward, out RaycastHit hit, stepHeight, groundMask))
          {
              // Cast down from just above the obstacle to find top of step
              Vector3 stepCheckOrigin = hit.point + Vector3.up * stepHeight;
              if (Physics.Raycast(stepCheckOrigin, Vector3.down, out RaycastHit stepHit, stepHeight, groundMask))
              {
                  float stepRise = stepHit.point.y - bottom.y * stepHeight;

                  if (stepRise > 0 && stepRise <= stepHeight)
                  {
                      // Move the player up by the step rise
                      rb.position += Vector3.up * stepRise;
                      print($"Stair detected, moving up by: {stepRise:F2}");
                  }
              }
          }
      }*/
    #region inputs
    Vector2 moveInput;
    public void OnMove(InputValue context)
    {
        moveInput = context.Get<Vector2>();
        //isMoving is altijd true wanneer 1 van de inputs niet 0 is.
        //anders returnen we false, wat betekent dat we niet bewegen.
        isMoving = moveInput.x != 0 || moveInput.y != 0;

    }


    public void OnJump(InputValue context)
    {
        isJump = context.isPressed;

        print(isJump);
    }
    #endregion
}

using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    //public bool modelIsGrounded = true;

    [Header("Movement Speeds")]
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();

        if (inputManager.verticalInput > 0 || inputManager.horizontalInput != 0)
            HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput; // (., ., .) * z
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput; // (., ., .) * x
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.linearVelocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero; // (0 0 0)

        targetDirection = cameraObject.forward * inputManager.verticalInput; // z
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput; // x
        targetDirection.Normalize();
        targetDirection.y = 0; // напрямок у який треба повернутись

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // кінцевий поворот (., ., ., .)
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // плавно по-троху до кінцевого напрямку; переміщення за кадр

        transform.rotation = playerRotation;
    }

    /* public void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && modelIsGrounded)
        {
            Vector3 forwardDirection = moveDirection;
            forwardDirection.y = 0;
            forwardDirection.Normalize();

            float forwardForce = 10f;
            float upwardForce = 12f;

            Vector3 jumpForce = forwardDirection * forwardForce + Vector3.up * upwardForce;
            playerRigidbody.AddForce(jumpForce, ForceMode.Impulse);

            modelIsGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            modelIsGrounded = true;
        }
    }*/
}

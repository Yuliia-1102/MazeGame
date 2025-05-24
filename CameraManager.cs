using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform targetTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float followSpeed = 0.2f;
    public float cameraLookSpeed = 2;

   /*  public Transform cameraTransform;
    private float defaultPosition;
    public Transform cameraPivot;
    public float cameraCollisionRadius = 0.2f;
    public LayerMask collisionLayers;
    public float cameraCollisionOffSet = 0.2f;
    public float minColOffset = 0.2f;
    private Vector3 cameraVectorPosition; */
    private void Awake()
    {
        targetTransform = FindFirstObjectByType<PlayerManager>().transform; // компонент позиції, обертання і масштабу
        transform.position = targetTransform.position;
        /* cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z; */
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        // HandleCameraCollisions();
    }
        
    public void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, followSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 directionToLook = targetTransform.forward;
        directionToLook.y = 0; 

        if (directionToLook == Vector3.zero)
            directionToLook = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, cameraLookSpeed * Time.deltaTime);
    }
    
    /* private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = targetPosition - (distance - cameraCollisionOffSet);
        }

        if (Mathf.Abs(targetPosition) < minColOffset)
        {
            targetPosition = targetPosition - minColOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    } */
}
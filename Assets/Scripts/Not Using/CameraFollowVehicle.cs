using UnityEngine;

public class CameraFollowVehicle : MonoBehaviour
{
    public Transform targetTransform;
    public Transform reversePos;
    public float followSpeed = 10;
    public Vector3 offset = new Vector3(0f, 2.5f, -7.5f);
    public float rotationSmoothing = 5f;
    public bool isReversing = false;
    private Rigidbody rb;
    private float rbVelo;
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private Quaternion capturedRotation;
    private Vector3 targetAngles;

    private void Start()
    {
        rb = targetTransform.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        StayWithTarget();
        FollowTargetRotation();
        ReverseRotate();
    }
    private void Update()
    {
        MoveCamera();
    }

    void StayWithTarget()
    {
        if (targetTransform == null)
            return;

        if (!isReversing)
        {
            Vector3 _targetPos = targetTransform.position +
                             targetTransform.forward * offset.z +
                             targetTransform.right * offset.x +
                             targetTransform.up * offset.y;

            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * followSpeed);
        }
    }

    void ReverseRotate()
    {
        rbVelo = targetTransform.InverseTransformDirection(rb.velocity).z;
        if (rbVelo < -1f)
        {
            isReversing = true;
            transform.position = Vector3.Lerp(transform.position, reversePos.position, rotationSmoothing * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, reversePos.rotation, rotationSmoothing * Time.deltaTime);
        }
        else
        {
            isReversing = false;
        }
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FollowTargetRotation()
    {
        if (targetTransform != null && !isReversing)
        {
            capturedRotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, capturedRotation, rotationSmoothing * Time.deltaTime);
            targetAngles = capturedRotation.eulerAngles;
        }
    }
}

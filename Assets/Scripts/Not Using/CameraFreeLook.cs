using UnityEngine;

public class CameraFreeLook : MonoBehaviour
{
    public Vector2 rotationRange = new Vector2(70f, 361f);
    public float rotationSpeed = 10f;
    public float rotationSmoothing = 5f;
    private float m_inputH;
    private float m_inputV;
    public Transform targetTransform;
    private Vector3 targetAngles;
    private Quaternion capturedRotation;
    private bool isInFreeLook;
    private CameraFollowVehicle CameraFollowVehicle;

    private void OnEnable()
    {
        CameraFollowVehicle = GetComponent<CameraFollowVehicle>();
    }

    private void Update()
    {
        FreeLookRotation();
    }

    private void FixedUpdate()
    {
        FollowTargetRotation();
    }

    void FreeLookRotation()
    {
        if (/*Input.GetMouseButton(1) && */Time.timeScale > 0)
        {
            if (targetTransform == null)
                return;

            isInFreeLook = true;
            transform.rotation = capturedRotation;

            m_inputH = Input.GetAxis("Mouse X");
            m_inputV = Input.GetAxis("Mouse Y");

            if (targetAngles.y > 180f)
            {
                targetAngles.y -= 360f;
            }
            if (targetAngles.x > 180f)
            {
                targetAngles.x -= 360f;
            }
            if (targetAngles.y < -180f)
            {
                targetAngles.y += 360f;
            }
            if (targetAngles.x < -180f)
            {
                targetAngles.x += 360f;
            }

            targetAngles.y += m_inputH * rotationSpeed;
            targetAngles.x += m_inputV * rotationSpeed;

            targetAngles.y = Mathf.Clamp(targetAngles.y, -rotationRange.y * 0.5f, rotationRange.y * 0.5f);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -rotationRange.x * 0.5f, rotationRange.x * 0.5f);

            transform.rotation = Quaternion.Euler(-targetAngles.x, targetAngles.y, 0f);
        }
        else
        {
            isInFreeLook = false;
        }
    }


    void FollowTargetRotation()
    {
        if (targetTransform != null && !isInFreeLook && !CameraFollowVehicle.isReversing)
        {
            capturedRotation = Quaternion.LookRotation(targetTransform.forward, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, capturedRotation, rotationSmoothing * Time.deltaTime);
            targetAngles = capturedRotation.eulerAngles;
        }
    }
}

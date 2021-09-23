using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float velocityRequiredToZoomOut = 50f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 170f, 0f);
    [SerializeField] private float zoomOutDistance = 50f;
    private Rigidbody playerRb;
    private bool zoomedOut = false;

    /*
     * Set up the rotation of the camera and grab a ref to the player.
     * */
    private void Start()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        playerRb = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        StayWithTarget();
        ZoomWithSpeed();
    }

    /*
     * Used to move the camera with the player.
     */
    void StayWithTarget()
    {
        //Grab the rotations and positions of the camera this frame.
        float rotX = transform.localEulerAngles.x;
        float rotZ = transform.localEulerAngles.y;
        float posY = target.position.y;

        //If the player can not be found stop running.
        if (target == null)
            return;

        //Clamp the rotations and positions of the camera.
        rotX = Mathf.Clamp(rotX, 90, 90);
        rotZ = Mathf.Clamp(rotZ, 0, 0);
        posY = Mathf.Clamp(posY, offset.y, offset.y + zoomOutDistance);

        //Create a vector3 using the players position and our offset.
        Vector3 desiredPos = target.position +
                             target.forward * offset.z +
                             target.right * offset.x +
                             target.up * offset.y;

        //Set the position of the camera using the vector3 created above.
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);

        //Set the rotation of the camera using the players Y rotation and the camera X/Z rotations
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotX, player.localEulerAngles.y, rotZ), rotationSpeed * Time.deltaTime);
    }

    /*
     * When the player reaches the speed threshold zoom the camera out to get a larger viewable area on the minimap
     */
    void ZoomWithSpeed()
    {
        if (playerRb.velocity.magnitude > velocityRequiredToZoomOut && !zoomedOut)
        {
            offset += new Vector3(0, zoomOutDistance, 0);
            zoomedOut = true;
        }
        else if (playerRb.velocity.magnitude < velocityRequiredToZoomOut && zoomedOut)
        {
            offset -= new Vector3(0, zoomOutDistance, 0);
            zoomedOut = false;
        }
    }
}

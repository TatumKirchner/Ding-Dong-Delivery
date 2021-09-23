using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficController : MonoBehaviour
{
    private TrafficAI trafficAI;
    public Waypoint currentWaypoint;

    public float steeringSensitivity = 0.01f;
    public float breakingSensitivity = 1.0f;
    public float accelerationSensitivity = 0.3f;
    [SerializeField] private float lookAhead = 20f;
    float lastTimeMoving = 0;

    // Start is called before the first frame update
    void Start()
    {
        trafficAI = GetComponent<TrafficAI>();
    }

    void ProgressTracker()
    {
        //Check if we are close enough to get our next waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < lookAhead)
        {
            bool shouldBranch = false;

            //If the next waypoint is a branch then get a random number to see if we should turn
            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)];
            }
            else
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTracker();

        Vector3 localTarget;
        float targetAngle;

        //If the car is stuck on something start a timer
        if (trafficAI.rb.velocity.magnitude > 1)
        {
            lastTimeMoving = Time.time;
        }

        //Once the timer has reached its threshold reset the vehicle to its current waypoint
        if (Time.time > lastTimeMoving + 10)
        {
            trafficAI.transform.position = currentWaypoint.GetPosition();
            trafficAI.transform.rotation = currentWaypoint.transform.rotation;
        }

        //Get the direction we need to move in and the angle at which we need to set the wheels to reach the waypoint
        localTarget = trafficAI.transform.InverseTransformPoint(currentWaypoint.GetPosition());
        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        //Create our values to pass into the method to move the vehicle
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(trafficAI.currentSpeed);
        float speedFactor = trafficAI.currentSpeed / 20;
        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90f;

        float brake = 0;

        float accel = 1f;

        trafficAI.AccelerateCart(accel, steer, brake);
    }
}

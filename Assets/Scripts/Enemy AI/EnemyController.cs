using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyAiCar EnemyAiCar;

    [SerializeField] 
    private float steeringSensitivity = 0.01f;      //How quickly the steering tires will turn
    [SerializeField] 
    private float accelerationSensitivity = 0.3f;   //How quickly torque is added to the wheels
    private float lastTimeMoving = 0;               //Used as a timer when the car is stuck
    private float elapsed = 0.0f;                   //Used for a timer when the car is stuck

    private int stuckCount = 0;                     //Increments when the car is stuck

    private Vector3 currentWaypoint;                //Where we car currently moving towards
    private Transform agent;                        //Reference to the NavMesh Agent
    private Vector3 lastKnownPosition;              //The last known position of the player. Used in case the player is not on the NavMesh.

    private Rigidbody rb;                           //Reference to the cars Rigidbody
    private NavMeshPath path;                       //The path that is used to find the destination
    private EnemyTarget enemyTarget;                //An object on the player car to be used in path finding.

    private void Awake()
    {
        path = new NavMeshPath();
    }

    private void Start()
    {
        EnemyAiCar = GetComponent<EnemyAiCar>();
        rb = GetComponent<Rigidbody>();
        enemyTarget = GameObject.Find("Game Manager").GetComponent<EnemyTarget>();
        currentWaypoint = transform.position;
        agent = transform.Find("Agent");
    }

    private void Update()
    {
        //Calculates the path to the player every second. If the player is not on the NavMesh then find a path to the last known position.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;

            if (NavMesh.CalculatePath(agent.position, enemyTarget.target.position, NavMesh.AllAreas, path))
            {
                currentWaypoint = path.corners[1];
                lastKnownPosition = enemyTarget.target.position;
            }
            else if (NavMesh.CalculatePath(agent.position, lastKnownPosition, NavMesh.AllAreas, path))
            {
                currentWaypoint = path.corners[1];
            }
        }
        
    }

    private void FixedUpdate()
    {
        Move();
        YouStuck();

        //If the car is stuck move the car to a spot on the NavMesh and reset the path finding.
        if (stuckCount >= 350)
        {
            stuckCount = 0;
            if (FindNavMesh(out Vector3 point))
            {
                transform.position = point;
                currentWaypoint = transform.position;
            }
        }
    }

    //Find a spot on the NavMesh near the car.
    bool FindNavMesh(out Vector3 result)
    {
        if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 100, out NavMeshHit hit, 50f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    //Find the Angle to the waypoint for steering.
    //Calculate how much acceleration to apply.
    //Send value to the method to move the car.
    void Move()
    {
        Vector3 localTarget;
        float targetAngle;

        localTarget = EnemyAiCar.transform.InverseTransformPoint(currentWaypoint);
        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;


        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(EnemyAiCar.current_speed);
        float speedFactor = EnemyAiCar.current_speed / 20;
        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90f;

        float brake = 0;

        float accel = 1f;
        if (corner > 20 && speedFactor > 0.1f && speedFactor > 0.2f)
            accel = Mathf.Lerp(0, 1 * accelerationSensitivity, 1 - cornerFactor);

        EnemyAiCar.AccelerateCart(accel, steer, brake);
    }

    //Increment stuckCount if the car is stopped. Used to reset the car when stuck.
    void YouStuck()
    {
        if (rb.velocity.magnitude > 1)
        {
            lastTimeMoving = Time.time;
        }

        if (Time.time > lastTimeMoving + 4)
        {
            stuckCount++;
        }
    }
}

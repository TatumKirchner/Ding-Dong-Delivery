using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiController : MonoBehaviour
{
    public EnemyDestination EnemyDestination;
    private EnemyAiCar EnemyAiCar;
    public float steeringSensitivity = 0.01f;
    public float breakingSensitivity = 1.0f;
    public float accelerationSensitivity = 0.3f;

    public GameObject trackerPrefab;
    NavMeshAgent agent;

    int currentTrackerWP;
    [SerializeField] float lookAhead = 10;

    float lastTimeMoving = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAiCar = GetComponent<EnemyAiCar>();
        GameObject tracker = Instantiate(trackerPrefab, EnemyAiCar.transform.position, EnemyAiCar.transform.rotation) as GameObject;
        agent = tracker.GetComponent<NavMeshAgent>();
        EnemyDestination = GameObject.Find("Enemy Destination").GetComponent<EnemyDestination>();
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(agent.transform.position, EnemyAiCar.transform.position) > lookAhead)
        {
            agent.isStopped = true;
            return;
        }
        else
        {
            agent.isStopped = false;
        }

        agent.SetDestination(EnemyDestination.target.position);
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTracker();

        Vector3 localTarget;
        float targetAngle;

        if (EnemyAiCar._rigidbody.velocity.magnitude > 1)
        {
            lastTimeMoving = Time.time;
        }

        if (Time.time > lastTimeMoving + 4)
        {

            gameObject.GetComponent<Rigidbody>().MovePosition(transform.position - transform.forward * .75f);
            agent.transform.position = EnemyAiCar.transform.position;
        }

        localTarget = EnemyAiCar.transform.InverseTransformPoint(agent.transform.position);
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
}

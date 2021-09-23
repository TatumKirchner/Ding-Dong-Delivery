using UnityEngine;
using UnityEngine.InputSystem;

public class ResetCar : MonoBehaviour
{
    // Automatically put the car the right way up, if it has come to rest upside-down.
    [SerializeField] private float m_WaitTime = 3f;           // time to wait before self righting
    [SerializeField] private float m_VelocityThreshold = 1f;  // the velocity below which the car is considered stationary for self-righting

    private float m_LastOkTime; // the last time that the car was in an OK state
    private Rigidbody m_Rigidbody;
    private GameplayControls playerControls;
    private IsPaused isPaused;

    private void Awake()
    {
        playerControls = new GameplayControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        isPaused = GameObject.Find("Game Manager").GetComponent<IsPaused>();
        m_Rigidbody = GetComponent<Rigidbody>();
        playerControls.Gameplay.ResetCar.performed += _ => ResetVehicle();
        isPaused = GameObject.Find("Game Manager").GetComponent<IsPaused>();
    }

    // Update is called once per frame
    void Update()
    {
        // is the car the right way up
        if (transform.up.y > 0f || m_Rigidbody.velocity.magnitude > m_VelocityThreshold)
        {
            m_LastOkTime = Time.time;
        }

        //If the car has been off its wheels for long enough right the car
        if (Time.time > m_LastOkTime + m_WaitTime && !isPaused.isPaused)
        {
            RightCar();
        }
    }

    //Method for forcing the car back to an upright position
    void ResetVehicle()
    {
        if (!isPaused.isPaused)
        {
            Transform t = GetComponent<Transform>();
            t.position = t.position + new Vector3(0, 0.2f, 0);
            t.rotation = new Quaternion(0, -1, 0, 1);
        }  
    }

    private void RightCar()
    {
        // set the correct orientation for the car, and lift it off the ground a little
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}

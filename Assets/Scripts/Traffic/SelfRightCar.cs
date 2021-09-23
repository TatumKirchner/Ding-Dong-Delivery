using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRightCar : MonoBehaviour
{
    //Used for AI vehicles to right themselves

    Rigidbody m_Rigidbody;

    [SerializeField] float m_VelocityThreshold = 1f;
    [SerializeField] float m_WaitTime = 2f;
    float m_LastOkTime;

    IsPaused isPaused;

    private void OnEnable()
    {
        isPaused = GameObject.Find("Game Manager").GetComponent<IsPaused>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0f || m_Rigidbody.velocity.magnitude > m_VelocityThreshold)
        {
            m_LastOkTime = Time.time;
        }

        if (Time.time > m_LastOkTime + m_WaitTime && !isPaused.isPaused)
        {
            RightCar();
        }
    }

    private void RightCar()
    {
        // set the correct orientation for the car, and lift it off the ground a little
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}

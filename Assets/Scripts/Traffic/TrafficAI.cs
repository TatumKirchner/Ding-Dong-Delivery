using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficAI : MonoBehaviour
{
    public WheelCollider[] wheels;
    public Transform[] wheelMeshes;

    public float wheelTorque = 200;
    public float brakeTorque = 500;
    public float maxSteerAngle = 30;

    [SerializeField] Vector3 centerOfMassOffset;

    private Vector3 wheelPosition;
    private Quaternion wheelRotation;

    public float currentSpeed;

    [HideInInspector]
    public Rigidbody rb;

    public bool isAICart;

    public float maxSpeed = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheels[0].attachedRigidbody.centerOfMass = centerOfMassOffset;
    }

    //Method to move the cart towards their waypoint
    public void AccelerateCart(float v, float h, float b)
    {
        currentSpeed = Mathf.RoundToInt(rb.velocity.magnitude * 3.6f);

        if (v > 0)
        {
            if (currentSpeed < maxSpeed)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheelTorque;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = 0;
                }
            }
        }
        else if (v < 0)
        {
            if (currentSpeed > -5)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheelTorque;
                }
            }
            else
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = 0;
                }
            }
        }

        if (v == 0 && !isAICart)
        {
            b = 0.3f;
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = Mathf.Clamp(b, 0f, 1f) * brakeTorque;
        }

        wheels[0].steerAngle = Mathf.Clamp(h, -1f, 1f) * maxSteerAngle;
        wheels[1].steerAngle = Mathf.Clamp(h, -1f, 1f) * maxSteerAngle;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMeshes[i].position = wheelPosition;
            wheelMeshes[i].rotation = wheelRotation;
        }
    }
}

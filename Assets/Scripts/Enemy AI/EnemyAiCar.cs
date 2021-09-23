using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiCar : MonoBehaviour
{
    public WheelCollider[] wheels;      //Array of the cars Wheel Colliders
    public Transform[] wheels_mesh;     //Array of the cars wheel position
    public float wheel_torque = 200;    //The amount of torque to apply to the drive wheels
    public float brake_torque = 500;    //The amount of torque to apply to the wheels when braking
    public float max_steerangle = 30;   //The max angle at which the steering wheels will turn

    private Vector3 wheel_position;     //Temporary var to hold a wheel position
    private Quaternion wheel_rotation;  //Temporary var to hold a wheel rotation

    public float current_speed;         //The current speed of the car

    [HideInInspector]
    public Rigidbody _rigidbody;        //Reference to the cars rigidbody

    private Vector3 savedPauseVelocity;             //Saves the cars velocity on pause
    private Vector3 savedPauseAngularVelocity;      //Saves the cars angular velocity on pause

    public float maxSpeed = 30;     //The max speed of the car

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //Method to drive/move the car
    public void AccelerateCart(float v, float h, float b)
    {
        current_speed = Mathf.RoundToInt(_rigidbody.velocity.magnitude * 3.6f);

        if (v > 0)
        {
            if (current_speed < maxSpeed)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheel_torque;
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
            if (current_speed > -5)
            {
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque = Mathf.Clamp(v, -1f, 1f) * wheel_torque;
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

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].brakeTorque = Mathf.Clamp(b, 0f, 1f) * brake_torque;
        }

        wheels[0].steerAngle = Mathf.Clamp(h, -1f, 1f) * max_steerangle;
        wheels[1].steerAngle = Mathf.Clamp(h, -1f, 1f) * max_steerangle;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            wheels_mesh[i].position = wheel_position;
            wheels_mesh[i].rotation = wheel_rotation;
        }
    }

    public void OnPause(bool pause)
    {
        if (pause)
        {
            savedPauseVelocity = _rigidbody.velocity;
            savedPauseAngularVelocity = _rigidbody.angularVelocity;
            if (_rigidbody.isKinematic == false)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
        }
        else
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = savedPauseVelocity;
            _rigidbody.angularVelocity = savedPauseAngularVelocity;
        }
    }
}

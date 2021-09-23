using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerHelper : MonoBehaviour
{
    public Rigidbody body;
    private EnemyAiCar EnemyAiCar;

    public WheelCollider wheelL;
    public WheelCollider wheelR;
    public float antiRollVal = 5000f;
    private float m_OldRotation;
    [Range(0, 1)] [SerializeField] private float m_SteerHelper;
    private Rigidbody m_Rigidbody;
    [SerializeField] private float m_Downforce = 200f;
    [SerializeField] private Vector3 m_CentreOfMassOffset;

    //This class is used to add some forces to the car to assist in turning. Most of this is from the Unity Standard assets.

    private void Start()
    {
        EnemyAiCar = GetComponent<EnemyAiCar>();
        m_Rigidbody = GetComponent<Rigidbody>();

        EnemyAiCar.wheels[0].attachedRigidbody.centerOfMass = m_CentreOfMassOffset;
    }

    void FixedUpdate()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = wheelL.GetGroundHit(out WheelHit hit);
        if (groundedL)
        {
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
        }

        bool groundedR = wheelR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRollVal;

        if (groundedL)
            body.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);

        if (groundedR)
            body.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);

        SteeringHelper();
        AddDownForce();
    }

    private void SteeringHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelhit;
            EnemyAiCar.wheels[i].GetGroundHit(out wheelhit);
            if (wheelhit.normal == Vector3.zero)
                return; // wheels are not on the ground so don't realign the rigidbody velocity
        }

        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            m_Rigidbody.velocity = velRotation * m_Rigidbody.velocity;
        }
        m_OldRotation = transform.eulerAngles.y;
    }

    private void AddDownForce()
    {
        EnemyAiCar.wheels[0].attachedRigidbody.AddForce(-transform.up * m_Downforce *
                                                     EnemyAiCar.wheels[0].attachedRigidbody.velocity.magnitude);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        //When the player hits the fence turn off the kinematic
        if (other.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }
}

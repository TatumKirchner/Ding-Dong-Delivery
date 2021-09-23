using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapons : MonoBehaviour
{
    private RespectPoints RespectPoints;
    [SerializeField] private float respectPointsToAdd;
    [SerializeField] private float timeToDestroy = 5;
    [SerializeField] private float impactForce = 100f;

    private void Start()
    {
        RespectPoints = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespectPoints>();
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the weapon hits an enemy add force to it and add respect points to the player
        if (collision.collider.CompareTag("Enemy"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 position = contact.point;
            collision.collider.attachedRigidbody.AddForce(position * impactForce, ForceMode.Impulse);
            RespectPoints.AddPoints(respectPointsToAdd);
        }
    }
}

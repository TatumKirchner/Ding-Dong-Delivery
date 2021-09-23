using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderDropOff : MonoBehaviour
{
    OrderManager orderManager;

    private void Start()
    {
        orderManager = GameObject.Find("Game Manager").GetComponent<OrderManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(orderManager.DropoffOrder());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }
}

using System.Collections;
using UnityEngine;

public class DeliveryPickup : MonoBehaviour
{
    [SerializeField] private float m_waitTime = 5f;
    [SerializeField] private ParticleSystem m_pickupPS;
    private OrderManager orderManager;
    [SerializeField] private AudioSource source;

    private void OnEnable()
    {
        orderManager = GameObject.Find("Game Manager").GetComponent<OrderManager>();
    }

    //When the player enters the trigger start order pickup Coroutine.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(OrderPickup());
        }
    }

    //When player exits the trigger stop the Coroutine
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    //Coroutine that picks up the order. Used mainly as a timer so the player has to come to a complete stop.
    IEnumerator OrderPickup()
    {
        if (orderManager.oneStar)
        {
            yield return new WaitForSeconds(m_waitTime);
            orderManager.isPickedup = true;
            m_pickupPS.Play();
            if (source != null)
            {
                source.Play();
            }
        }
        if (orderManager.twoStar)
        {
            yield return new WaitForSeconds(m_waitTime);
            orderManager.isPickedup = true;
            m_pickupPS.Play();
            if (source != null)
            {
                source.Play();
            }
        }
        if (orderManager.threeStar)
        {
            yield return new WaitForSeconds(m_waitTime);
            orderManager.isPickedup = true;
            m_pickupPS.Play();
            if (source != null)
            {
                source.Play();
            }
        }
    }
}

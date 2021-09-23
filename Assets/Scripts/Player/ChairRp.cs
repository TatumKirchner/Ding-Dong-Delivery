using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairRp : MonoBehaviour
{
    private RespectPoints rp;
    [SerializeField] float pointsToAdd = 1f;
    [SerializeField] private int hitsAllowed = 5;
    private int hits = 0;
    private AudioSource source;

    private void Start()
    {
        rp = GameObject.Find("Game Manager").GetComponent<RespectPoints>();
        source = GetComponent<AudioSource>();
    }

    //When a collision happens with the player add points
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (source != null)
            {
                source.Play();
            }

            if (hits < hitsAllowed)
            {
                rp.AddPoints(pointsToAdd);
                hits++;
            }
        }        
    }
}

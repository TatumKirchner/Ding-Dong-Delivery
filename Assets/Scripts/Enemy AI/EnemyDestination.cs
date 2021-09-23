using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestination : MonoBehaviour
{
    public Transform target;        //Gets the players transform at Start. Enemies will grab it from here.

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}

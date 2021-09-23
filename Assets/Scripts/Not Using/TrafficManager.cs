using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    private GameObject spawningRoutesOne;
    //private GameObject spawningRoutesTwo;
    //private GameObject spawningRoutesThree;

    [SerializeField] private float spawnWaitTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        spawningRoutesOne = GameObject.Find("Spawning Routes 1");
        //spawningRoutesTwo = GameObject.Find("Spawning Routes 2");
        //spawningRoutesThree = GameObject.Find("Spawning Routes 3");

        spawningRoutesOne.SetActive(false);
        //spawningRoutesTwo.SetActive(false);
        //spawningRoutesThree.SetActive(false);

        StartCoroutine(ActivateRoutes(spawnWaitTime, spawningRoutesOne));
        //StartCoroutine(ActivateRoutes(spawnWaitTime + 10f, spawningRoutesTwo));
        //StartCoroutine(ActivateRoutes(spawnWaitTime + 20f, spawningRoutesThree));
    }

    IEnumerator ActivateRoutes(float waitTime, GameObject route)
    {
        yield return new WaitForSeconds(waitTime);
        route.SetActive(true);
    }
}

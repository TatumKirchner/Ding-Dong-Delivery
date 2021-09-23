using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSpawner : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject suvPrefabBlue, suvPrefabGray, suvPrefabRed,
        truckPrefabBase, truckPrefabBrown, truckPrefabGray, truckPrefabGreen,
        sedanPrefabBlue, sedanPrefabGray, SedanPrefabGreen,
        vanPrefabBlue, vanPrefabGray, vanPrefabGreen, copCar, ambulance, fireTruck;
    public int numberToSpawn;

    // Add the vehicle prefabs to the list
    void Start()
    {
        prefabList.Add(suvPrefabBlue);
        prefabList.Add(suvPrefabGray);
        prefabList.Add(suvPrefabRed);
        prefabList.Add(truckPrefabBase);
        prefabList.Add(truckPrefabBrown);
        prefabList.Add(truckPrefabGray);
        prefabList.Add(truckPrefabGreen);
        prefabList.Add(sedanPrefabBlue);
        prefabList.Add(sedanPrefabGray);
        prefabList.Add(SedanPrefabGreen);
        prefabList.Add(vanPrefabBlue);
        prefabList.Add(vanPrefabGray);
        prefabList.Add(vanPrefabGreen);
        prefabList.Add(copCar);
        prefabList.Add(ambulance);
        prefabList.Add(fireTruck);

        StartCoroutine(Spawn());
    }

    //Spawn traffic at random waypoints
    IEnumerator Spawn()
    {
        int count = 0;
        while (count < numberToSpawn)
        {
            int prefabIndex = Random.Range(0, 16);
            GameObject obj = Instantiate(prefabList[prefabIndex]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<TrafficController>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            obj.transform.rotation = child.rotation;

            yield return new WaitForEndOfFrame();

            count++;
        }        
    }
}

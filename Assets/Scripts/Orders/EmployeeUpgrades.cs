using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeUpgrades : MonoBehaviour
{
    [HideInInspector]
    public bool moises = false;
    [HideInInspector]
    public bool kyle = false;
    [HideInInspector]
    public bool brenda = false;
    [HideInInspector]
    public bool johnny = false;
    [HideInInspector]
    public bool tito = false;
    [HideInInspector]
    public bool hire = false;

    public float employeeCost = 150f;

    [Range(0.1f, 0.9f)]
    public float multiplyer = 0.15f;
    private float baseMultiplyer = 0.15f;

    [HideInInspector]
    public int numberOfEmployees = 0;

    private void Start()
    {
        multiplyer += 1;
    }

    private void Update()
    {
        HireEmployee();
    }

    //Used to Hire an employee. Called from the shop class. Increased the multiplier of payment from deliveries.
    public void HireEmployee()
    {
        if (hire)
        {
            switch (numberOfEmployees)
            {
                case 1:
                    hire = false;
                    moises = true;
                    kyle = false;
                    brenda = false;
                    johnny = false;
                    tito = false;
                    multiplyer += baseMultiplyer;
                    break;
                case 2:
                    hire = false;
                    moises = true;
                    kyle = true;
                    brenda = false;
                    johnny = false;
                    tito = false;
                    multiplyer += baseMultiplyer;
                    break;
                case 3:
                    hire = false;
                    moises = true;
                    kyle = true;
                    brenda = true;
                    johnny = false;
                    tito = false;
                    multiplyer += baseMultiplyer;
                    break;
                case 4:
                    hire = false;
                    moises = true;
                    kyle = true;
                    brenda = true;
                    johnny = true;
                    tito = false;
                    multiplyer += baseMultiplyer;
                    break;
                case 5:
                    hire = false;
                    moises = true;
                    kyle = true;
                    brenda = true;
                    johnny = true;
                    tito = true;
                    multiplyer += baseMultiplyer;
                    break;
                default:
                    hire = false;
                    break;
            }
        }
    }
}

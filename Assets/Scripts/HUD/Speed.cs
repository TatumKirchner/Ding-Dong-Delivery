using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    public Rigidbody player;
    public float maxSpeed;

    [SerializeField] private float minSpeedAngle;
    [SerializeField] private float maxSpeedAngle;

    [SerializeField] private RectTransform arrow;
    [SerializeField] private Text mph;
    private float speed;
    [SerializeField] CarSwitch carSwitch;

    private void Start()
    {
        maxSpeed = player.GetComponent<PlayerCarController>().MaxSpeed;
    }

    private void Update()
    {
        //Convert to MPH
        speed = player.velocity.magnitude * 2.23693629f;

        //Move the UI needle depending on the player speed.
        if (arrow != null)
        {
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedAngle, maxSpeedAngle, speed / maxSpeed));
        }
        //Update the UI text of the MPH readout to the player speed.
        if (mph != null)
        {
            speed = Mathf.RoundToInt(speed);
            mph.text = speed.ToString();
        }
    }

    //If the players car gets an upgrade or the player buys a new car update the player ref and get the new max speed so the needle is more accurate.
    public void UpdateMaxSpeed()
    {
        player = carSwitch.currentCar.GetComponent<Rigidbody>();
        maxSpeed = player.GetComponent<PlayerCarController>().MaxSpeed;
    }
}

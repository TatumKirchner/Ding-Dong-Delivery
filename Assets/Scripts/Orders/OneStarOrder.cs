using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneStarOrder : MonoBehaviour
{
    [SerializeField] private GameObject m_pickupSpot, m_pickupSpot2, m_pickupSpot3;
    [SerializeField] private GameObject m_dropoffSpot, m_dropoffSpot2, m_dropoffSpot3;
    [SerializeField] private GameObject foodDamageHUD;
    [SerializeField] private GameObject timer;

    [SerializeField] private float orderPickupTimer = 120f;

    [SerializeField] private OrderTimer OrderTimer;
    [SerializeField] private GpsPath gps;

    public bool pickedUp = false;
    public bool oneStar, twoStar, threeStar;

    // Turn the pickup and drop off spots off. They need to be on so other classes can access them before start.
    void Start()
    {
        m_pickupSpot.SetActive(false);
        m_pickupSpot2.SetActive(false);
        m_pickupSpot3.SetActive(false);
        m_dropoffSpot.SetActive(false);
        foodDamageHUD.SetActive(false);
        oneStar = false;
        twoStar = false;
        threeStar = false;
    }

    //Sets the respective pickup spot on, sets it to the gps target, and starts the timer.
    #region OrderStart
    public void OrderOneStar()
    {
        m_pickupSpot.SetActive(true);
        oneStar = true;
        gps.target = m_pickupSpot.transform;
        SetTimer();
    }

    public void OrderTwoStar()
    {
        m_pickupSpot2.SetActive(true);
        twoStar = true;
        gps.target = m_pickupSpot2.transform;
        SetTimer();
    }

    public void OrderThreeStar()
    {
        m_pickupSpot3.SetActive(true);
        threeStar = true;
        gps.target = m_pickupSpot3.transform;
        SetTimer();
    }
    #endregion

    private void Update()
    {
        OrderPickedUp();
    }

    //When the player has stopped in the trigger it calls this method.
    void OrderPickedUp()
    {
        if (pickedUp && oneStar)
        {
            gps.target = m_dropoffSpot.transform;
            m_pickupSpot.SetActive(false);
            m_dropoffSpot.SetActive(true);
            foodDamageHUD.SetActive(true);
        }
        if (pickedUp && twoStar)
        {
            gps.target = m_dropoffSpot2.transform;
            m_pickupSpot2.SetActive(false);
            m_dropoffSpot2.SetActive(true);
            foodDamageHUD.SetActive(true);
        }
        if (pickedUp && threeStar)
        {
            gps.target = m_dropoffSpot3.transform;
            m_pickupSpot3.SetActive(false);
            m_dropoffSpot3.SetActive(true);
            foodDamageHUD.SetActive(true);
        }
    }

    //Access the timer class and start the timer.
    void SetTimer()
    {
        timer.SetActive(true);
        OrderTimer.timeRemaining = orderPickupTimer;
        OrderTimer.timerIsRunning = true;
    }
}

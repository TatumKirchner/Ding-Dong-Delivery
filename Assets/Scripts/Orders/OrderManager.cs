using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderManager : MonoBehaviour
{
    #region Pickup/Drop off orders vars
    [Header("Pickups")]
    public GameObject[] oneStarPickups;
    public GameObject[] twoStarPickups;
    public GameObject[] threeStarPickups;
    private GameObject currentPickupSpot;
    [HideInInspector] public bool foodCanDamage = false;

    [Header("Drop offs")]
    public GameObject[] oneStarDropoffs;
    public GameObject[] twoStarDropoffs;
    public GameObject[] threeStarDropoffs;
    private GameObject currentDropoffSpot;
    public float dropoffWaitTime = 5f;
    [SerializeField] private ParticleSystem dropoffPs;
    #endregion

    #region Order Screens
    [Header("Order Screens")]
    [SerializeField] private GameObject[] oneStarOrderScreens;
    [SerializeField] private GameObject[] twoStarOrderScreens;
    [SerializeField] private GameObject[] threeStarOrderScreens;
    [SerializeField] private GameObject ceoPanel;
    private GameObject currentOrderScreen;
    #endregion

    #region Timer
    [Header("Timer")]
    [SerializeField] private float timeToPickup = 120f;
    [SerializeField] private float oneStarOrderTimer = 90f;
    [SerializeField] private float twoStarOrderTimer = 120f;
    [SerializeField] private float threeStarOrderTimer = 160f;
    [SerializeField] private GameObject timer;
    #endregion

    #region Payments
    [Header("Max Payments")]
    [SerializeField] private float oneStarPayment = 8.40f;
    [SerializeField] private float twoStarPayment = 14.60f;
    [SerializeField] private float threeStarPayment = 21.60f;
    [SerializeField] private float rpToAdd = 30f;
    private float currentRpToAdd;
    private float currentOneStarPayment, currentTwoStarPayment, currentThreeStarPayment;

    [HideInInspector] public bool oneStarOrderStarted = false;
    [HideInInspector] public bool twoStarOrderStarted = false;
    [HideInInspector] public bool threeStarOrderStarted = false;
    [HideInInspector] public bool oneStar = false;
    [HideInInspector] public bool twoStar = false;
    [HideInInspector] public bool threeStar = false;
    [HideInInspector] public bool isPickedup = false;
    private bool startTimer = false;
    #endregion

    #region Class References
    [Header("Class References")]
    [SerializeField] OrderTimer orderTimer;
    public GpsPath gps;
    private FoodDamage foodDamage;
    private Money money;
    private RespectPoints respectPoints;
    private OrderAudioManager orderAudio;
    private DrivingAroundMusic music;
    #endregion

    #region UI Elements
    [Header("UI Elements")]
    [SerializeField] public GameObject foodDamageHud;
    [SerializeField] public FoodDamageBar foodDamageBar;
    [SerializeField] private Image ordermenu;
    [SerializeField] private GameObject buttonOne;
    [SerializeField] private GameObject buttonTwo;
    [SerializeField] private GameObject buttonThree;
    #endregion

    #region CurrentOrder
    [Header("Current Order Number")]
    public int currentOneStarOrder = 0;
    public int currentTwoStarOrder = 0;
    public int currentThreeStarOrder = 0;
    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField] private AudioSource deliverySuccess;
    [SerializeField] private AudioSource deliveryFail;
    [HideInInspector] public bool introFinished = false;
    #endregion

    private void Awake()
    {
        //Turn off all of the pickup and drop off spots.

        foreach (GameObject pickupSpots in oneStarPickups)
            pickupSpots.SetActive(false);

        foreach (GameObject pickupSpots in twoStarPickups)
            pickupSpots.SetActive(false);

        foreach (GameObject pickupSpots in threeStarPickups)
            pickupSpots.SetActive(false);

        foreach (GameObject dropoffSpots in oneStarDropoffs)
            dropoffSpots.SetActive(false);

        foreach (GameObject dropoffSpots in twoStarDropoffs)
            dropoffSpots.SetActive(false);

        foreach (GameObject dropoffSpots in threeStarDropoffs)
            dropoffSpots.SetActive(false);

        foreach (GameObject orderPanels in oneStarOrderScreens)
            orderPanels.SetActive(false);

        foreach (GameObject orderPanels in twoStarOrderScreens)
            orderPanels.SetActive(false);

        foreach (GameObject orderPanels in threeStarOrderScreens)
            orderPanels.SetActive(false);
    }

    private void Start()
    {
        money = GetComponent<Money>();
        respectPoints = GetComponent<RespectPoints>();
        foodDamage = GetComponent<FoodDamage>();
        timer.SetActive(false);
        ResetPayment();
        foodDamageHud.SetActive(false);
        orderAudio = GetComponent<OrderAudioManager>();
        music = GetComponent<DrivingAroundMusic>();
        ordermenu.enabled = false;
        buttonOne.SetActive(false);
        buttonTwo.SetActive(false);
        buttonThree.SetActive(false);
    }

    private void Update()
    {
        OrderStarted();
        OrderPickedUp();
        TimesUp();

        //Used on a new game. Turns all of the UI on and switches the intro panel off.
        if (music.introPlayed && !introFinished)
        {
            ceoPanel.SetActive(false);
            ordermenu.enabled = true;
            buttonOne.SetActive(true);
            buttonTwo.SetActive(true);
            buttonThree.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttonOne);
            introFinished = true;
        }
    }

    //Switch statements are used to handle which pickup and drop off spots to turn on.
    void OrderStarted()
    {
        if (oneStarOrderStarted && currentOneStarOrder < 6 && music.introPlayed)
        {
            oneStar = true;
            //TurnEnemiesOff();
            switch (currentOneStarOrder)
            {
                case 0:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[0].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 0);
                    oneStarPickups[0].SetActive(true);
                    currentPickupSpot = oneStarPickups[0];
                    currentOrderScreen = oneStarOrderScreens[0];
                    gps.target = oneStarPickups[0].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 1:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[1].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 1);
                    oneStarPickups[1].SetActive(true);
                    currentPickupSpot = oneStarPickups[1];
                    currentOrderScreen = oneStarOrderScreens[1];
                    gps.target = oneStarPickups[1].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 2:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[2].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 2);
                    oneStarPickups[2].SetActive(true);
                    currentPickupSpot = oneStarPickups[2];
                    currentOrderScreen = oneStarOrderScreens[2];
                    gps.target = oneStarPickups[2].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 3:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[3].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 3);
                    oneStarPickups[3].SetActive(true);
                    currentPickupSpot = oneStarPickups[3];
                    currentOrderScreen = oneStarOrderScreens[3];
                    gps.target = oneStarPickups[3].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 4:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[4].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 4);
                    oneStarPickups[4].SetActive(true);
                    currentPickupSpot = oneStarPickups[4];
                    currentOrderScreen = oneStarOrderScreens[4];
                    gps.target = oneStarPickups[4].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 5:
                    oneStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    oneStarOrderScreens[5].SetActive(true);
                    orderAudio.OrderStartedAudio(1, 5);
                    oneStarPickups[5].SetActive(true);
                    currentPickupSpot = oneStarPickups[5];
                    currentOrderScreen = oneStarOrderScreens[5];
                    gps.target = oneStarPickups[5].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
            }
        }
        if (twoStarOrderStarted && currentTwoStarOrder < 6)
        {
            twoStar = true;
            switch (currentTwoStarOrder)
            {
                case 0:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[0].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 0);
                    twoStarPickups[0].SetActive(true);
                    currentPickupSpot = twoStarPickups[0];
                    currentOrderScreen = twoStarOrderScreens[0];
                    gps.target = twoStarPickups[0].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 1:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[1].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 1);
                    twoStarPickups[1].SetActive(true);
                    currentPickupSpot = twoStarPickups[1];
                    currentOrderScreen = twoStarOrderScreens[1];
                    gps.target = twoStarPickups[1].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 2:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[2].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 2);
                    twoStarPickups[2].SetActive(true);
                    currentPickupSpot = twoStarPickups[2];
                    currentOrderScreen = twoStarOrderScreens[2];
                    gps.target = twoStarPickups[2].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 3:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[3].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 3);
                    twoStarPickups[3].SetActive(true);
                    currentPickupSpot = twoStarPickups[3];
                    currentOrderScreen = twoStarOrderScreens[3];
                    gps.target = twoStarPickups[3].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 4:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[4].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 4);
                    twoStarPickups[4].SetActive(true);
                    currentPickupSpot = twoStarPickups[4];
                    currentOrderScreen = twoStarOrderScreens[4];
                    gps.target = twoStarPickups[4].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 5:
                    twoStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    twoStarOrderScreens[5].SetActive(true);
                    orderAudio.OrderStartedAudio(2, 5);
                    twoStarPickups[5].SetActive(true);
                    currentPickupSpot = twoStarPickups[5];
                    currentOrderScreen = twoStarOrderScreens[5];
                    gps.target = twoStarPickups[5].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
            }
        }
        if (threeStarOrderStarted && currentThreeStarOrder < 5)
        {
            threeStar = true;
            switch (currentThreeStarOrder)
            {
                case 0:
                    threeStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    threeStarOrderScreens[0].SetActive(true);
                    orderAudio.OrderStartedAudio(3, 0);
                    threeStarPickups[0].SetActive(true);
                    currentPickupSpot = threeStarPickups[0];
                    currentOrderScreen = threeStarOrderScreens[0];
                    gps.target = threeStarPickups[0].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 1:
                    threeStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    threeStarOrderScreens[1].SetActive(true);
                    orderAudio.OrderStartedAudio(3, 1);
                    threeStarPickups[1].SetActive(true);
                    currentPickupSpot = threeStarPickups[1];
                    currentOrderScreen = threeStarOrderScreens[1];
                    gps.target = threeStarPickups[1].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 2:
                    threeStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    threeStarOrderScreens[2].SetActive(true);
                    orderAudio.OrderStartedAudio(3, 2);
                    threeStarPickups[2].SetActive(true);
                    currentPickupSpot = threeStarPickups[2];
                    currentOrderScreen = threeStarOrderScreens[2];
                    gps.target = threeStarPickups[2].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 3:
                    threeStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    threeStarOrderScreens[3].SetActive(true);
                    orderAudio.OrderStartedAudio(3, 3);
                    threeStarPickups[3].SetActive(true);
                    currentPickupSpot = threeStarPickups[3];
                    currentOrderScreen = threeStarOrderScreens[3];
                    gps.target = threeStarPickups[3].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
                case 4:
                    threeStarOrderStarted = false;
                    ordermenu.enabled = false;
                    buttonOne.SetActive(false);
                    buttonTwo.SetActive(false);
                    buttonThree.SetActive(false);
                    threeStarOrderScreens[4].SetActive(true);
                    orderAudio.OrderStartedAudio(3, 4);
                    threeStarPickups[4].SetActive(true);
                    currentPickupSpot = threeStarPickups[4];
                    currentOrderScreen = threeStarOrderScreens[4];
                    gps.target = threeStarPickups[4].transform.Find("Target").transform;
                    if (!startTimer)
                        SetTimer(timeToPickup);
                    break;
            }
        }
    }

    void OrderPickedUp()
    {
        if (isPickedup)
        {
            //TurnEnemiesOn();

            if (oneStar)
            {
                switch (currentOneStarOrder)
                {
                    case 0:
                        
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[0].SetActive(false);
                        oneStarDropoffs[0].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[0];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[0].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                    case 1:
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[1].SetActive(false);
                        oneStarDropoffs[1].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[1];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[1].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                    case 2:
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[2].SetActive(false);
                        oneStarDropoffs[2].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[2];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[2].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                    case 3:
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[3].SetActive(false);
                        oneStarDropoffs[3].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[3];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[3].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                    case 4:
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[4].SetActive(false);
                        oneStarDropoffs[4].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[4];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[4].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                    case 5:
                        foodCanDamage = true;
                        isPickedup = false;
                        oneStarPickups[5].SetActive(false);
                        oneStarDropoffs[5].SetActive(true);
                        currentDropoffSpot = oneStarDropoffs[5];
                        foodDamageHud.SetActive(true);
                        gps.target = oneStarDropoffs[5].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(oneStarOrderTimer);
                        oneStarOrderStarted = false;
                        break;
                }
            }
            if (twoStar)
            {
                switch (currentTwoStarOrder)
                {
                    case 0:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[0].SetActive(false);
                        twoStarDropoffs[0].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[0];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[0].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                    case 1:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[1].SetActive(false);
                        twoStarDropoffs[1].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[1];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[1].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                    case 2:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[2].SetActive(false);
                        twoStarDropoffs[2].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[2];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[2].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                    case 3:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[3].SetActive(false);
                        twoStarDropoffs[3].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[3];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[3].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                    case 4:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[4].SetActive(false);
                        twoStarDropoffs[4].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[4];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[4].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                    case 5:
                        foodCanDamage = true;
                        isPickedup = false;
                        twoStarPickups[5].SetActive(false);
                        twoStarDropoffs[5].SetActive(true);
                        currentDropoffSpot = twoStarDropoffs[5];
                        foodDamageHud.SetActive(true);
                        gps.target = twoStarDropoffs[5].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(twoStarOrderTimer);
                        twoStarOrderStarted = false;
                        break;
                }
            }
            if (threeStar)
            {
                switch (currentThreeStarOrder)
                {
                    case 0:
                        foodCanDamage = true;
                        isPickedup = false;
                        threeStarPickups[0].SetActive(false);
                        threeStarDropoffs[0].SetActive(true);
                        currentDropoffSpot = threeStarDropoffs[0];
                        foodDamageHud.SetActive(true);
                        gps.target = threeStarDropoffs[0].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(threeStarOrderTimer);
                        threeStarOrderStarted = false;
                        break;
                    case 1:
                        foodCanDamage = true;
                        isPickedup = false;
                        threeStarPickups[1].SetActive(false);
                        threeStarDropoffs[1].SetActive(true);
                        currentDropoffSpot = threeStarDropoffs[1];
                        foodDamageHud.SetActive(true);
                        gps.target = threeStarDropoffs[1].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(threeStarOrderTimer);
                        threeStarOrderStarted = false;
                        break;
                    case 2:
                        foodCanDamage = true;
                        isPickedup = false;
                        threeStarPickups[2].SetActive(false);
                        threeStarDropoffs[2].SetActive(true);
                        currentDropoffSpot = threeStarDropoffs[2];
                        foodDamageHud.SetActive(true);
                        gps.target = threeStarDropoffs[2].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(threeStarOrderTimer);
                        threeStarOrderStarted = false;
                        break;
                    case 3:
                        foodCanDamage = true;
                        isPickedup = false;
                        threeStarPickups[3].SetActive(false);
                        threeStarDropoffs[3].SetActive(true);
                        currentDropoffSpot = threeStarDropoffs[3];
                        foodDamageHud.SetActive(true);
                        gps.target = threeStarDropoffs[3].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(threeStarOrderTimer);
                        threeStarOrderStarted = false;
                        break;
                    case 4:
                        foodCanDamage = true;
                        isPickedup = false;
                        threeStarPickups[4].SetActive(false);
                        threeStarDropoffs[4].SetActive(true);
                        currentDropoffSpot = threeStarDropoffs[4];
                        foodDamageHud.SetActive(true);
                        gps.target = threeStarDropoffs[4].transform.Find("Target").transform;
                        startTimer = false;
                        if (!startTimer)
                            SetTimer(threeStarOrderTimer);
                        threeStarOrderStarted = false;
                        break;
                }
            }
        }
    }

    //If time ran out turn off any pickup and drop off spots and reset all of the order variables. Such as food damage, timer, and payment.
    private void TimesUp()
    {
        if (orderTimer.timeRemaining <= 0)
        {
            if (oneStar)
            {
                if (currentPickupSpot != null)
                    currentPickupSpot.SetActive(false);

                if (currentDropoffSpot != null)
                    currentDropoffSpot.SetActive(false);

                if (currentOrderScreen != null)
                    currentOrderScreen.SetActive(false);

                orderAudio.OrderTimeUpAudio(1, currentOneStarOrder);
                oneStarOrderStarted = false;
                isPickedup = false;
                foodCanDamage = false;
                startTimer = false;
                deliveryFail.Play();
                timer.SetActive(false);
                foodDamageHud.SetActive(false);
                ordermenu.enabled = true;
                buttonOne.SetActive(true);
                buttonTwo.SetActive(true);
                buttonThree.SetActive(true);
                oneStar = false;
                ResetFoodDamageBar();
                ResetPayment();
                ResetGPS();
            }
            if (twoStar)
            {
                if (currentPickupSpot != null)
                    currentPickupSpot.SetActive(false);

                if (currentDropoffSpot != null)
                    currentDropoffSpot.SetActive(false);

                if (currentOrderScreen != null)
                    currentOrderScreen.SetActive(false);

                orderAudio.OrderTimeUpAudio(2, currentTwoStarOrder);
                twoStarOrderStarted = false;
                isPickedup = false;
                foodCanDamage = false;
                startTimer = false;
                deliveryFail.Play();
                timer.SetActive(false);
                foodDamageHud.SetActive(false);
                ordermenu.enabled = true;
                buttonOne.SetActive(true);
                buttonTwo.SetActive(true);
                buttonThree.SetActive(true);
                twoStar = false;
                ResetFoodDamageBar();
                ResetPayment();
                ResetGPS();
            }
            if (threeStar)
            {
                if (currentPickupSpot != null)
                    currentPickupSpot.SetActive(false);

                if (currentDropoffSpot != null)
                    currentDropoffSpot.SetActive(false);

                if (currentOrderScreen != null)
                    currentOrderScreen.SetActive(false);

                orderAudio.OrderTimeUpAudio(3, currentThreeStarOrder);
                threeStarOrderStarted = false;
                isPickedup = false;
                foodCanDamage = false;
                startTimer = false;
                deliveryFail.Play();
                timer.SetActive(false);
                foodDamageHud.SetActive(false);
                ordermenu.enabled = true;
                buttonOne.SetActive(true);
                buttonTwo.SetActive(true);
                buttonThree.SetActive(true);
                threeStar = false;
                ResetFoodDamageBar();
                ResetPayment();
                ResetGPS();
            }
        }
    }

    //Used when the player completes an order. Turns off order screen, triggers payment, and order finished audio.
    public IEnumerator DropoffOrder()
    {
        if (oneStar)
        {
            yield return new WaitForSeconds(dropoffWaitTime);
            orderAudio.OrderFinishedAudio(1, currentOneStarOrder);
            DropoffSucess(currentOneStarPayment);
            oneStar = false;
            currentOneStarOrder++;
            foreach (GameObject go in oneStarOrderScreens)
            {
                go.SetActive(false);
            }
        }
        if (twoStar)
        {
            yield return new WaitForSeconds(dropoffWaitTime);
            orderAudio.OrderFinishedAudio(2, currentTwoStarOrder);
            DropoffSucess(currentTwoStarPayment);
            twoStar = false;
            currentTwoStarOrder++;
            foreach (GameObject go in twoStarOrderScreens)
            {
                go.SetActive(false);
            }
        }
        if (threeStar)
        {
            yield return new WaitForSeconds(dropoffWaitTime);
            orderAudio.OrderFinishedAudio(3, currentThreeStarOrder);
            DropoffSucess(currentThreeStarPayment);
            threeStar = false;
            currentThreeStarOrder++;
            foreach (GameObject go in threeStarOrderScreens)
            {
                go.SetActive(false);
            }
        }
    }

    //Resets all order variables and apply's all payments.
    void DropoffSucess(float payment)
    {
        currentDropoffSpot.SetActive(false);
        ordermenu.enabled = true;
        buttonOne.SetActive(true);
        buttonTwo.SetActive(true);
        buttonThree.SetActive(true);
        foodCanDamage = false;
        startTimer = false;
        isPickedup = false;
        deliverySuccess.Play();
        CalculatePayment();
        dropoffPs.Play();
        timer.SetActive(false);
        foodDamageHud.SetActive(false);
        money.AddMoney(payment);
        respectPoints.AddPoints(currentRpToAdd);
        ResetPayment();
        ResetFoodDamageBar();
        ResetGPS();
        if (currentOneStarOrder >= 5)
            currentOneStarOrder = 0;
        if (currentTwoStarOrder >= 5)
            currentTwoStarOrder = 0;
        if (currentThreeStarOrder >= 4)
            currentThreeStarOrder = 0;
    }

    //Resets the current payment to the max amount as well as respect points and food damage.
    void ResetPayment()
    {
        currentOneStarPayment = oneStarPayment;
        currentTwoStarPayment = twoStarPayment;
        currentThreeStarPayment = threeStarPayment;
        foodDamage.m_foodQuality = 100f;
        currentRpToAdd = rpToAdd;
    }

    //Determines how much the player gets paid for the delivery depending on how damaged the food is.
    void CalculatePayment()
    {
        currentOneStarPayment = Mathf.Clamp(currentOneStarPayment, 0, oneStarPayment);
        currentTwoStarPayment = Mathf.Clamp(currentTwoStarPayment, 0, twoStarPayment);
        currentThreeStarPayment = Mathf.Clamp(currentThreeStarPayment, 0, threeStarPayment);
        rpToAdd = Mathf.Clamp(currentRpToAdd, 0, rpToAdd);


        if (foodDamage.m_foodQuality > 75)
        {
            currentOneStarPayment = oneStarPayment;
            currentTwoStarPayment = twoStarPayment;
            currentThreeStarPayment = threeStarPayment;
            currentRpToAdd = rpToAdd;
        }
        if (foodDamage.m_foodQuality < 75f && foodDamage.m_foodQuality > 50f)
        {
            currentOneStarPayment *= 0.75f;
            currentTwoStarPayment *= 0.75f;
            currentThreeStarPayment *= 0.75f;
            currentRpToAdd *= 0.75f;
            currentRpToAdd = Mathf.Round(currentRpToAdd);
        }
        if (foodDamage.m_foodQuality < 50f && foodDamage.m_foodQuality > 25f)
        {
            currentOneStarPayment *= 0.50f;
            currentTwoStarPayment *= 0.50f;
            currentThreeStarPayment *= 0.50f;
            currentRpToAdd *= 0.50f;
            currentRpToAdd = Mathf.Round(currentRpToAdd);
        }
        if (foodDamage.m_foodQuality < 25f)
        {
            currentOneStarPayment *= 0.25f;
            currentTwoStarPayment *= 0.25f;
            currentThreeStarPayment *= 0.25f;
            currentRpToAdd *= 0.25f;
            currentRpToAdd = Mathf.Round(currentRpToAdd);
        }

        Debug.Log(currentOneStarPayment + currentRpToAdd);
    }

    //Sets food damage to max amount.
    void ResetFoodDamageBar()
    {
        foodDamageBar.SetFoodDamage(100f);
    }

    //Clears the line renderer so it stops drawing a line.
    void ResetGPS()
    {
        gps.target = null;
        gps.lineRenderer.positionCount = 0;
    }

    //Starts the timer.
    void SetTimer(float time)
    {
        startTimer = true;
        timer.SetActive(true);
        orderTimer.timeRemaining = time;
        orderTimer.timerIsRunning = true;
    }
}

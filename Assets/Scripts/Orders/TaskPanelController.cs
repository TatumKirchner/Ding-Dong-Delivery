using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskPanelController : MonoBehaviour
{
    private Animator m_animator;
    public GameObject panel;
    private GameplayControls playerControls;
    [SerializeField] private GameObject buttonOne;
    public bool panelActive = false;
    [SerializeField] IsPaused isPaused;
    private OrderManager orderManager;
    [SerializeField] DrivingAroundMusic drivingMusic;
    [SerializeField] private Button[] taskPanelButtons;

    private void Awake()
    {
        playerControls = new GameplayControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        orderManager = FindObjectOfType<OrderManager>();
        ButtonsInteractable();
        playerControls.Gameplay.OrderMenu.performed += _ => Open();
    }

    //Plays the animation to open the phone
    void Open()
    {
        if (panel != null)
        {
            if (m_animator != null && !isPaused.isPaused)
            {
                bool isOpen = m_animator.GetBool("PanelActive");

                m_animator.SetBool("PanelActive", !isOpen);

                ButtonsInteractable();

                if (!isOpen)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(buttonOne);
                    panelActive = true;
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    panelActive = false;
                }
            }
        }
    }

    //If the panel is not on screen make the button non-intractable.
    public void ButtonsInteractable()
    {
        if (!m_animator.GetBool("PanelActive"))
        {
            foreach (Button button in taskPanelButtons)
            {
                button.interactable = false;
            }
        }
        else
        {
            foreach (Button button in taskPanelButtons)
            {
                button.interactable = true;
            }
        }
    }

    //Methods for the button on the phone. Start their respective orders with a call to the order manager.
    public void OneStar()
    {
        if (!orderManager.oneStar && !orderManager.twoStar && !orderManager.threeStar)
            orderManager.oneStarOrderStarted = true;
    }

    public void TwoStar()
    {
        if (!orderManager.oneStar && !orderManager.twoStar && !orderManager.threeStar)
            orderManager.twoStarOrderStarted = true;
    }

    public void ThreeStar()
    {
        if (!orderManager.oneStar && !orderManager.twoStar && !orderManager.threeStar)
            orderManager.threeStarOrderStarted = true;
    }
}

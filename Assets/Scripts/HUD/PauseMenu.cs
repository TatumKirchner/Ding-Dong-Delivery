using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    #region Parent Panels
    [Header("Parent Panels")]
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject creditsButtonsPanel;
    [SerializeField] private GameObject gamepadPanel;
    [SerializeField] private GameObject keyboardPanel;
    [SerializeField] private GameObject verificationPanel;
    [SerializeField] private GameObject TutorialPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject videoPanel;
    #endregion

    #region Credits
    [Header("Credits")]
    [SerializeField] private GameObject tatum;
    [SerializeField] private GameObject darius;
    [SerializeField] private GameObject rudy;
    [SerializeField] private GameObject joey;
    [SerializeField] private GameObject shanhao;
    [SerializeField] private GameObject robert;
    [SerializeField] private GameObject connor;
    [SerializeField] private GameObject wyatt;
    [SerializeField] private GameObject specialThanks;
    [SerializeField] private GameObject musicCredits;
    #endregion

    private IsPaused pauseControler;
    private GameplayControls playerControls;
    public bool pauseOpen = false;
    [SerializeField] private Shop shop;
    [SerializeField] private StartMenu startMenu;
    [SerializeField] private Button[] pauseButtons;

    #region Videos
    [Header("Videos")]
    [SerializeField] private VideoClip delivery;
    [SerializeField] private VideoClip damage;
    [SerializeField] private VideoClip upgrades;
    [SerializeField] private VideoClip weapons;
    [SerializeField] private VideoClip respect;
    [SerializeField] private VideoClip tips;
    [SerializeField] private VideoPlayer player;
    [SerializeField] private AudioSource source;
    private bool videoPlaying = false;
    #endregion

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
        pauseControler = GameObject.Find("Game Manager").GetComponent<IsPaused>();
        playerControls.Gameplay.Pause.performed += _ => OpenPauseMenu();
        player.SetTargetAudioSource(0, source);
        source.ignoreListenerPause = true;
    }

    private void Update()
    {
        //If the pause menu is open, pause the game.
        if (pausePanel.activeInHierarchy)
        {
            pauseControler.isPaused = true;
        }

        //If the player paused
        if (videoPlaying)
        {
            if (!player.isPlaying && pauseOpen)
            {
                VideoBack();
                videoPlaying = false;
            }
        }
    }

    /*
     * Switches between the pause menu being open and closed
     */
    void OpenPauseMenu()
    {
        if (!shop.shopActive && !startMenu.startPanelOpen)
        {
            pauseOpen = !pauseOpen;
            if (pauseOpen)
            {
                pauseControler.isPaused = true;
                pausePanel.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
            }
            else
            {
                player.Stop();
                pauseControler.isPaused = false;
                pausePanel.SetActive(false);
                controlPanel.SetActive(false);
                creditsButtonsPanel.SetActive(false);
                gamepadPanel.SetActive(false);
                keyboardPanel.SetActive(false);
                optionsPanel.SetActive(false);
                tatum.SetActive(false);
                darius.SetActive(false);
                rudy.SetActive(false);
                joey.SetActive(false);
                shanhao.SetActive(false);
                robert.SetActive(false);
                connor.SetActive(false);
                wyatt.SetActive(false);
                specialThanks.SetActive(false);
                verificationPanel.SetActive(false);
                videoPanel.SetActive(false);
                TutorialPanel.SetActive(false);
                videoPlaying = false;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    /*
     * All of the following methods are for the pause menu buttons
     */

    public void Resume()
    {
        pausePanel.SetActive(false);
        pauseOpen = false;
        pauseControler.isPaused = false;
    }

    public void Options()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        optionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsPanel.transform.GetChild(2).gameObject);
    }

    public void Controls()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        controlPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlPanel.transform.GetChild(1).gameObject);
    }

    public void Exit()
    {
        OpenVerificationPanel();
    }

    public void FullScreen()
    {
        Screen.fullScreen = true;
    }

    public void WindowedMode()
    {
        Screen.fullScreen = false;
    }

    public void OptionsMenuBack()
    {
        if (startMenu.startPanelOpen)
        {
            startPanel.SetActive(true);
            pausePanel.SetActive(false);
            optionsPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            optionsPanel.SetActive(false);
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
        }
    }

    public void ControlsMenuBack()
    {
        if (startMenu.startPanelOpen)
        {
            controlPanel.SetActive(false);
            startPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            controlPanel.SetActive(false);
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
        }
    }

    public void Credits()
    {
        if (startMenu.startPanelOpen)
        {
            startPanel.SetActive(false);
            creditsButtonsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            pausePanel.SetActive(false);
            creditsButtonsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsButtonsPanel.transform.GetChild(1).gameObject);
        }
    }

    public void Gamepad()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        gamepadPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gamepadPanel.transform.GetChild(0).gameObject);
    }

    public void Keyboard()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        keyboardPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(keyboardPanel.transform.GetChild(0).gameObject);
    }

    public void GamepadMouseControlsBack()
    {
        if (startMenu.startPanelOpen)
        {
            controlPanel.SetActive(false);
            gamepadPanel.SetActive(false);
            keyboardPanel.SetActive(false);
            startPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            controlPanel.SetActive(false);
            gamepadPanel.SetActive(false);
            keyboardPanel.SetActive(false);
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
        }
    }

    public void Tatum()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        tatum.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(tatum.transform.GetChild(1).gameObject);
    }

    public void Darius()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        darius.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(darius.transform.GetChild(1).gameObject);
    }

    public void Rudy()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        rudy.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(rudy.transform.GetChild(1).gameObject);
    }

    public void Joey()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        joey.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(joey.transform.GetChild(1).gameObject);
    }

    public void Shanhao()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        shanhao.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(shanhao.transform.GetChild(1).gameObject);
    }

    public void Robert()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        robert.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(robert.transform.GetChild(1).gameObject);
    }

    public void Connor()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        connor.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(connor.transform.GetChild(1).gameObject);
    }

    public void Wyatt()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        wyatt.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(wyatt.transform.GetChild(1).gameObject);
    }

    public void SpecialThanks()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        specialThanks.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(specialThanks.transform.GetChild(1).gameObject);
    }

    public void MusicCredits()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        musicCredits.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(musicCredits.transform.GetChild(1).gameObject);
    }

    public void CreditsBack()
    {
        if (startMenu.startPanelOpen)
        {
            creditsButtonsPanel.SetActive(false);
            startPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
        }
        else
        {
            creditsButtonsPanel.SetActive(false);
            pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
        }
    }

    public void CreditsPeoplePanelBack()
    {
        tatum.SetActive(false);
        darius.SetActive(false);
        rudy.SetActive(false);
        joey.SetActive(false);
        shanhao.SetActive(false);
        robert.SetActive(false);
        connor.SetActive(false);
        wyatt.SetActive(false);
        musicCredits.SetActive(false);
        specialThanks.SetActive(false);
        creditsButtonsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsButtonsPanel.transform.GetChild(1).gameObject);
    }

    public void OpenVerificationPanel()
    {
        foreach (Button button in pauseButtons)
        {
            button.interactable = false;
        }
        verificationPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(verificationPanel.transform.GetChild(1).gameObject);
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        foreach (Button button in pauseButtons)
        {
            button.interactable = true;
        }
        verificationPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
    }

    public void OpenTut()
    {
        TutorialPanel.SetActive(true);
        pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(TutorialPanel.transform.GetChild(1).gameObject);
    }

    public void TutBack()
    {
        TutorialPanel.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
    }

    public void DeliveryTut()
    {
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = delivery;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
    }

    public void DamageTut()
    {
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = damage;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
    }

    public void UpgradeTut()
    {
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = upgrades;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
    }

    public void WeaponsTut()
    {
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = weapons;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
    }

    public void RespectTut()
    {
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = respect;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
    }

    public void TipsTut()
    {
        player.Prepare();
        videoPanel.SetActive(true);
        TutorialPanel.SetActive(false);
        player.clip = tips;
        player.Play();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(videoPanel.transform.GetChild(2).gameObject);
        videoPlaying = true;
        
    }

    public void VideoBack()
    {
        player.Stop();
        videoPanel.SetActive(false);
        TutorialPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(TutorialPanel.transform.GetChild(2).gameObject);
    }
}

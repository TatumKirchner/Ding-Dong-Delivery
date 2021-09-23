using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private IsPaused paused;
    [SerializeField] private CarSwitch carSwitch;
    [SerializeField] private Animator phoneAnimator;

    public bool startPanelOpen = true;

    public AudioSource source;
    public AudioClip menuMusicClip;
    public AudioClip phoneCallClip;

    //Play start menu music, pause the game, and set the selected object for controllers.
    private void Start()
    {
        source.clip = menuMusicClip;
        source.ignoreListenerPause = true;
        source.Play();
        paused.isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startPanel.transform.GetChild(0).gameObject);
    }

    /* Method called when the Start Game button is pressed.
     * Shut down the start menu and music. Open up the Phone panel for intro and play intro clip*/
    public void StartGame()
    {
        carSwitch.Upgrade();
        source.clip = phoneCallClip;
        source.ignoreListenerPause = false;
        startPanel.SetActive(false);
        paused.isPaused = false;
        startPanelOpen = false;
        phoneAnimator.SetBool("PanelActive", true);
        source.Play();
    }
}

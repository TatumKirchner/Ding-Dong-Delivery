using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrivingAroundMusic : MonoBehaviour
{
    [HideInInspector] public AudioSource drivingMusic;
    private OrderAudioManager orderAudio;
    private IsPaused paused;
    [SerializeField] private AudioClip[] clips;
    private int nextClip;
    [HideInInspector] public bool introPlayed = false;
    [SerializeField] private GameObject taskPanelButtonOne;
    [SerializeField] private TaskPanelController panelController;
    private CarSwitch carSwitch;

    private void Start()
    {
        drivingMusic = GameObject.Find("CM FreeLook1/Audio Source/Music").GetComponent<AudioSource>();
        carSwitch = GetComponent<CarSwitch>();
        orderAudio = GetComponent<OrderAudioManager>();
        paused = GetComponent<IsPaused>();
        drivingMusic.PlayDelayed(2);
    }

    //Pick a random clip
    void PickAClip()
    {
        nextClip = Random.Range(0, 2);
    }

    private void LateUpdate()
    {
        //Used for the intro only. Once the intro clip finishes music will play and phone buttons are unlocked.
        if (!drivingMusic.isPlaying && !introPlayed && !AudioListener.pause)
        {
            introPlayed = true;
            panelController.ButtonsInteractable();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(taskPanelButtonOne);
            //Called to make sure the player car is correct.
            carSwitch.Upgrade();

        }

        //Once the active clip is done set it to null
        if (!drivingMusic.isPlaying && !orderAudio.audioSource.isPlaying && !paused.isPaused)
        {
            drivingMusic.clip = null;
        }

        //Once the clip is null pick a new one and play it.
        if (!orderAudio.audioSource.isPlaying && !drivingMusic.isPlaying && drivingMusic.clip == null)
        {
            PickAClip();
            drivingMusic.clip = clips[nextClip];
            drivingMusic.Play();
        }

        
    }
}

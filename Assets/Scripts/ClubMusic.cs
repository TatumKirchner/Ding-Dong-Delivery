using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubMusic : MonoBehaviour
{
    private AudioSource source;
    private IsPaused pauseManager;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        pauseManager = FindObjectOfType<IsPaused>();
    }

    private void Update()
    {
        if (pauseManager.isPaused)
        {
            source.Pause();
        }
        else
        {
            source.UnPause();
        }
    }
}

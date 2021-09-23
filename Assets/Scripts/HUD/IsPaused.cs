using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPaused : MonoBehaviour
{
    public bool isPaused = false;

    /*
     * When the game is paused stop time, show the cursor, and pause the audio. Do the opposite when unpaused.
     */
    void Update()
    {
        if (isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            AudioListener.pause = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

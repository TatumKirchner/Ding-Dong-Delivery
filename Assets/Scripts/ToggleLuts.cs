using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ToggleLuts : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    //When the player enters the water turn the weight of the global volume off so the underwater lut will be applied instead.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            globalVolume.weight = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            globalVolume.weight = 1;
        }
    }
}

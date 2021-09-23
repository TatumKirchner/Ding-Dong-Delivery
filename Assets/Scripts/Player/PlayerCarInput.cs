using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarInput : MonoBehaviour
{
    private PlayerCarController m_Car; // the car controller we want to use
    private GameplayControls playerControls;


    private void Awake()
    {
        m_Car = GetComponent<PlayerCarController>();
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

    //Read the input from player controls and pass that to the move method.
    private void FixedUpdate()
    {
        float vInput = playerControls.Gameplay.Drive.ReadValue<float>();
        float h = playerControls.Gameplay.Turn.ReadValue<float>();
        float handbrake = playerControls.Gameplay.Handbreak.ReadValue<float>();
        
        m_Car.Move(h, vInput, vInput, handbrake);
    }
}

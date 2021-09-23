using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespectPoints : MonoBehaviour
{
    [SerializeField] private Text text;
    public float points = 0f;

    /*
     * Todo
     * Move the text being updated to an event.
     */
    void Update()
    {
        text.text = "Respect " + points;
    }

    //Called to add Points to the player
    public void AddPoints(float rPoints)
    {
        points += rPoints;
    }

    //Called to remove point from the player.
    public void RemovePoints(float rPoints)
    {
        points -= rPoints;
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> balls; // List of all BallCollision scripts
    public TMP_Text strokeCounterDisplay; // Reference to the Text element on the screen

    private int totalStrokes = 0;

    // Update the UI with the total stroke count
    public void UpdateStrokeCount()
    {
        totalStrokes++;

        // Update the UI text
        strokeCounterDisplay.text = "Total Strokes: " + totalStrokes;
    }
}

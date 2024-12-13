using TMPro;
using UnityEngine;

public class ScreenCounterDisplay : MonoBehaviour
{
    public TMP_Text counterText; // Reference to the Text element
    public GameObject ball1;           // Reference to the Ball script
    public GameObject ball2;
    public GameObject ball3;
    public GameObject ball4;
    public GameObject ball5;
    public

    void Update()
    {
        if (counterText != null)
        {
            counterText.text = "Hits: " + ball1.getStrokeCount() ;
        }
    }
}

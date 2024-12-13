using UnityEngine;

public class BrokenDoorPanel : MonoBehaviour
{
    public float moveAmplitude = 0.1f; // Maximum movement distance (in units)
    public float speed = 2f; // Speed of the movement

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the door panel
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Calculate the oscillation offset using a sine wave
        float oscillation = Mathf.Sin(Time.time * speed) * moveAmplitude;

        // Apply the oscillation to the Y-axis
        transform.localPosition = initialPosition + new Vector3(0, oscillation, 0);
    }
}

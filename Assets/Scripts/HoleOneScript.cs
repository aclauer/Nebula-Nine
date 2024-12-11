using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleOneScript : HoleScript
{
    // ball should be attached in inspector?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfHole"))
        {
            Debug.Log("Ball is in the hole!");

            // Disable the ball and play sound
            ballSound.Play();
            ball.SetActive(false);

            // Open the next door

        }
    }
}

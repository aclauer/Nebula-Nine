using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HoleScript : MonoBehaviour
{
    public GameObject ball;

    private const float SPEED_FACTOR = 1.5f;
    private const float SPIN_FACTOR = 0.2f;

    protected AudioSource ballSound;

    // Start is called before the first frame update
    void Start()
    {
        if (ball != null) {
            ballSound = ball.GetComponent<AudioSource>();
        } else {
            Debug.Log("** Ball is null, cannot get sound.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GolfClub"))
        {
            Debug.Log("Detected a golf ball collision");

            Rigidbody ballRigidBody = GetComponent<Rigidbody>();

            float clubSpeed = other.relativeVelocity.magnitude;
            float forceMagnitude = clubSpeed * SPEED_FACTOR;

            Vector3 forceDir = other.contacts[0].normal;

            ballRigidBody.AddForce(-forceDir * forceMagnitude, ForceMode.Impulse);

            Vector3 spinDir = Vector3.Cross(forceDir, Vector3.up);
            float spinMagnitude = clubSpeed * SPIN_FACTOR;

            ballRigidBody.AddTorque(spinDir * spinMagnitude, ForceMode.Impulse);

            // Turn off collider
        }

        // Play sound for all collisions
        ballSound.Play();
    }

    protected abstract void OnTriggerEnter(Collider other);
}

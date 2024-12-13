using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination; // Where the player will be teleported

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the destination
            other.transform.position = teleportDestination.position;

            // Optional: Reset player velocity if using a Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}

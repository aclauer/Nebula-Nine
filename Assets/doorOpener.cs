using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class doorOpener : MonoBehaviour
{
    public GameObject player;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 position = player.transform.localPosition;

        if (player == null)
        {
            Debug.Log("**Player is null**");
        }

        Debug.Log("Player: " + player);
        Debug.Log("Player transform: " + player.transform);

        Debug.Log("Player position: " + player.transform.position);
        Debug.Log("Door position: " + transform.position);

        Vector3 relativePosition = player.transform.position - transform.position;
        Debug.Log("Relative position: " + relativePosition + " (" + relativePosition.magnitude + ")");

        if (relativePosition.magnitude < 3.0)
        {
            animator.SetBool("character_nearby", true);
        } else
        {
            animator.SetBool("character_nearby", false);
        }  
    }
}

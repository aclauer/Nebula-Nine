using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOneHandler : MonoBehaviour
{
    public Animator animator;
    public AudioSource doorOpenSound = null;

    // Start is called before the first frame update
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        //Debug.Log("Opening the door on start");
        //animator.SetBool("character_nearby", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonSelected()
    {
        Debug.Log("Callee function (door should be opening)");
        animator.SetBool("character_nearby", true);
        doorOpenSound.Play(); 
        
    }
}

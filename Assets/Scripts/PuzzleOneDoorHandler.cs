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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonSelected()
    {
        animator.SetBool("Open", true);
        doorOpenSound.Play();
    }
}

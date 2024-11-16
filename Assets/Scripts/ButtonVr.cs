using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Button trigger enter: " + button);
        if (!isPressed)
        {
            button.transform.localPosition += new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Button trigger exit: " + button);
        if (other.gameObject == presser)
        {
            button.transform.localPosition -= new Vector3(0, 0.003f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void openDoor1()
    {
     //do stuff
    }
}

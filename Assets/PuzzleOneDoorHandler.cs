using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOneHandler : MonoBehaviour
{
    public GameObject doorButton;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonSelected()
    {
        Debug.Log("We have pressed the button.");
        animator.SetBool("player_nearby", true);
    }
}

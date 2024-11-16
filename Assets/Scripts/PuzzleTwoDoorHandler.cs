using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleTwoDoorHandler : MonoBehaviour
{

    public Animator animator;

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
        animator.SetBool("character_nearby", true);
    }
}

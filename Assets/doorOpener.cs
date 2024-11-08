using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class doorOpener : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        while (true)
        {
            animator.SetBool("character_nearby", true);

            Thread.Sleep(2000);

            animator.SetBool("character_nearby", false);

            Thread.Sleep(2000);
        }
        
    }
}

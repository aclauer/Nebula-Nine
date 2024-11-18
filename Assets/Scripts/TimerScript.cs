using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float timer;
    public GameObject spaceShip;
    public GameObject teleport;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            spaceShip.SetActive( false );
            teleport.SetActive( false );
        }
    }
}

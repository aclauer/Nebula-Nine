using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHoleHandler : MonoBehaviour
{
    GameObject[] holes;
    GameObject currentHole;
    int holeIndex = 0;
    public int totalStrokes = 0;


    // Start is called before the first frame update
    void Start()
    {
        holes = GameObject.FindGameObjectsWithTag("level");
        currentHole = holes[holeIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextHole()
    {
        holeIndex++;
        currentHole = holes[holeIndex];
    }
}

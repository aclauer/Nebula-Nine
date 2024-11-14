using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleThreeDoorHandler : MonoBehaviour
{
    public GameObject club;

    private bool puzzleActive = true;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        club.SetActive(puzzleActive);
    }

    public void setPuzzleStatus(bool status) {
        puzzleActive = status;
    }
}

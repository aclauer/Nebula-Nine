using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab; 
    public Transform spawnArea; 
    public int targetCount = 30;

    private GameObject[] spawnedTargets;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SpawnTargets()
    {
        if (spawnedTargets == null){
            spawnedTargets = new GameObject[targetCount];
            for (int i = 0; i < targetCount; i++)
            {
                Vector3 randomPosition = new Vector3(
                    Random.Range(spawnArea.position.x -2, spawnArea.position.x + 2),
                    Random.Range(spawnArea.position.y + 1, spawnArea.position.y + 3 ), 
                    Random.Range(spawnArea.position.z - 3, spawnArea.position.z + 1)
                );

                GameObject newTarget = Instantiate(targetPrefab, randomPosition, Quaternion.identity, spawnArea);
                spawnedTargets[i] = newTarget;
            }
        }
    }
}


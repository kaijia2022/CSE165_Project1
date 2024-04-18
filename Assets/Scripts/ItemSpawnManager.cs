using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Array to hold your prefabs
    public Transform spawnPoint;        // The location where you want to spawn the objects

    // Method to spawn the prefab at the specified spawn point
    public void SpawnObject(int index)
    {
        if (index >= 0 && index < prefabsToSpawn.Length)
        {
            // Instantiate the prefab at the spawn point's position and rotation
            Instantiate(prefabsToSpawn[index], spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Index out of range, make sure it's correct!");
        }
    }
}

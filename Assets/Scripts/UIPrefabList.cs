using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrefabList : MonoBehaviour
{
    public GameObject buttonPrefab;  // Assign in inspector
    public List<GameObject> prefabList;  // List of all prefabs to display

    void Start()
    {
        foreach (var prefab in prefabList)
        {
            // Instantiate button for each prefab
            GameObject buttonObj = Instantiate(buttonPrefab, transform);
            buttonObj.GetComponentInChildren<Text>().text = prefab.name;  // Set button text to prefab's name

            // Add click event
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SpawnObject(prefab));
        }
    }

    void SpawnObject(GameObject prefab)
    {
        // Instantiate the prefab at the desired location
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

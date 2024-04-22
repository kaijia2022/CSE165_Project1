using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPrefabList : MonoBehaviour
{
    public GameObject buttonPrefab;  // Assign in inspector
    public List<GameObject> prefabList;  // List of all prefabs to display
    public float buttonSpacingX = 100f; //horizontal sapce between each button
    public float buttonSpacingY = 50f; //vertical sapce between each button
    public float startPosX = -250;
    public float startPosY = 150;
    public float maxCountY = 5;

    void Start()
    {
        int i = 0;
        int j = 0;
        foreach (var prefab in prefabList)
        {

            // Instantiate button for each prefab
            GameObject buttonObj = Instantiate(buttonPrefab, transform);
            buttonObj.transform.localPosition = new Vector3(startPosX + j * buttonSpacingX, startPosY - i * buttonSpacingY, 0);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = prefab.name;  // Set button text to prefab's name

            // Add click event
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SpawnObject(prefab));
            Debug.Log(prefab.name);
            Debug.Log(buttonObj.GetComponentInChildren<TextMeshProUGUI>().text);
            i++;
            if (i == maxCountY)
            {
                i = 0;
                j++;
            }
        }
    }

    void SpawnObject(GameObject prefab)
    {
        // Instantiate the prefab at the desired location
        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}

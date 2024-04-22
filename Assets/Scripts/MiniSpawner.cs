using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpawner : MonoBehaviour
{
    public LayerMask layerMask; // Define the layer mask to filter which objects the raycast should interact with
    public LayerMask spawnLayer;
    public float maxInteractDistance = 30f;
    public GameObject[] prefabsToSpawn;
    private Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
    public Vector3 spawnOffset = new Vector3(-5f, 0.5f, -2f);
    public Transform parentTransform;
    void Update()
    {
        RaycastHit hit;
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
        {

            // Perform the raycast and check if it hits any object
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractDistance, layerMask))
            {
                // Get the GameObject that was hit
                GameObject hitObject = hit.collider.gameObject;

                foreach (var prefab in prefabsToSpawn)
                {
                    if (prefab.name == hitObject.transform.name)
                    {
                        spawnPosition = hitObject.transform.parent.parent.position + spawnOffset;
                        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        obj.transform.SetParent(parentTransform);
                        obj.transform.gameObject.layer = spawnLayer;
                        break;
                    }
                }
                
            }
        }

    }

}

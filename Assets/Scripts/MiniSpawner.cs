using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpawner : MonoBehaviour
{
    public LayerMask layerMask; // Define the layer mask to filter which objects the raycast should interact with
    public LayerMask spawnLayer;
    public float maxInteractDistance = 30f;
    public GameObject[] prefabsToSpawn;
    private Vector3 spawnPosition = new Vector3(-3f, 0.5f, -3f);
    //   public Vector3 spawnOffset = new Vector3(-5f, 0.5f, -2f);
    private Vector3 spawnOffset = new Vector3(-3f, 0.5f, -3f);
    public Transform parentTransform;

    public Color adjustColor = Color.white;
    public Color buttonPressed = Color.black; 
    private GameObject Adjuster;
    private bool adjust;
    public LayerMask adjustLayer;
    public float minX = -6.8f;
    public float minZ = -9.9f;
    public float maxX = 4.3f;
    public float maxZ = 4.3f;
    public float angleCount = 0f; 
    private Vector3 dir = new Vector3(0f, 0f, 0f); 

    void Start()
    {
        adjust = false;
    }

    void Update()
    {
        RaycastHit hit;
        RaycastHit hitButton;
        RaycastHit land; 

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hitButton, maxInteractDistance, adjustLayer))
            {
                GameObject hitObject = hitButton.collider.gameObject;
                Renderer renderer = hitObject.GetComponent<Renderer>();

                //setting on and off for the adjust mode
                if(hitObject.name == "AdjustSpawn")
                {
                    Adjuster = hitObject; 
                    if (adjust)
                    {
                        Adjuster.GetComponent<Renderer>().material.color = adjustColor;
                        adjust = false; 
                    } else
                    {
                        adjust = true;
                        Adjuster.GetComponent<Renderer>().material.color = buttonPressed; 
                    }
                }

                if (adjust)
                {
                    if (Physics.Raycast(transform.position, transform.forward, out land, maxInteractDistance, adjustLayer))
                    {
                        if (land.point.x > minX && land.point.z > minZ && land.point.x < maxX && land.point.z < maxZ)
                        {
                            spawnOffset = new Vector3(land.point.x, 0f, land.point.z);
                        }
                    }
                }

                if(hitObject.name == "SetAngle")
                {
                    dir = dir + new Vector3(0f, 0.5f, 0f);
                    angleCount = angleCount + 1f; 
                }

            }
        }

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
                        //spawnPosition = hitObject.transform.parent.parent.position + spawnOffset;
                        spawnPosition = new Vector3(0f, 0.5f, 0f) + spawnOffset;
                        GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        obj.transform.Rotate(dir, angleCount * 10f);
                        obj.transform.SetParent(parentTransform);
                        obj.transform.gameObject.layer = spawnLayer;
                        break;
                    }
                }
                
            }
        }

    }

}

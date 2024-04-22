using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public LayerMask teleportationLayer; // Layer mask for teleportation area
    public float teleportationRange = 20f; // Maximum teleportation range
    public float minX = -6.8f;
    public float minZ = -9.9f;
    public float maxX = 4.3f;
    public float maxZ = 4.3f;
    private bool isTeleporting = false; // Flag to indicate if teleportation is in progress

    private void Update()
    {
        // Check if teleportation action is triggered (e.g., button press)
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            // Perform raycast from controller to detect teleportation target
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, teleportationRange, teleportationLayer))
            {
                // Teleport the player to the hit point
                if (hit.point.x > minX && hit.point.z > minZ && hit.point.x < maxX && hit.point.z < maxZ)
                {
                    TeleportPlayer(new Vector3(hit.point.x, 1.7f, hit.point.z));
                    Debug.Log("teleported Player");
                }
                
            }
        }
    }

    private void TeleportPlayer(Vector3 targetPosition)
    {
        // Ensure teleportation is not already in progress
        if (!isTeleporting)
        {
            // Set flag to indicate teleportation in progress
            isTeleporting = true;

            // Teleport the player to the target position
            player.position = targetPosition;

            // Reset flag after teleportation is complete
            isTeleporting = false;
        }
    }
}

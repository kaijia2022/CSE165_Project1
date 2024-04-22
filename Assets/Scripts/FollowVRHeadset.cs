using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVRHeadset : MonoBehaviour
{
    public Transform vrCamera; // Assign this to the VR Camera in the inspector
    public float distanceInFront = 1.0f; // Distance in front of the camera

    void Start()
    {
        if (vrCamera != null)
        {
            // Set the position of the Canvas to be distanceInFront units directly in front of the camera
            transform.position = vrCamera.position + vrCamera.forward * distanceInFront;

            // Optionally, make the Canvas always face the camera
            transform.rotation = Quaternion.LookRotation(transform.position - vrCamera.position);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnMenuController: MonoBehaviour
{
    public GameObject canvas; 

    void Update()
    {
        // Check if the B button on the right controller is pressed
        if (Input.GetButtonDown("OculusButtonB"))
        {
            // Toggle the canvas visibility
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}

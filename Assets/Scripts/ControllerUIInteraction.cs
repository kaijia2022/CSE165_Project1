using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerUIInteraction : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if the event was triggered by a VR pointer
        if (eventData.pointerPressRaycast.module is OVRRaycaster)
        {
            // Check if the trigger button is pressed on either controller
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                // Handle button press logic
                Debug.Log("Trigger button pressed on Oculus controller");

                // Add your custom logic here to execute when the trigger is pressed
            }
        }
    }
}

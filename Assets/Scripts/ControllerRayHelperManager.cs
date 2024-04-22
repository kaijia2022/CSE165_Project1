using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OVR;
using UnityEngine.EventSystems;

public class ControllerRayHelperManager : MonoBehaviour
{
    public GameObject LeftrayHelper;
    public GameObject RightrayHelper;
    void Update()
    {
        // Check if either the left or right controller is connected and being held
        bool isLeftControllerActive = OVRInput.IsControllerConnected(OVRInput.Controller.LTouch);
        bool isRightControllerActive = OVRInput.IsControllerConnected(OVRInput.Controller.RTouch);

        // Activate or deactivate the ray helper based on controller status
        if (LeftrayHelper != null)
        {
            LeftrayHelper.SetActive(isLeftControllerActive);
        }

        if (RightrayHelper != null)
        {
            RightrayHelper.SetActive(isRightControllerActive);
        }



        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = new Vector2(Screen.width / 2, Screen.height / 2); // Center of the screen

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>() != null) // Ensure it is a button
                {
                    var button = result.gameObject.GetComponent<Button>();
                    button.onClick.Invoke(); // Simulate a click
                    break;
                }
            }
        }

    }
}

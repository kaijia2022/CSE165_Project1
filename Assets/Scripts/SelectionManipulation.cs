using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class SelectionManipulation : MonoBehaviour
{
    public LayerMask ManipulationLayer, ObjectLayer;
    public float maxInteractDistance = 30f;
    public float scaleSpeed = 0.1f;
    public float rotateSpeed = 15f;
    public float translateSpeed = 0.2f;
    private GameObject selectedObject;
    private GameObject Scaler;
    private GameObject rotator;
    private GameObject translator;
    private bool isScaleMode, isRotateMode, isTranslateMode;
    public Color ScaleColor = Color.green;
    public Color RotateColor = Color.red;
    public Color TranslateColor = Color.blue;
    public Color QuitColor = Color.magenta;
    public Color buttonPressed = Color.black;
    public Color highlightColor = Color.yellow;
    private Color ObjectColor;
    private int axis;
    void Start()
    {
        isScaleMode = false;
        isRotateMode = false;
        isTranslateMode = false;
    }
    void Update()
    {
        //Selection
        RaycastHit hitButton;
        RaycastHit hitObj;
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {

           if (Physics.Raycast(transform.position, transform.forward, out hitObj, maxInteractDistance, ObjectLayer))
            {
                if (selectedObject)
                {
                    selectedObject.GetComponent<Renderer>().material.color = ObjectColor;
                }
                GameObject hitObject = hitObj.collider.gameObject;
                selectedObject = hitObject;
                ObjectColor = hitObject.GetComponent<Renderer>().material.color;
                hitObject.GetComponent<Renderer>().material.color = highlightColor;

            }


            if (Physics.Raycast(transform.position, transform.forward, out hitButton, maxInteractDistance, ManipulationLayer))
            {

                GameObject hitObject = hitButton.collider.gameObject;
                Renderer renderer = hitObject.GetComponent<Renderer>();
                

                switch (hitObject.name)
                {
                    case "Scale":
                        resetColor();
                        Scaler = hitObject;
                        resetMode();
                        isScaleMode = true;
                        renderer.material.color = buttonPressed;
                        break;
                    case "Rotate":
                        axis = -1;
                        resetColor();
                        rotator = hitObject;
                        resetMode();
                        isRotateMode = true;
                        renderer.material.color = buttonPressed;
                        break;
                   /* case "alpha":
                        if (isRotateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 0;
                        }
                        break;
                    case "beta":
                        if (isRotateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 1;
                        }
                        break;
                    case "theta":
                        if (isRotateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 2;
                        }

                        break;*/
                    case "Translate":
                        axis = -1;
                        resetColor();
                        translator = hitObject;
                        resetMode();
                        isTranslateMode = true;
                        renderer.material.color = buttonPressed;
                        break;
                   /* case "one":
                        if (isTranslateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 0;
                        }
                        break;
                    case "two":
                        if (isTranslateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 1;
                        }
                        break;
                    case "three":
                        if (isTranslateMode)
                        {
                            resetColor();
                            renderer.material.color = buttonPressed;
                            axis = 2;
                        }

                        break; */
                    case "Quit":
                        resetColor();
                        resetMode();
                        selectedObject = null;
                        break;
                    
                }

            }


        }
        if (isScaleMode)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Vector3 newScale = selectedObject.transform.localScale + Vector3.one * scaleSpeed * Time.deltaTime;
                selectedObject.transform.localScale = newScale;
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                Vector3 newScale = selectedObject.transform.localScale - Vector3.one * scaleSpeed * Time.deltaTime;
                selectedObject.transform.localScale = newScale;
            }
        }
        else if (isRotateMode)
        {
            Vector3 dir = Vector3.up;
          /*  switch (axis)
            {
                case -1:
                    return;
                case 0:
                    dir = Vector3.right;
                    break;
                case 1:
                    dir = Vector3.up;
                    break;
                case 2:
                    dir = Vector3.forward;
                    break;


            }*/
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                selectedObject.transform.Rotate(dir, rotateSpeed);
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                selectedObject.transform.Rotate(dir, -rotateSpeed);
            }
        }

        else if (isTranslateMode)
        {
            Vector3 dir = Vector3.right;
            /*  switch (axis)
                {
                    case -1:
                        return;
                    case 0:
                        dir = Vector3.right;
                        break;
                    case 1:
                        dir = Vector3.up;
                        break;
                    case 2:
                        dir = Vector3.forward;
                        break;


                }*/
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                selectedObject.transform.Translate(dir * translateSpeed);
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                selectedObject.transform.Translate(dir * -translateSpeed);
            }
        }


    }

    void resetMode()
    {
        isScaleMode = false;
        isRotateMode = false;
        isTranslateMode = false;
    }

    void resetColor()
    {
        if (isScaleMode)
        {
            Scaler.GetComponent<Renderer>().material.color = ScaleColor;
        }
        else if (isRotateMode)
        {
            rotator.GetComponent<Renderer>().material.color = RotateColor;
            for (int i = 0; i < rotator.transform.childCount; i++)
            {
                Transform child = rotator.transform.GetChild(i);
                child.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        else if (isTranslateMode)
        {
            translator.GetComponent<Renderer>().material.color = TranslateColor;
            for (int i = 0; i < translator.transform.childCount; i++)
            {
                Transform child = translator.transform.GetChild(i);
                child.GetComponent<Renderer>().material.color = Color.white;
            }
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ObjectController : MonoBehaviour
{
    private MLInput.Controller controller;
    GameObject selectedObject;
    public GameObject atachPoint;
    public GameObject controllerInput;
    bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);

    }

    void updateTrigger()
    {
        if (controller.TriggerValue > 0.0f)
        {
            if (trigger == true)
            {
                RaycastHit hit;
                if (Physics.Raycast(controller.Position, transform.forward, out hit))
                {
                    if (hit.transform.gameObject.tag == "FullObject")
                    {
                        selectedObject = hit.transform.gameObject.transform.parent.gameObject;
                        //atachPoint.transform.position = hit.transform.position;
                        atachPoint.transform.position = selectedObject.transform.position;
                    }
                    if (hit.transform.gameObject.tag == "Parts")
                    {
                        selectedObject = hit.transform.gameObject;
                        atachPoint.transform.position = selectedObject.transform.position;
                    }
                }
                trigger = false;
            }
        }
        if (controller.TriggerValue < 0.2f)
        {
            trigger = true;
            if (selectedObject != null)
            {
                selectedObject = null;
            }
        }

    }

    void UpdateTouchPad()
    {
        if (controller.Touch1Active)
        {
            float x = controller.Touch1PosAndForce.x;
            float y = controller.Touch1PosAndForce.y;
            float force = controller.Touch1PosAndForce.z;

            if (force > 0)
            {
                if (x > 0.5 || x < -0.5)
                {
                    selectedObject.transform.localScale += selectedObject.transform.localScale * x * Time.deltaTime;
                }
                if (y > 0.3 || y < -0.3)
                {
                    atachPoint.transform.position = Vector3.MoveTowards(atachPoint.transform.position, gameObject.transform.position, -y * Time.deltaTime);
                }
            }
        }


    }

    private void OnDestroy()
    {
        MLInput.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.Position;
        transform.rotation = controller.Orientation;

        GameObject aux = null;
        if (selectedObject)
        {
            aux = selectedObject;
            selectedObject.transform.position = atachPoint.transform.position;
            selectedObject.transform.rotation = gameObject.transform.rotation;

            if (selectedObject.transform.tag == "Parts")
            {
                selectedObject.transform.GetChild(0).gameObject.SetActive(true);
            }

        }

        updateTrigger();
        UpdateTouchPad();

        if (selectedObject == null && aux.transform.tag == "Parts")
        {
            aux.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}

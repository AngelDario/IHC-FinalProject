using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Paintable : MonoBehaviour
{

    public GameObject Brush;
    public float BrushSize = 0.1f;

    //Controller
    public Transform controllerTransform;
    private bool triggerPulled = false;
    private MLInput.Controller _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = MLInput.GetController(MLInput.Hand.Right);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
             
            if (Physics.Raycast(Ray, out hit))
            {
                var go = Instantiate(Brush, hit.point + Vector3.up * 0.1f, Quaternion.Euler(-90.0f,0.0f, 0.0f), transform);
                go.transform.localScale = Vector3.one * BrushSize;
            }
        }

        //-------------------ControllerRaycast-----------------------
        if ((_controller.TriggerValue > 0.8f) && !triggerPulled)
        {
            //Get the orientation of the controller
            controllerTransform.rotation = _controller.Orientation;
            // Create a raycast parameters variable
            MLRaycast.QueryParams _raycastParams = new MLRaycast.QueryParams
            {
                // Update the parameters with our controller's transform
                Position = _controller.Position,
                Direction = controllerTransform.forward,
                UpVector = controllerTransform.up,
                // Provide a size of our raycasting array (1x1)
                Width = 1,
                Height = 1
            };
            // Feed our modified raycast parameters and handler to our raycast request
            MLRaycast.Raycast(_raycastParams, HandleOnReceiveRaycast);
            triggerPulled = true;
        }
        //Trigger is released, reset our bool so we can raycast again
        if ((_controller.TriggerValue < 0.2f))
        {
            triggerPulled = false;
        }
    }

    void HandleOnReceiveRaycast(MLRaycast.ResultState state, UnityEngine.Vector3 point, Vector3 normal, float confidence)
    {
        if (state == MLRaycast.ResultState.HitObserved)
        {
            var go = Instantiate(Brush, point + Vector3.up * 0.1f, Quaternion.Euler(-90.0f, 0.0f, 0.0f), transform);
            go.transform.localScale = Vector3.one * BrushSize;
        }
    }

}

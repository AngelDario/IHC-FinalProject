using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;

public class ObjectManagment : MonoBehaviour
{

    public MLInput.Controller controller;
    //public GameObject CanvasImage;
    public GameObject CanvasTurorial;
    public GameObject controllerInput;
    //public GameObject DetailsCanvas;
    //public GameObject[] Details;
    //public GameObject[] Objects;

    // Start is called before the first frame update
    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
        //int Details_Size = Details.Length;
        //for (int i = 0; i < Details_Size; i++)
        //{
        //    Details[i].SetActive(false);
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (controller.TriggerValue > 0.5f) { 
        
        //    //Debug.LogFormat("Entro!!!!!!");

        //int Objects_Size = Objects.Length;
        //int Details_Size = Details.Length;
        

        RaycastHit hit;
        if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
        {
            if (controller.IsBumperDown)
            {
                if(hit.transform.gameObject.name == "PlayButton")
                {
                    CanvasTurorial.SetActive(false);
                }
            }
            //for (int i = 0; i < Objects_Size; i++)
            //{
            //    if (hit.transform.gameObject == Objects[i])
            //    {
            //        Objects[i].transform.GetChild(0).gameObject.SetActive(true);
            //        //Activar su detalle
            //        SetDetailActive(i);
            //    }
            //    else
            //    {
            //        Objects[i].transform.GetChild(0).gameObject.SetActive(false);
            //        Details[i].SetActive(false);
            //    }
            //}

        }
        
        //if(controller.TriggerValue > 0.5f)
        //{
        //    DetailsCanvas.SetActive(true);
        //}
        //else
        //{
        //    DetailsCanvas.SetActive(false);
        //}
    }

    void SetDetailActive(int index)
    {
        //if (controller.TriggerValue > 0.5f)
        //{
        //    Details[index].SetActive(true);
        //}
        //else
        //{
        //    Details[index].SetActive(false);
        //}
    }
    
    void printName(string name)
    {
        Debug.LogFormat(name);
    }
    //void StartApp()
    //{
    //    CanvasImage.SetActive(false);
    //}

    private void OnDestroy()
    {
        MLInput.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MenuController : MonoBehaviour
{
    public MLInput.Controller controller;
    public GameObject controllerInput;
    public GameObject Objetos;
    public GameObject[] ObjectsToPlace;
    private int ObjectActive = 0;
    private bool isObjectSpawned = false;
    private GameObject placeObject;

    void Start()
    {
        MLInput.Start();
        controller = MLInput.GetController(MLInput.Hand.Left);
        UpdateActiveObjects();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
        {
            if (controller.IsBumperDown)
            {
                if (hit.transform.gameObject.name == "ButtonLeft")
                {
                    ObjectActive = positive_mod(ObjectActive - 1, Objetos.transform.childCount);
                    //Debug.Log(ObjectActive);
                    UpdateActiveObjects();

                }
                else if (hit.transform.gameObject.name == "ButtonRight")
                {
                    ObjectActive = positive_mod(ObjectActive + 1, Objetos.transform.childCount);
                    //Debug.Log(ObjectActive);
                    UpdateActiveObjects();
                }
                else if (hit.transform.gameObject.name == "ObjectSpawner")
                {
                    if (!isObjectSpawned)
                    {
                        isObjectSpawned = true;
                        placeObject = Instantiate(ObjectsToPlace[ObjectActive], hit.point + new Vector3(0.0f, 0.0f, -0.5f), Quaternion.Euler(hit.normal));
                        //placeObject = Instantiate(ObjectsToPlace[ObjectActive], hit.point , Quaternion.Euler(hit.normal));
                    }
                }
                else if (hit.transform.gameObject.name == "SepararButton")
                {
                    if (isObjectSpawned)
                    {
                        for (int i = 0; i < placeObject.transform.childCount; i++)
                        {
                            placeObject.transform.GetChild(i).gameObject.tag = "Parts";
                        }
                    }
                }

            }

        }
        

    }
    
    int positive_mod(int i, int n)
    {
        int mod = i % n;
        if (mod < 0)
        {
            mod += n;
        }
        return mod;
    }

    void UpdateActiveObjects()
    {
        for (int i = 0; i < Objetos.transform.childCount; i++)
        {
            Objetos.transform.GetChild(i).gameObject.SetActive(false);
        }

        Objetos.transform.GetChild(ObjectActive).gameObject.SetActive(true);

        isObjectSpawned = false;
        Destroy(placeObject);
    }
    
    private void OnDestroy()
    {
        MLInput.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

[RequireComponent(typeof(MLPrivilegeRequesterBehavior))]

public class ImageTrackingManager : MonoBehaviour
{

    public GameObject TrackedImage;
    private MLPrivilegeRequesterBehavior _privRequiested = null;
    // Start is called before the first frame update
    void Awake()
    {
        _privRequiested = GetComponent<MLPrivilegeRequesterBehavior>();
        _privRequiested.OnPrivilegesDone += HandlePrivilegesDone;
    }

    void HandlePrivilegesDone(MLResult result)
    {
        if (!result.IsOk)
        {
            Debug.Log("Error Privilege Request Failed");
        }
        else
        {
            Debug.Log("Succes: Priv Granted");
        }
    }
    
    void StartCapture()
    {
        TrackedImage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

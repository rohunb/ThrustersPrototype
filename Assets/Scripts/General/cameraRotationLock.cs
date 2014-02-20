using UnityEngine;
using System.Collections;

public class cameraRotationLock : MonoBehaviour {

    public GameObject mainCamera;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (mainCamera)
        {
            transform.rotation = mainCamera.transform.rotation;
        }
	}
}

using UnityEngine;
using System.Collections;

public class FakeMousePointerMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Camera.main)
        {
            if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
            {
                Debug.Log("s");
                transform.localRotation = SixenseInput.Controllers[1].Rotation;
                //transform.rotation=Quaternion.identity;
                //				Quaternion rot = SixenseInput.Controllers[1].Rotation;
                //				rot.eulerAngles= Quaternion.Euler(
                //transform.Rotate(0f,-90f,0);
            }
            else
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
                transform.LookAt(mousePos);
            }
        }
	}
}

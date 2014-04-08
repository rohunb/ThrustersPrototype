using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));

		if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
		{
			transform.rotation = SixenseInput.Controllers[1].Rotation;
		}
		else
		{
			transform.LookAt(mousePos);
		}
	}
}

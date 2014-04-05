using UnityEngine;
using System.Collections;

public class bkgCameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if (GOD.cameraPos != null)
		{
			gameObject.transform.position = GOD.cameraPos;
			gameObject.transform.rotation = GOD.cameraRot;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class LookAtTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
        transform.LookAt(mousePos);
        //transform.LookAt(new Vector3(mousePos.x, mousePos.y, 100));
	}
}

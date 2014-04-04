using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject go = GameObject.Find("FakeMousePointer");
		
		go.transform.rotation = SixenseInput.Controllers[1].Rotation;
		
		Ray rayCharles = new Ray(go.transform.position, go.transform.forward);

		RaycastHit hit;
		
		bool whocares = Physics.Raycast(rayCharles, out hit);

		//hit.point = Camera.main.WorldToViewportPoint(hit.point);
		
		//xMin = (hit.point.x);
		//yMin = (hit.point.y);

		Debug.Log (hit.point.x + " " + hit.point.y);
		
		Debug.DrawLine(rayCharles.origin, hit.point);
	}
}

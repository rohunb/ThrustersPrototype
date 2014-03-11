using UnityEngine;
using System.Collections;

public class OrbitAtGameObject : MonoBehaviour {
    public GameObject targetObject;
    public float orbitSpd;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.LookAt(targetObject.transform);
        gameObject.transform.RotateAround(Vector3.zero, Vector3.up, orbitSpd * Time.deltaTime);        
	}
}

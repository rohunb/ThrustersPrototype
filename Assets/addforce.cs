using UnityEngine;
using System.Collections;

public class addforce : MonoBehaviour {

	// Use this for initialization
	void Start () {
        rigidbody.AddForceAtPosition(transform.right, new Vector3(-1.308789f ,-0.5759091f, -4.525307f));
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}

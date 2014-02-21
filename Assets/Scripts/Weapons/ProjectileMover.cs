using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
    public float laserSpeed=1000f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.forward*laserSpeed*Time.deltaTime);
        
        Vector3 velocity=rigidbody.velocity;
        velocity = transform.forward * laserSpeed;
        rigidbody.velocity = velocity;
	}
}

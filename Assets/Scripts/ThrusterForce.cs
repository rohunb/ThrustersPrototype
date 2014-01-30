using UnityEngine;
using System.Collections;

public class ThrusterForce : MonoBehaviour {

    public float maxThrustForce = 100.0f;
    public bool damaged;
    private Rigidbody spaceship;
	// Use this for initialization
	void Start () {
        damaged = false;
        spaceship = transform.parent.gameObject.rigidbody;
	}
	
	// Update is called once per frame
	void Update () {

        

	}
    public void FireThruster()
    {
        Debug.Log("firing thruster");
        Vector3 direction = spaceship.transform.position - transform.position;
        spaceship.AddForceAtPosition(direction.normalized, transform.position);
        //spaceship.AddForce(transform.forward * maxThrustForce);

    }
}

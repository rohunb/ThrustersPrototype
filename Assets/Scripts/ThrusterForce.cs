using UnityEngine;
using System.Collections;

public class ThrusterForce : MonoBehaviour {

    public float maxThrustForce = 100.0f;
    public bool damaged;
    private Rigidbody spaceship;
    private ParticleSystem afterburner;
	// Use this for initialization
	void Start () {
        afterburner=gameObject.GetComponentInChildren<ParticleSystem>();
        afterburner.active = false;
        spaceship = transform.parent.gameObject.rigidbody;
	}
	
	// Update is called once per frame
	void Update () {

        //afterburner.active = false;

	}
    public void FireThruster()
    {
        afterburner.active = true;
        spaceship.AddForceAtPosition(transform.forward * maxThrustForce, transform.position);
        Invoke("StopAfterburner", 0.5f);

    }
    private void StopAfterburner()
    {
        afterburner.active = false;
    }
}

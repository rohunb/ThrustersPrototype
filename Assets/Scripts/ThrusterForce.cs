using UnityEngine;
using System.Collections;

public class ThrusterForce : MonoBehaviour {

    public float maxThrustForce = 100.0f;
    public bool damaged;
    private Rigidbody spaceship;
    private ParticleSystem afterburner;
    private bool firing;
    private float originalLifetime;
    private float originalStartSize;   
	// Use this for initialization
	void Start () {
        afterburner=gameObject.GetComponentInChildren<ParticleSystem>();
        afterburner.active = false;
        spaceship = transform.parent.gameObject.rigidbody;
        firing = false;
        originalLifetime = afterburner.startLifetime;
        originalStartSize = afterburner.startSize;
	}
	
	// Update is called once per frame
	void Update () {

        if (!firing)
        {
            afterburner.active = false;    
        }
	}
    public void FireThruster()
    {
        afterburner.active = true;
        firing = true;
        spaceship.AddForceAtPosition(transform.forward * maxThrustForce, transform.position);
        Invoke("StopAfterburner", 0.5f);
        //StopAfterburner();

    }
    private void StopAfterburner()
    {
        //afterburner.active = false;
        firing = false;
        
    }
}

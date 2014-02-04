using UnityEngine;
using System.Collections;

public class ThrusterForce : MonoBehaviour {

    public float maxThrustForce = 100.0f;
    public bool damaged;
    private Rigidbody spaceship;
    private ParticleSystem[] afterburners;
    private MeshRenderer mesh;
    private bool firing;
    //private float originalLifetime;
    //private float originalStartSize;   
	// Use this for initialization
	void Start () {
        mesh = gameObject.GetComponentInChildren<MeshRenderer>();
        mesh.enabled = false;
        afterburners=gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.enableEmission = false;    
        }
        
        spaceship = transform.parent.gameObject.rigidbody;
        firing = false;
        //originalLifetime = afterburners[0].startLifetime;
        //originalStartSize = afterburners[0].startSize;
	}
	
	// Update is called once per frame
	void Update () {

        if (!firing)
        {
            foreach (ParticleSystem afterburner in afterburners)
            {
                afterburner.enableEmission = false;    
            }
        }
	}
    public void FireThruster()
    {
        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.enableEmission = true;    
        }
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

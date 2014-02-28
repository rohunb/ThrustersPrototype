using UnityEngine;
using System.Collections;

public class ThrusterForce : MonoBehaviour {

    public float maxThrustForce = 100.0f;
    public bool damaged;
    private Rigidbody spaceship;
    private ParticleSystem[] afterburners;
    private MeshRenderer mesh;
    private bool firing;
    
    public Color minColour;
    private float originalLifetime;
    private float originalStartSize;
    //private Color originalStartColour;

    public float thrustAmount = 1f;
	// Use this for initialization
	void Start () {
        mesh = gameObject.GetComponentInChildren<MeshRenderer>();
        mesh.enabled = false;
        afterburners=gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.enableEmission = false;    
        }

        //spaceship = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<Rigidbody>();
        spaceship = transform.root.rigidbody;
        firing = false;
        originalLifetime = afterburners[0].startLifetime;
        originalStartSize = afterburners[0].startSize;
        //originalStartColour = afterburners[0].startColor;
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
        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.startLifetime = Mathf.Lerp(0.0f, originalLifetime,thrustAmount);// Mathf.Clamp(thrustAmount,.4f,1.0f));
            afterburner.startSize = Mathf.Lerp(originalStartSize * .3f, originalStartSize,thrustAmount);// Mathf.Clamp(thrustAmount, .4f, 1.0f));
            //afterburner.startColor = Color.Lerp(minColour, originalStartColour, thrustAmount);
        }
	}
    public void FireThruster()
    {
        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.enableEmission = true;    
        }
        firing = true;
        spaceship.AddForceAtPosition(transform.forward * maxThrustForce, transform.position,ForceMode.Force );
        Invoke("StopAfterburner", 0.5f);
        //StopAfterburner();

    }
    private void StopAfterburner()
    {
        //afterburner.active = false;
        firing = false;
        
    }
}

using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
    public float speed=1000f;
	private AudioEngine ae;
	// Use this for initialization
	void Start () {
		ae = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
		//ae.playSFX("Laser");
        //Destroy(gameObject, 3.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.Translate(Vector3.forward*laserSpeed*Time.deltaTime);
        
        Vector3 velocity=rigidbody.velocity;
        velocity = transform.forward * speed;
        rigidbody.velocity = velocity;
	}
}

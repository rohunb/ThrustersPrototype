using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
    public float speed;
    public float range;
	//private AudioEngine ae;
	// Use this for initialization
	void Start () {
		//ae = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
		//ae.playSFX("Laser");
        float timeToDestroy=range/speed;
        Destroy(gameObject, timeToDestroy);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        Vector3 velocity=rigidbody.velocity;
        velocity = transform.forward * speed;
        rigidbody.velocity = velocity;
	}
}

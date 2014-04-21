using UnityEngine;
using System.Collections;

public class ProjectileMover : MonoBehaviour {
	public float speed;
	public float range;
    public float laserTurnSpeed;
    public Transform target;


	// Use this for initialization
	void Start () {

		float timeToDestroy=range/speed;
		Destroy(gameObject, timeToDestroy);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 velocity=rigidbody.velocity;
		velocity = transform.forward * speed;
		rigidbody.velocity = velocity;
        if (target)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), laserTurnSpeed * Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {

    public float missileTurnSpeed;
    public Transform target;
	
    void FixedUpdate () {
        if (target)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), missileTurnSpeed * Time.deltaTime);
	}
}

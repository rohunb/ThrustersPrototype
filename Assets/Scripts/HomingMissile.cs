using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {

    public float missileSpeed;
    public float missileTurnSpeed;
    public float missileAcceleration;
    public static GameObject target;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //if(target==null)
        //    target = GameObject.FindGameObjectWithTag("Target");
        if (target == null)
        {
            //normal missile movement
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), missileTurnSpeed * Time.deltaTime);
            transform.position += transform.forward * missileSpeed * Time.deltaTime;
            
            
        }
       
        

	}
}

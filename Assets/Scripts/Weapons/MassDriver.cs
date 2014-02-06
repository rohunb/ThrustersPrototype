using UnityEngine;
using System.Collections;

public class MassDriver : MonoBehaviour {

	public GameObject projectile;
    public Transform shootPoint;
	
    public void Fire(float damage,float force,Rigidbody ship)
    {
        GameObject projectileClone=Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
        projectileClone.rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        ship.AddForce(transform.forward * -force,  ForceMode.Impulse);


    }
}

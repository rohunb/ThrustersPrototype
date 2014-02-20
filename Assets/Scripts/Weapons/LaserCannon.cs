using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {
    public GameObject laser;
    public Transform shootPoint;
	
    public void Fire(float damage,GameObject _origin)
    {
        GameObject laserClone = Instantiate(laser, shootPoint.position, shootPoint.rotation) as GameObject;
        laserClone.GetComponent<ProjectileDamager>().origin = _origin;
    }
}

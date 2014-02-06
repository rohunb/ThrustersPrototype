using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {
    public GameObject laser;
    public Transform shootPoint;
	
    public void Fire(float damage)
    {
        Instantiate(laser, shootPoint.position, shootPoint.rotation);
    }
}

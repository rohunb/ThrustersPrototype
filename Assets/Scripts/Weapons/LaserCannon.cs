using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {
	public GameObject laser;
	public Transform shootPoint;
	
	public void Fire(int _damage,float _speed,GameObject _origin)
	{
		GameObject laserClone = Instantiate(laser, shootPoint.position, shootPoint.rotation) as GameObject;
		ProjectileDamager damager = laserClone.GetComponent<ProjectileDamager>();
		GOD.audioengine.playSFX("Laser");
		damager.origin = _origin;
		damager.damage = _damage;
		laserClone.GetComponent<ProjectileMover>().speed = _speed;
	}
}


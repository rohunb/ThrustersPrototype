using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public string name;
    public int damage;
    public GameObject projectile;
    public float projectileSpeed;
    public float reloadTimer;
    public Transform shootPoint;
    public Transform target;
    public GameObject origin;
    public float range;
    protected bool canFire = true;
	
    public virtual void Fire()
    {
        
    }
}

using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {
    
    public float laserDamage;
    public float laserReloadTimer = 1.0f;


    private LaserCannon[] laserCannons;
    private bool canFire;
	// Use this for initialization
	void Start () {
        canFire = true;
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("FireLasers");
            canFire = false;
        }
	}
    IEnumerator FireLasers()
    {
        foreach (LaserCannon laserCannon in laserCannons)
        {
            laserCannon.Fire(laserDamage);
        }
        yield return new WaitForSeconds(laserReloadTimer);
        canFire = true;
    }
}

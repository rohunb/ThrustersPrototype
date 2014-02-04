using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {
    
    public float laserDamage;
    public float laserReloadTimer = 1.0f;

    public float missileDamage;
    public float missileReloadTimer = 1.0f;

    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    private MissileLauncher[] missileLaunchers;
    private bool canFireMissiles;
	// Use this for initialization
	void Start () {
        canFireLasers = true;
        canFireMissiles = true;
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
        missileLaunchers = gameObject.GetComponentsInChildren<MissileLauncher>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1") && canFireLasers)
        {
            StartCoroutine("FireLasers");
            canFireLasers = false;
        }
        if (Input.GetButton("Fire2") && canFireMissiles)
        {
            StartCoroutine("FireMissiles");
            canFireMissiles = false;
        }
	}
    IEnumerator FireLasers()
    {
        foreach (LaserCannon laserCannon in laserCannons)
        {
            laserCannon.Fire(laserDamage);
        }
        yield return new WaitForSeconds(laserReloadTimer);
        canFireLasers = true;
    }
    IEnumerator FireMissiles()
    {
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.Fire(missileDamage);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }
}

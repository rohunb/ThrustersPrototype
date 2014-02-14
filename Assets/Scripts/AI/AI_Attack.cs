using UnityEngine;
using System.Collections;

public class AI_Attack : MonoBehaviour {
    public float laserDamage;
    public float laserReloadTimer = 1.0f;

    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    public Transform target;
    AI_Controller ai_controller;
	// Use this for initialization
	void Start () {
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
        canFireLasers = true;
        ai_controller = gameObject.GetComponent<AI_Controller>();
        target = GameObject.FindGameObjectWithTag("AI_Dest").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (ai_controller.attacking)
        {
            foreach (LaserCannon laserCannon in laserCannons)
            {
                laserCannon.transform.LookAt(target);
            }
            if (canFireLasers)
            {
                StartCoroutine("FireLasers");
                canFireLasers = false;
            }
        }
        else
        {
            foreach (LaserCannon laserCannon in laserCannons)
            {
                laserCannon.transform.rotation = transform.rotation;
            }
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
}

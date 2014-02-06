using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {
    
    public float laserDamage;
    public float laserReloadTimer = 1.0f;

    public float missileDamage;
    public float missileReloadTimer = 1.0f;

    public Texture2D crosshair;
    
    
    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    private MissileLauncher[] missileLaunchers;
    private bool canFireMissiles;

    private Transform target;


	// Use this for initialization
	void Start () {
        canFireLasers = true;
        canFireMissiles = true;
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
        missileLaunchers = gameObject.GetComponentsInChildren<MissileLauncher>();
        target = GameObject.FindGameObjectWithTag("Target").transform;

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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine("FireClusterMissiles");
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
            missileLauncher.FireMissile(missileDamage,target);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }
    IEnumerator FireClusterMissiles()
    {
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.FireClusterMissile(missileDamage,target);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }
    void OnGUI()
    {
        //Debug.Log("transform: "+transform.position);
        //Debug.Log("pos: " + pos);
        //Rect textRect = new Rect(pos.x, pos.y, crosshair.width, crosshair.height);
        //GUI.DrawTexture(textRect, crosshair);
        //Vector2 pos = Camera.main.WorldToScreenPoint(transform.position+transform.forward*20);

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 20);
        float xMin = Screen.width - (crosshair.width / 2 / 10)-pos.x;
        float yMin = Screen.height  - (crosshair.height / 2 / 10)-pos.y;
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);
    }
}


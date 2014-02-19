using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {
    
    public float laserDamage;
    public float laserReloadTimer = 1.0f;

    public float missileDamage;
    public float missileReloadTimer = 1.0f;

    public float massDriverDamage;
    public float massDriverReloadTimer = 1.0f;
    public float massDriverForce = 50f;

    public Texture2D crosshair;
    
    
    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    private MissileLauncher[] missileLaunchers;
    private bool canFireMissiles;

    private MassDriver[] massDrivers;
    private bool canFireMassDriver;

    private Transform target;

    enum Weapons {Lasers,Missiles,ClusterMissiles,MassDrivers }
    private Weapons currentWeapon;
	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        canFireLasers = true;
        canFireMissiles = true;
        canFireMassDriver = true;
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
        missileLaunchers = gameObject.GetComponentsInChildren<MissileLauncher>();
        massDrivers = gameObject.GetComponentsInChildren<MassDriver>();
        //target = GameObject.FindGameObjectWithTag("Target").transform;
        target = null;
        currentWeapon = Weapons.Lasers;

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = Weapons.Lasers;
            Debug.Log("Lasers Selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = Weapons.Missiles;
            Debug.Log("Missiles Selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = Weapons.ClusterMissiles;
            Debug.Log("Cluster Missiles Selected");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = Weapons.MassDrivers;
            Debug.Log("Mass Drivers Selected");
        }
        if (Input.GetButton("Fire1"))
        {
            switch (currentWeapon)
            {
                case Weapons.Lasers:
                    if(canFireLasers)
                    {
                        StartCoroutine("FireLasers");
                        canFireLasers = false;
                    }
                    break;
                case Weapons.Missiles:
                    if(canFireMissiles)
                    {
                        StartCoroutine("FireMissiles");
                        canFireMissiles = false;
                    }
                    break;
                case Weapons.ClusterMissiles:
                    if(canFireMissiles)
                    {
                        StartCoroutine("FireClusterMissiles");
                        canFireMissiles = false;
                    }
                    break;
                case Weapons.MassDrivers:
                    if(canFireMassDriver)
                    {
                        StartCoroutine("FireMassDrivers");
                        canFireMassDriver = false;
                    }
                    break;
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

    IEnumerator FireMassDrivers()
    {
        foreach (MassDriver massDriver in massDrivers)
        {
            massDriver.Fire(massDriverDamage,massDriverForce,rigidbody);
        }
        yield return new WaitForSeconds(massDriverReloadTimer);
        canFireMassDriver = true;
    }

    void OnGUI()
    {
        //Debug.Log("transform: "+transform.position);
        //Debug.Log("pos: " + pos);
        //Rect textRect = new Rect(pos.x, pos.y, crosshair.width, crosshair.height);
        //GUI.DrawTexture(textRect, crosshair);
        //Vector2 pos = Camera.main.WorldToScreenPoint(transform.position+transform.forward*20);

        //Vector2 pos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 10);
        //float xMin = Screen.width - (crosshair.width / 2 / 10)-pos.x;
        //float yMin = Screen.height  - (crosshair.height / 2 / 10)-pos.y;
        //GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);

        float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2 / 10);
        float yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);
        

        GUILayout.BeginArea(new Rect(10, Screen.height-30, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Weapon: " + currentWeapon.ToString());
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}


using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour
{

    public float laserDamage;
    public float laserReloadTimer = 1.0f;

    public float missileDamage;
    public float missileReloadTimer = 1.0f;

    public float massDriverDamage;
    public float massDriverReloadTimer = 1.0f;
    public float massDriverForce = 50f;

    public Transform targeter;
    public Texture2D crosshair;
    public Texture2D targetBoxTexture;

    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    private MissileLauncher[] missileLaunchers;
    private bool canFireMissiles;

    private MassDriver[] massDrivers;
    private bool canFireMassDriver;

    private Transform target;

    enum Weapons { Lasers, Missiles, ClusterMissiles, MassDrivers }
    private Weapons currentWeapon;
    // Use this for initialization
    void Start()
    {
        Screen.showCursor = false;
        canFireLasers = true;
        canFireMissiles = true;
        canFireMassDriver = true;
        laserCannons = gameObject.GetComponentsInChildren<LaserCannon>();
        missileLaunchers = gameObject.GetComponentsInChildren<MissileLauncher>();
        massDrivers = gameObject.GetComponentsInChildren<MassDriver>();
        //target = GameObject.FindGameObjectWithTag("EnemyShip").transform;
        target = null;
        currentWeapon = Weapons.Lasers;

    }

    // Update is called once per frame
    void Update()
    {
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
                    if (canFireLasers)
                    {
                        StartCoroutine("FireLasers");
                        canFireLasers = false;
                    }
                    break;
                case Weapons.Missiles:
                    if (canFireMissiles)
                    {
                        StartCoroutine("FireMissiles");
                        canFireMissiles = false;
                    }
                    break;
                case Weapons.ClusterMissiles:
                    if (canFireMissiles)
                    {
                        StartCoroutine("FireClusterMissiles");
                        canFireMissiles = false;
                    }
                    break;
                case Weapons.MassDrivers:
                    if (canFireMassDriver)
                    {
                        StartCoroutine("FireMassDrivers");
                        canFireMassDriver = false;
                    }
                    break;
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            LockOn();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            TargetNextEnemy();
        }
        if (Input.GetKeyDown(KeyCode.T))
            TargetNearestEnemy();

    }

    private void TargetNextEnemy()
    {
        if (!target)
            TargetNearestEnemy();
        else
        {
            for (int i = 0; i < EnemyController.enemies.Count; i++)
            {
                if (target.gameObject == EnemyController.enemies[i])
                {
                    if (i == EnemyController.enemies.Count - 1)
                        target = EnemyController.enemies[0].transform;
                    else
                        target = EnemyController.enemies[i + 1].transform;
                    break;
                }
            }


        }
    }

    private void TargetNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShip");
        if (enemies.Length > 0)
        {
            float minDistance = Vector3.Distance(enemies[0].transform.position, transform.position);
            target = enemies[0].transform;
            float distance = 0;
            foreach (GameObject enemy in enemies)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance)
                {
                    target = enemy.transform;
                    minDistance = distance;
                }
            }
        }
    }
    void LockOn()
    {
        Ray targettingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(targettingRay, out hit, 10000f))
        {
            if (hit.collider.gameObject.tag == "EnemyShip")
            {
                //audio lock on
                target = hit.collider.gameObject.transform;
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
            missileLauncher.FireMissile(missileDamage, target);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }
    IEnumerator FireClusterMissiles()
    {
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.FireClusterMissile(missileDamage, target);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }

    IEnumerator FireMassDrivers()
    {
        foreach (MassDriver massDriver in massDrivers)
        {
            massDriver.Fire(massDriverDamage, massDriverForce, rigidbody);
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


        GUILayout.BeginArea(new Rect(10, Screen.height - 70, 150, 150));
        GUILayout.BeginVertical();
        
        GUILayout.Label("Weapon: " + currentWeapon.ToString());
        if (target)
        {
            GUILayout.Label("Target: " + target.name.ToString());
            GUILayout.Label("Angle to Target: " + Vector3.Angle(targeter.forward, target.position - transform.position).ToString());
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (target)
        {
            float size = 10000 / Vector3.Distance(transform.position, target.position);
            size = Mathf.Clamp(size, 45f, 112f);
            Vector3 position = Camera.main.WorldToScreenPoint(target.position);
            position.y = Screen.height - position.y;
            Debug.Log("Position: " + position.ToString());
            position.x = Mathf.Clamp(position.x, 0, Screen.width - size / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - size / 2);
            if (Vector3.Angle(targeter.forward, target.position - transform.position) > 90)
            {
                if (position.x >= Screen.width / 2)
                    position.x = Mathf.Clamp(position.x, Screen.width - size / 2, Screen.width - size / 2);
                else
                    position.x = Mathf.Clamp(position.x, 0, 0);
                if (position.y >= Screen.height / 2)
                    position.y = Mathf.Clamp(position.y, Screen.height - size / 2, Screen.height - size / 2);
                else
                    position.y = Mathf.Clamp(position.y, 0, 0);
                
            }
            GUI.DrawTexture(new Rect((position.x - (size / 2)), (position.y - (size / 2)), size, size), targetBoxTexture);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2)), size * 20, size * 2), "Target = " + target.name);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 15), size * 20, size * 2), "Dist = " + Vector3.Distance(transform.position, target.position));
        }
    }
}


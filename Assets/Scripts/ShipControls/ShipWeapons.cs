﻿using UnityEngine;
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

    public float minTargetableDistance = 2000f;
    public Transform targeter;
    public Texture2D crosshair;
    public Texture2D targetBoxTexture;
    public Texture2D targetLeadTexture;

    private LaserCannon[] laserCannons;
    private bool canFireLasers;

    private MissileLauncher[] missileLaunchers;
    private bool canFireMissiles;

    private MassDriver[] massDrivers;
    private bool canFireMassDriver;

    private Transform target;
    private Vector3 targetLead;

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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = Weapons.Missiles;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = Weapons.ClusterMissiles;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = Weapons.MassDrivers;
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TargetNextEnemy();
        }
        if (Input.GetKeyDown(KeyCode.T))
            TargetNearestEnemy();
        if (target)
        {
            if (laserCannons.Length > 0)
            {
                float laserVelocity = laserCannons[0].laser.GetComponent<ProjectileMover>().laserSpeed;
                float distToTarget = Vector3.Distance(transform.position, target.position);
                float timeToTarget = distToTarget / laserVelocity;
                Vector3 targetVel = target.rigidbody.velocity;
                targetLead = target.position + target.rigidbody.velocity * timeToTarget;
            }
        }
    }
    
    private void TargetNextEnemy()
    {
        if (!target)
        {
            TargetNearestEnemy();
            
        }
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
        float minDistance;
        if (enemies.Length > 0)
        {
            minDistance = Vector3.Distance(enemies[0].transform.position, transform.position);
            if(minDistance<minTargetableDistance)
                target = enemies[0].transform;
            float distance = 0;
            foreach (GameObject enemy in enemies)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance && distance < minTargetableDistance)
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
            laserCannon.Fire(laserDamage,gameObject);
        }
        yield return new WaitForSeconds(laserReloadTimer);
        canFireLasers = true;
    }
    IEnumerator FireMissiles()
    {
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.FireMissile(missileDamage, target,gameObject);
        }
        yield return new WaitForSeconds(missileReloadTimer);
        canFireMissiles = true;
    }
    IEnumerator FireClusterMissiles()
    {
        foreach (MissileLauncher missileLauncher in missileLaunchers)
        {
            missileLauncher.FireClusterMissile(missileDamage, target,gameObject);
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
        float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2 / 10);
        float yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);


        GUILayout.BeginArea(new Rect(10, Screen.height - 120, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Afterburner: " + gameObject.GetComponent<ShipMove>().currentAfterburnerLevel);
        GUILayout.Label("Health: " + gameObject.GetComponent<Health>().health);
        GUILayout.Label("Shield: " + gameObject.GetComponent<Health>().shieldStrength);
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
            Vector3 leadPos = Camera.main.WorldToScreenPoint(targetLead);
            float leadSize = 15f;
            leadPos.y = Screen.height - leadPos.y;
            leadPos.x = Mathf.Clamp(leadPos.x, 0, Screen.width - leadSize / 2);
            leadPos.y = Mathf.Clamp(leadPos.y, 0, Screen.height - leadSize / 2);
            if(Vector3.Angle(targeter.forward,target.position-transform.position)<90)
            {
                GUI.DrawTexture(new Rect((leadPos.x - (leadSize / 2)), (leadPos.y - (leadSize / 2)), leadSize, leadSize), targetLeadTexture);
            }
            GUI.DrawTexture(new Rect((position.x - (size / 2)), (position.y - (size / 2)), size, size), targetBoxTexture);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2)), size * 20, size * 2), "Target = " + target.name);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 15), size * 20, size * 2), "Health = " + target.gameObject.GetComponent<Health>().health);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 30), size * 20, size * 2), "Shield = " + target.gameObject.GetComponent<Health>().shieldStrength);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 45), size * 20, size * 2), "Dist = " + Vector3.Distance(transform.position, target.position));
        }
    }
}


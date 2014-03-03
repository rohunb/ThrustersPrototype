using UnityEngine;
using System.Collections;

public class ShipAttack : MonoBehaviour {

    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Weapon tertiaryWeapon;
    public Weapon utilityWeapon;
    public Weapon[] allWeapons;

    //lasers
    public int laserDamage=10;
    public float laserSpeed=1000;
    public float laserReloadTimer = 0.2f;
    public float laserRange = 2000f;

    //railgun
    public int railDamage = 1000;
    public float railSpeed = 3000f;
    public float railReloadTimer = 2.0f;
    public float railRange=2500f;
    
    //missiles
    public int missileDamage=300;
    public float missileReloadTimer = 3.0f;

    //cluster missiles
    public int clusterMissileDamage = 15;
    public float clusterMissileReloadTimer = 3.0f;
    public Transform target;
    
    //torpedoes
    public int torpedoDamage = 3000;
    public float torpedoReloadTimer = 3.0f;

    //mining laser
    public float miningLaserRange = 1000f;
    public int mineAmount = 10;
    public float mineInterval = 1.0f;

    int currentWeaponIndex = 0;
	// Use this for initialization
    void Start()
    {
        allWeapons = new Weapon[4];
        allWeapons[0] = primaryWeapon;
        allWeapons[1] = secondaryWeapon;
        allWeapons[2] = tertiaryWeapon;
        allWeapons[3] = utilityWeapon;

        if (primaryWeapon is Weapon_Lasers)
        {
            primaryWeapon.damage = laserDamage;
            primaryWeapon.projectileSpeed = laserSpeed;
            primaryWeapon.reloadTimer = laserReloadTimer;
            primaryWeapon.range = laserRange;
        }
        else if(primaryWeapon is Weapon_Railgun)
        {
            primaryWeapon.damage = railDamage;
            primaryWeapon.projectileSpeed = railSpeed;
            primaryWeapon.reloadTimer = railReloadTimer;
            primaryWeapon.range = railRange;
        }

        if(secondaryWeapon is Weapon_ClusterMissile)
        {
            secondaryWeapon.damage = clusterMissileDamage;
            secondaryWeapon.target = this.target;
            secondaryWeapon.reloadTimer = clusterMissileReloadTimer;
        }
        else if(secondaryWeapon is Weapon_Missile)
        {
            secondaryWeapon.damage = missileDamage;
            secondaryWeapon.target = this.target;
            secondaryWeapon.reloadTimer = clusterMissileReloadTimer;
        }

        if (tertiaryWeapon is Weapon_Torpedo)
        {
            tertiaryWeapon.damage = torpedoDamage;
            tertiaryWeapon.reloadTimer = torpedoReloadTimer;
        }

        if(utilityWeapon is Weapon_MiningLaser)
        {
            utilityWeapon.range = miningLaserRange;
            Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)utilityWeapon;
            miningLaser.mineAmount = mineAmount;
            miningLaser.mineInterval = mineInterval;
        }

        foreach (Weapon weapon in allWeapons)
        {

            weapon.origin = gameObject;
        }

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("Fire1"))
        {
            primaryWeapon.Fire();
        }
        if(Input.GetButton("Fire2"))
        {
            secondaryWeapon.Fire();
        }
        if(Input.GetKey(KeyCode.Alpha1))
        {
            tertiaryWeapon.Fire();
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            utilityWeapon.Fire();
        }
        else if(utilityWeapon is Weapon_MiningLaser)
        {
            Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)utilityWeapon;
            miningLaser.StopFiring();
        }
            
	}

}

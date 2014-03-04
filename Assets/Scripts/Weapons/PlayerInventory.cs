using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{

    //equipped weapons
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Weapon tertiaryWeapon;
    public Weapon utilityWeapon;
    
    public List<Weapon> availableWeapons;

    //lasers
    public int laserDamage = 10;
    public float laserSpeed = 1000;
    public float laserReloadTimer = 0.2f;
    public float laserRange = 2000f;

    //railgun
    public int railDamage = 1000;
    public float railSpeed = 3000f;
    public float railReloadTimer = 2.0f;
    public float railRange = 2500f;

    //missiles
    public int missileDamage = 300;
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

    //economy
    private int credits;

    
    public int GetCredits()
    {
        return credits;
    }

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
	
	}
    public bool CreateTransaction(int amount)
    {
        if(credits+amount>=0)
        {
            credits+=amount;
            return true;
        }
        else
            return false;
    }
    
    void OnGUI()
    {
        
    }

    void Start()
    {
        credits = 0;


        if (primaryWeapon is Weapon_Lasers)
        {
            primaryWeapon.damage = laserDamage;
            primaryWeapon.projectileSpeed = laserSpeed;
            primaryWeapon.reloadTimer = laserReloadTimer;
            primaryWeapon.range = laserRange;
        }
        else if (primaryWeapon is Weapon_Railgun)
        {
            primaryWeapon.damage = railDamage;
            primaryWeapon.projectileSpeed = railSpeed;
            primaryWeapon.reloadTimer = railReloadTimer;
            primaryWeapon.range = railRange;
        }

        if (secondaryWeapon is Weapon_ClusterMissile)
        {
            secondaryWeapon.damage = clusterMissileDamage;
            secondaryWeapon.target = this.target;
            secondaryWeapon.reloadTimer = clusterMissileReloadTimer;
        }
        else if (secondaryWeapon is Weapon_Missile)
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

        if (utilityWeapon is Weapon_MiningLaser)
        {
            utilityWeapon.range = miningLaserRange;
            Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)utilityWeapon;
            miningLaser.mineAmount = mineAmount;
            miningLaser.mineInterval = mineInterval;
        }

        foreach (Weapon weapon in availableWeapons)
        {
            weapon.origin = gameObject;
        }
    }
}

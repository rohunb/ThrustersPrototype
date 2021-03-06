﻿using UnityEngine;
using System.Collections;

public class Weapon_Lasers : Weapon
{
    public bool homeIn = false;
    void Start()
    {
        weaponType = WeaponType.Primary;

    }
    public override void Init()
    {
        weaponType = WeaponType.Primary;
    }
    public override void Fire()
    {
        if (canFire)
        {
            StartCoroutine("FireLasers");
            canFire = false;
        }
        
    }
    IEnumerator FireLasers()
    {
        GameObject laserClone = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;

		GOD.audioengine.playSFX("Laser");

        ProjectileDamager damager = laserClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;
        
        ProjectileMover mover = laserClone.GetComponent<ProjectileMover>();
        mover.speed = projectileSpeed;
        mover.range = range;
        if(homeIn)
            mover.target = target;
        
        
        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

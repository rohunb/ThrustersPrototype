using UnityEngine;
using System.Collections;

public class Weapon_Lasers : Weapon
{
   
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

        ProjectileDamager damager = laserClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;
        
        ProjectileMover mover = laserClone.GetComponent<ProjectileMover>();
        mover.speed = projectileSpeed;
        mover.range = range;
        
        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

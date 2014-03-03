using UnityEngine;
using System.Collections;

public class Weapon_Missile : Weapon {

    public override void Fire()
    {
        if (canFire)
        {
            StartCoroutine("FireMissiles");
            canFire = false;
        }
    }
    IEnumerator FireMissiles()
    {
        GameObject missileClone = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
        missileClone.rigidbody.AddForce(transform.up * 50f, ForceMode.Impulse);
        missileClone.GetComponent<HomingMissile>().target = target;

        ProjectileDamager damager = missileClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;

        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

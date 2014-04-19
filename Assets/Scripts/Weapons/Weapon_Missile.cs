using UnityEngine;
using System.Collections;

public class Weapon_Missile : Weapon {

    void Start()
    {
        weaponType = WeaponType.Secondary;
    }
    public override void Init()
    {
        weaponType = WeaponType.Secondary;
        
    }
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
		GOD.audioengine.playSFX("MissleLaunch");
        missileClone.rigidbody.velocity = origin.rigidbody.velocity;
		missileClone.rigidbody.AddForce(origin.transform.up * -150f, ForceMode.Impulse);
        missileClone.GetComponent<HomingMissile>().target = target;

        ProjectileDamager damager = missileClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;

        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

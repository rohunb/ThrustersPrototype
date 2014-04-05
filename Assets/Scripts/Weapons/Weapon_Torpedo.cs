using UnityEngine;
using System.Collections;

public class Weapon_Torpedo : Weapon {
    void Start()
    {
        weaponType = WeaponType.Tertiary;
    }
    public override void Init()
    {
        weaponType = WeaponType.Tertiary; ;
    }
    public override void Fire()
    {
        if(canFire)
        {
            StartCoroutine("FireTorpedo");
            canFire = false;
        }
    }
    IEnumerator FireTorpedo()
    {
        GameObject missileClone = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
		GOD.audioengine.playSFX("Torpedo");
        missileClone.rigidbody.AddForce(transform.up * 100f, ForceMode.Impulse);
        ProjectileDamager damager = missileClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;
        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

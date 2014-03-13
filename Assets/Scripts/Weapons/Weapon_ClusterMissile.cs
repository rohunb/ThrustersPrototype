using UnityEngine;
using System.Collections;

public class Weapon_ClusterMissile : Weapon {
    
    void Start()
    {
        weaponType = WeaponType.Secondary;
    }
    public override void Fire()
    {
        if (canFire)
        {
            StartCoroutine("FireClusterMissiles");
            canFire = false;
        }
    }
    IEnumerator FireClusterMissiles()
    {
        GameObject clusterMissileClone = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
		GOD.audioengine.playSFX("MissleLaunch");
		clusterMissileClone.rigidbody.AddForce(origin.transform.up * -150f, ForceMode.Impulse);
        clusterMissileClone.GetComponent<HomingMissile>().target = target;
        ClusterMissile clusterMissile = clusterMissileClone.GetComponent<ClusterMissile>();
        clusterMissile.target = target;
        clusterMissile.origin = origin;
        clusterMissile.missileDamage = damage;

        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

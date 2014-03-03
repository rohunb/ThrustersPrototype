using UnityEngine;
using System.Collections;

public class Weapon_ClusterMissile : Weapon {
    

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
        clusterMissileClone.rigidbody.AddForce(transform.up * 50f, ForceMode.Impulse);
        clusterMissileClone.GetComponent<HomingMissile>().target = target;
        ClusterMissile clusterMissile = clusterMissileClone.GetComponent<ClusterMissile>();
        clusterMissile.target = target;
        clusterMissile.origin = origin;
        clusterMissile.missileDamage = damage;

        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
}

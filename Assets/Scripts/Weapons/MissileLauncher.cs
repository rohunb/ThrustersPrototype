using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

    public GameObject missile;
    public GameObject clusterMissile;
    public Transform shootPoint;

    public void FireMissile(int damage, Transform _target,GameObject _origin)
    {
        GameObject missileClone = Instantiate(missile, shootPoint.position, shootPoint.rotation) as GameObject;

        missileClone.rigidbody.AddForce(transform.up * 100f, ForceMode.Impulse);
        //missileClone.GetComponent<HomingMissile>().target = _target;
        missileClone.GetComponent<ProjectileDamager>().origin = _origin;
        missileClone.GetComponent<ProjectileDamager>().damage = damage;

    }
    public void FireClusterMissile(int damage, Transform _target,GameObject _origin)
    {
        GameObject clusterMissileClone = Instantiate(clusterMissile, shootPoint.position, shootPoint.rotation) as GameObject;
        clusterMissileClone.rigidbody.AddForce(transform.up * 50f, ForceMode.Impulse);
        clusterMissileClone.GetComponent<HomingMissile>().target = _target;
        clusterMissileClone.GetComponent<ClusterMissile>().target = _target;
        clusterMissileClone.GetComponent<ClusterMissile>().origin = _origin;
    }
}

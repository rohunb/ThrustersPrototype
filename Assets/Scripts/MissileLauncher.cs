using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

    public GameObject missile;
    public GameObject clusterMissile;
    public Transform shootPoint;

    public void FireMissile(float damage, Transform _target)
    {
        GameObject missileClone = Instantiate(missile, shootPoint.position, shootPoint.rotation) as GameObject;
        missile.GetComponent<HomingMissile>().target = _target;
    }
    public void FireClusterMissile(float damage, Transform _target)
    {
        GameObject clusterMissileClone = Instantiate(clusterMissile, shootPoint.position, shootPoint.rotation) as GameObject;
        clusterMissile.GetComponent<ClusterMissile>().target = _target;
    }
}

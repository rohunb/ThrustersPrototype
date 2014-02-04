using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

    public GameObject missile;
    public Transform shootPoint;

    public void Fire(float damage, Transform _target)
    {
        GameObject missileClone = Instantiate(missile, shootPoint.position, shootPoint.rotation) as GameObject;
        missile.GetComponent<HomingMissile>().target = _target;
    }
}

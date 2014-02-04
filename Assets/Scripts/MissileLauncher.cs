using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

    public GameObject missile;
    public Transform shootPoint;

    public void Fire(float damage)
    {
        Instantiate(missile, shootPoint.position, shootPoint.rotation);
    }
}

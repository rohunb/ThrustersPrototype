using UnityEngine;
using System.Collections;

public class Railgun : MonoBehaviour
{

    public GameObject rail;
    public Transform shootPoint;

    public float railEffectRotSpeed;
    public float railEffectClearTimer;
    LineRenderer line;

    public void Fire(int _damage, float _speed, GameObject _origin)
    {
        GameObject railshotClone = Instantiate(rail, shootPoint.position, shootPoint.rotation) as GameObject;
        railshotClone.GetComponent<ProjectileMover>().speed = _speed;
        ProjectileDamager damager = railshotClone.GetComponent<ProjectileDamager>();
        damager.origin = _origin;
        damager.damage = _damage;

    }
}
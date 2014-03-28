using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public string name;
    public int damage;
    public GameObject projectile;
    public float projectileSpeed;
    public float reloadTimer;
    public Transform shootPoint;
    public Transform target;
    public GameObject origin;
    public float range;
    protected bool canFire = true;
    public int level;
    public bool lookAtMouse;
    public int cost = 40;
    public enum WeaponType { Primary,Secondary,Tertiary,Utility}
    public WeaponType weaponType;

    public Transform movableGun;

	
    public virtual void Fire()
    {
        
    }
    protected virtual void Update()
    {
        if (lookAtMouse && Camera.main)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
            //movableGun.LookAt(mousePos);
            transform.LookAt(mousePos);

        }
    }
}

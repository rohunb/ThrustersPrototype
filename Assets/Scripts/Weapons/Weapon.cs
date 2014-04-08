using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public string wpnName;
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
	public Texture2D crosshairTex;
    public Transform movableGun;

    public virtual void Init() 
	{ 
//		crosshair xhair = gameObject.AddComponent<crosshair>();
//		xhair.crosshairTex = crosshairTex;
//		xhair.weapon = transform;

	}
    public virtual void Fire()
    {
        
    }
    protected virtual void Update()
    {
        if (lookAtMouse && Camera.main)
        {
			if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
			{

				transform.localRotation = SixenseInput.Controllers[1].Rotation;
				//transform.rotation=Quaternion.identity;
//				Quaternion rot = SixenseInput.Controllers[1].Rotation;
//				rot.eulerAngles= Quaternion.Euler(
				//transform.Rotate(0f,-90f,0);
			}
			else
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
				transform.LookAt(mousePos);
			}
        }
    }
}

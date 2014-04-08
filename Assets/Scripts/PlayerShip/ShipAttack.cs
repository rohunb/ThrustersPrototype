using UnityEngine;
using System.Collections;

public class ShipAttack : MonoBehaviour {

    //access inventory
    PlayerInventory inventory;

    //targetting
    public float minTargetableDistance = 2000f;
    public Transform targeter;
    public Texture2D crosshair;
    public Texture2D targetBoxTexture;
    public Texture2D targetLeadTexture;
    private Transform target;
    private Vector3 targetLead;

	float xMin = 0, yMin = 0;

	// Use this for initialization
    void Start()
    {
        Screen.showCursor = false;
        inventory = gameObject.GetComponent<PlayerInventory>();
        target = null;

    }
	
	// Update is called once per frame
	void Update () {

//		if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
//		{
//			GameObject go = GameObject.Find("FakeMousePointer");
//			
//			go.transform.rotation = SixenseInput.Controllers[1].Rotation;
//
//			Ray rayCharles = new Ray(go.transform.position, go.transform.forward);
//			RaycastHit hit;
//			
//			bool didIHitAnything = Physics.Raycast(rayCharles, out hit);
//
//			hit.point = Camera.main.WorldToScreenPoint(hit.point);
//
//			xMin = (hit.point.x / Screen.width);
//			yMin = (hit.point.y / Screen.height);
//			
//			Debug.DrawLine(rayCharles.origin, hit.point);
//			
//		}

        if(target)
        {
            foreach (Weapon weapon in inventory.equippedWeapons)
            {
                if (weapon.weaponType == Weapon.WeaponType.Secondary)
                    weapon.target = target;
            }
            float distToTarget = Vector3.Distance(transform.position, target.position);
            float minSpeed=0;
            int i = 0;
            while(i<inventory.equippedWeapons.Length)
            {
                if (inventory.equippedWeapons[i].weaponType == Weapon.WeaponType.Primary)
                {
                    minSpeed = inventory.equippedWeapons[i].projectileSpeed;
                    break;
                }
                i++;
            }
            while(i<inventory.equippedWeapons.Length)
            {
                if (inventory.equippedWeapons[i].weaponType == Weapon.WeaponType.Primary
                    && inventory.equippedWeapons[i].projectileSpeed<minSpeed)
                {
                    minSpeed = inventory.equippedWeapons[i].projectileSpeed;
                }
                i++;
            }
            float timeToTarget = distToTarget / minSpeed;
            targetLead = target.position + target.rigidbody.velocity * timeToTarget;

        }
    
    }
    public void FirePrimary()
    {
        foreach (Weapon weapon in inventory.equippedWeapons)
        {
            if (weapon && weapon.weaponType == Weapon.WeaponType.Primary)
                weapon.Fire();
        }
    }
    public void FireSecondary()
    {
        foreach (Weapon weapon in inventory.equippedWeapons)
        {
            if (weapon && weapon.weaponType == Weapon.WeaponType.Secondary)
                weapon.Fire();
        }
    }
    public void FireTertiary()
    {
        foreach (Weapon weapon in inventory.equippedWeapons)
        {
            if (weapon && weapon.weaponType == Weapon.WeaponType.Tertiary)
                weapon.Fire();
        }
    }
    public void FireUtility()
    {
        foreach (Weapon weapon in inventory.equippedWeapons)
        {
            if (weapon && weapon.weaponType == Weapon.WeaponType.Utility)
                weapon.Fire();
        }
    }
    public void StopFiringUtility()
    {
        foreach (Weapon weapon in inventory.equippedWeapons)
        {
            if (weapon is Weapon_MiningLaser)
            {
                Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)(weapon);
                miningLaser.StopFiring();
            }
        }
    }
    public void TargetNextEnemy()
    {
        if (!target)
        {
            TargetNearestEnemy();

        }
        else
        {
            for (int i = 0; i < EnemyController.enemies.Count; i++)
            {
                if (target.gameObject == EnemyController.enemies[i])
                {
                    if (i == EnemyController.enemies.Count - 1)
                        target = EnemyController.enemies[0].transform;
                    else
                        target = EnemyController.enemies[i + 1].transform;
                    break;
                }
            }


        }
    }

    public void TargetNearestEnemy()
    {
        GameObject[] enemies = EnemyController.enemies.ToArray();
        float minDistance;
        if (enemies.Length > 0)
        {
            minDistance = Vector3.Distance(enemies[0].transform.position, transform.position);
            if (minDistance < minTargetableDistance)
                target = enemies[0].transform;
            float distance = 0;
            foreach (GameObject enemy in enemies)
            {
                distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance && distance < minTargetableDistance)
                {
                    target = enemy.transform;
                    minDistance = distance;
                }
            }
        }
        
    }
    void OnGUI()
    {
        
        if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.MOUSE_KEYBOARD)
        {
            xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2 / 10);
            yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);

			GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);
        }
        else if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
        {

        }

        GUILayout.BeginArea(new Rect(10, Screen.height - 140, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Afterburner: " + gameObject.GetComponent<ShipMove>().currentAfterburnerLevel);
        GUILayout.Label("Health: " + gameObject.GetComponent<Health>().health);
        GUILayout.Label("Shield: " + gameObject.GetComponent<Health>().shieldStrength);
        GUILayout.Label("Credits: " + gameObject.GetComponent<PlayerInventory>().GetCredits());
        if (target)
        {
            GUILayout.Label("Target: " + target.name.ToString());
            GUILayout.Label("Angle to Target: " + Vector3.Angle(targeter.forward, target.position - transform.position).ToString());
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (target)
        {
            float size = 10000 / Vector3.Distance(transform.position, target.position);
            size = Mathf.Clamp(size, 45f, 112f);
            Vector3 position = Camera.main.WorldToScreenPoint(target.position);
            position.y = Screen.height - position.y;
            position.x = Mathf.Clamp(position.x, 0, Screen.width - size / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - size / 2);
            if (Vector3.Angle(targeter.forward, target.position - transform.position) > 90)
            {
                if (position.x >= Screen.width / 2)
                    position.x = Mathf.Clamp(position.x, Screen.width - size / 2, Screen.width - size / 2);
                else
                    position.x = Mathf.Clamp(position.x, 0, 0);
                if (position.y >= Screen.height / 2)
                    position.y = Mathf.Clamp(position.y, Screen.height - size / 2, Screen.height - size / 2);
                else
                    position.y = Mathf.Clamp(position.y, 0, 0);

            }
            //if (showTargetLead)
            {

                Vector3 leadPos = Camera.main.WorldToScreenPoint(targetLead);
                float leadSize = 15f;
                leadPos.y = Screen.height - leadPos.y;
                leadPos.x = Mathf.Clamp(leadPos.x, 0, Screen.width - leadSize / 2);
                leadPos.y = Mathf.Clamp(leadPos.y, 0, Screen.height - leadSize / 2);
                if (Vector3.Angle(targeter.forward, target.position - transform.position) < 90)
                {
                    GUI.DrawTexture(new Rect((leadPos.x - (leadSize / 2)), (leadPos.y - (leadSize / 2)), leadSize, leadSize), targetLeadTexture);
                }
            }
            GUI.DrawTexture(new Rect((position.x - (size / 2)), (position.y - (size / 2)), size, size), targetBoxTexture);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2)), size * 20, size * 2), "Target = " + target.name);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 15), size * 20, size * 2), "Health = " + target.gameObject.GetComponent<Health>().health);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 30), size * 20, size * 2), "Shield = " + target.gameObject.GetComponent<Health>().shieldStrength);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 45), size * 20, size * 2), "Dist = " + Vector3.Distance(transform.position, target.position));
        }
    }


}

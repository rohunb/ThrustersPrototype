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
    public Transform mousePointTargeter;

	float xMin = 0, yMin = 0;

    public float aimNearTargetDist = 10f;
    bool aimNearTarget = false;

	// Use this for initialization
    void Start()
    {
        Screen.showCursor = true;
        inventory = gameObject.GetComponent<PlayerInventory>();
        target = null;

    }
	
	// Update is called once per frame
    void Update()
    {

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

        if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.MOUSE_KEYBOARD)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
            foreach (Weapon weapon in inventory.equippedWeapons)
            {
                weapon.AimAt(mousePos);
            }
            xMin = Input.mousePosition.x - (crosshair.width / 2 / 10);
            yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);

            //Vector3 screenHitPos = Vector3.zero;
            //Ray ray = new Ray(mousePointTargeter.position, mousePointTargeter.forward);
            //Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.yellow);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
            //{
            //    //screenHitPos = targeter.InverseTransformPoint(hit.point);
            //    screenHitPos = hit.point;
            //    foreach (Weapon weapon in inventory.equippedWeapons)
            //    {
            //        weapon.AimAt(screenHitPos);
            //    }
            //    //Debug.Log(screenHitPos);
            //    //Debug.Log(screenHitPos);
            //}
            //if (Camera.main)
            //{

            //    Vector3 targetedPos = Camera.main.WorldToScreenPoint(screenHitPos);
            //    xMin = targetedPos.x;
            //    yMin = targetedPos.y;
            //    //Debug.Log("xMin, yMin: " + new Vector2(xMin, yMin));
            //}


			
            

        }
		else if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
		{

			Vector3 screenHitPos = Vector3.zero;
			Ray ray = new Ray(mousePointTargeter.position, mousePointTargeter.forward);
			Debug.DrawRay(ray.origin, ray.direction * 10000f,Color.yellow);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
			{
				//screenHitPos = targeter.InverseTransformPoint(hit.point);
				screenHitPos = hit.point;
                foreach (Weapon weapon in inventory.equippedWeapons)
                {
                    weapon.AimAt(screenHitPos);
                }
				//Debug.Log(screenHitPos);
				//Debug.Log(screenHitPos);
			}
			if(Camera.main)
			{

				Vector3 targetedPos = Camera.main.WorldToScreenPoint(screenHitPos);
				xMin = targetedPos.x;
				yMin = targetedPos.y;
				//Debug.Log("xMin, yMin: " + new Vector2(xMin, yMin));
			}

		}

		if (target)
		{
			foreach (Weapon weapon in inventory.equippedWeapons)
			{
				if (weapon && (weapon.weaponType == Weapon.WeaponType.Secondary || weapon is Weapon_Lasers))
					weapon.target = target;
			}
			float distToTarget = Vector3.Distance(transform.position, target.position);
			float minSpeed = 0;
			int i = 0;
			while (i < inventory.equippedWeapons.Length)
			{
				if (inventory.equippedWeapons[i].weaponType == Weapon.WeaponType.Primary)
				{
					minSpeed = inventory.equippedWeapons[i].projectileSpeed;
					break;
				}
				i++;
			}
			while (i < inventory.equippedWeapons.Length)
			{
				if ( inventory.equippedWeapons[i] && inventory.equippedWeapons[i].weaponType == Weapon.WeaponType.Primary
				    && inventory.equippedWeapons[i].projectileSpeed < minSpeed)
				{
					minSpeed = inventory.equippedWeapons[i].projectileSpeed;
				}
				i++;
			}
			float timeToTarget = distToTarget / minSpeed;
			targetLead = target.position + target.rigidbody.velocity * timeToTarget;
			
		}

    }
    void OnGUI()
    {

        if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.MOUSE_KEYBOARD)
        {
            //xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2 / 10);
            //yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);

            //GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);

            GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);

            //GUI.DrawTexture(new Rect(xMin - (crosshair.width / 20), Screen.height - yMin - (crosshair.width / 20), crosshair.width / 10, crosshair.height / 10), crosshair);

        }
        else if (GOD.whatControllerAmIUsing == WhatControllerAmIUsing.HYDRA)
        {
            GUI.DrawTexture(new Rect(xMin, Screen.height - yMin, crosshair.width / 10, crosshair.height / 10), crosshair);
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
    


}

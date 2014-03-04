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

	// Use this for initialization
    void Start()
    {
        Screen.showCursor = false;
        inventory = gameObject.GetComponent<PlayerInventory>();
        target = null;

    }
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetButton("Fire1"))
        //{
        //    inventory.primaryWeapon.Fire();
        //}
        //if(Input.GetButton("Fire2"))
        //{
        //    inventory.secondaryWeapon.Fire();
        //}
        //if(Input.GetKey(KeyCode.Alpha1))
        //{
        //    inventory.tertiaryWeapon.Fire();
        //}
        ////mining laser is a constant beam 
        //if(Input.GetKey(KeyCode.Alpha2))
        //{
        //    inventory.utilityWeapon.Fire();
        //}
        //else if (inventory.utilityWeapon is Weapon_MiningLaser)
        //{
        //    Weapon utlityWeapon = inventory.utilityWeapon;
        //    Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)(utlityWeapon);
        //    miningLaser.StopFiring();
        //}
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    TargetNextEnemy();
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //    TargetNearestEnemy();

	    if(target)
        {
            inventory.secondaryWeapon.target = target;
            float distToTarget = Vector3.Distance(transform.position, target.position);
            float timeToTarget = distToTarget / inventory.primaryWeapon.projectileSpeed;
            targetLead = target.position + target.rigidbody.velocity * timeToTarget;
        }
    
    }
    public void FirePrimary()
    {
        inventory.primaryWeapon.Fire();
    }
    public void FireSecondary()
    {
        inventory.secondaryWeapon.Fire();
    }
    public void FireTertiary()
    {
        inventory.tertiaryWeapon.Fire();
    }
    public void FireUtility()
    {
        inventory.utilityWeapon.Fire();
    }
    public void StopFiringUtility()
    {
        if (inventory.utilityWeapon is Weapon_MiningLaser)
        {
            Weapon utlityWeapon = inventory.utilityWeapon;
            Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)(utlityWeapon);
            miningLaser.StopFiring();
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
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShip");
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
        float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshair.width / 2 / 10);
        float yMin = (Screen.height - Input.mousePosition.y) - (crosshair.height / 2 / 10);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width / 10, crosshair.height / 10), crosshair);


        GUILayout.BeginArea(new Rect(10, Screen.height - 140, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Afterburner: " + gameObject.GetComponent<ShipMove>().currentAfterburnerLevel);
        GUILayout.Label("Health: " + gameObject.GetComponent<Health>().health);
        GUILayout.Label("Shield: " + gameObject.GetComponent<Health>().shieldStrength);
        //GUILayout.Label("Weapon: " + currentWeapon.ToString());
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

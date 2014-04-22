using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    public static List<GameObject> enemies;
    public GameObject explosion;
    public GameObject enemy;
    [SerializeField]
    private Transform playerTarget;
    private MissionController missionController;
	// Use this for initialization
	void Start () {
        playerTarget = GameObject.FindGameObjectWithTag("ShipTargeter").transform;
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("EnemyShip");
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
        enemies = new List<GameObject>();
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
            enemy.GetComponent<AI_Controller>().target = playerTarget;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (enemies.Count > 0)
        {
            foreach (GameObject enemy in enemies.ToArray())
            {

                Transform enemyTarget = enemy.GetComponent<AI_Controller>().target;
                float distToPlayer = Vector3.Distance(enemy.transform.position, playerTarget.position);
                if (enemyTarget != playerTarget && distToPlayer <= enemy.GetComponent<AI_Controller>().sightRange)
                {
                    enemy.GetComponent<AI_Controller>().target = playerTarget;
                }
                if (enemy.GetComponent<Health>().health <= 0)
                {
                    Instantiate(explosion, enemy.transform.position, Quaternion.identity);
                    enemies.Remove(enemy);
                    if (missionController.missionEnemies.Contains(enemy))
                        missionController.missionEnemies.Remove(enemy);
                    GameObject.Destroy(enemy);
					GOD.audioengine.playSFX("ShipExplosion");
                }
            }
        }
	}
    //void OnGUI()
    //{
        //GUILayout.BeginArea(new Rect(Screen.width-150, 10, 150, 150));
        //GUILayout.BeginVertical();
        //GUILayout.Label("Num Enemies: " + enemies.Count);
        //GUILayout.EndVertical();
        //GUILayout.EndArea();
    //}
    public void SpawnEnemy(int numEnemies, Vector3 spawnLoc, AI_Controller.AI_Types _ai_type, Transform _target)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnPos = spawnLoc + Random.onUnitSphere * 500;
            GameObject enemyClone = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
            enemyClone.GetComponent<AI_Controller>().ai_type = _ai_type;
            enemyClone.GetComponent<AI_Controller>().target = _target;
            enemyClone.transform.LookAt(_target);
            enemies.Add(enemyClone);
            missionController.missionEnemies.Add(enemyClone);
            
        }
    }
    public void DespawnEnemy(GameObject _enemy)
    {
        foreach (GameObject enemy in enemies.ToArray())
        {
            enemies.Remove(enemy);
            GameObject.Destroy(enemy);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    public static List<GameObject> enemies;
    public GameObject explosion;
    public GameObject enemy;
    private Transform target;
    private MissionController missionController;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("PlayerShip").transform;
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("EnemyShip");
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
        enemies = new List<GameObject>();
        foreach (GameObject enemy in enemiesArray)
        {
            enemies.Add(enemy);
        }

	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject enemy in enemies.ToArray())
        {

            if(enemy.GetComponent<Health>().health<=0)
            {
                Instantiate(explosion, enemy.transform.position, Quaternion.identity);
                enemies.Remove(enemy);
                if (missionController.missionEnemies.Contains(enemy))
                    missionController.missionEnemies.Remove(enemy);
                GameObject.Destroy(enemy);
            }
        }
	}
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width-150, 10, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Num Enemies: " + enemies.Count);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    public void SpawnEnemy(int numEnemies, Vector3 spawnLoc, AI_Controller.AI_Types _ai_type)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 spawnPos = spawnLoc + Random.onUnitSphere * 50;
            GameObject enemyClone = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
            enemies.Add(enemyClone);
            missionController.missionEnemies.Add(enemyClone);
            enemyClone.GetComponent<AI_Controller>().ai_type = _ai_type;
            enemyClone.GetComponent<AI_Controller>().target = this.target;
            enemyClone.transform.LookAt(target);
        }
    }
}

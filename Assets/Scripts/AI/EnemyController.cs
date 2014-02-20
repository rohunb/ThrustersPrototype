using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    public static List<GameObject> enemies;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("EnemyShip");
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
}

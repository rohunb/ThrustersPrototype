using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    public static List<GameObject> enemies;
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

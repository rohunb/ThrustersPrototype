using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionController : MonoBehaviour {

	public enum MissionType
    {
        Exterminate, //x number of ships at waypoint
        Assassinate, //kill a particular ship (likely to spawn with cronies)
        Gather, //mine or gather an item carried by an enemy
        FedEx, //deliver something to another base/planet
        DistressCall
    }
    public MissionType missionType;
    public Transform waypoint;
    public Vector3 enemySpawnPoint;
    public GameObject enemyToSpawn;
    public int enemiesToKill;
    public string missionText;
    public int enemiesLeftToKill;
    private EnemyController enemyController;
    public bool missionComplete = false;
    public List<GameObject> missionEnemies;

    // Use this for initialization
	void Start () {
        enemyController = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        missionEnemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (missionType)
        {
            case MissionType.Exterminate:
                if (missionEnemies.Count <= 0)
                    missionComplete = true;
                break;
            case MissionType.Assassinate:
                break;
            case MissionType.Gather:
                break;
            case MissionType.FedEx:
                break;
            case MissionType.DistressCall:
                break;
            default:
                break;
        }

	}
    public void GenerateExterminateMission(MissionType missionType,Transform _waypoint, Vector3 _enemySpawnPoint, int _enemiesToKill, AI_Controller.AI_Types _ai_type)
    {
        this.missionType = missionType;
        enemyController.SpawnEnemy(_enemiesToKill, _enemySpawnPoint, _ai_type);
        missionComplete = false;
        enemiesLeftToKill = _enemiesToKill;
    }
}

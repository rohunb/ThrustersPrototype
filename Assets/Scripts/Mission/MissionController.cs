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
        DistressCall,
        DestroyStructure,
        Race
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
    public bool missionFailed = false;
    public List<GameObject> missionEnemies;
    public Transform player;
    public GameObject victimPrefab;
    public GameObject currentVictim;
    public GameObject enemyStructPrefab;
    public GameObject currentEnemyStruct;
    public GameObject waypointPrefab;

    // for race
    public GameObject raceWaypointGameObject;
    public GameObject activeRaceWaypoint;
    public int remainingRaceWaypoints;
    public float raceTimer = 0.0f;

    // Use this for initialization
    void Start () {
        enemyController = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("PlayerShip").transform;
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
                if (missionEnemies.Count <= 0)
                    missionComplete = true;
                if (currentVictim && currentVictim.GetComponent<Health>().health <= 0)
                {
                    missionFailed = true;
                    CancelMission();
                }
                break;
            case MissionType.DestroyStructure:
                if (missionEnemies.Count <= 0 && !currentEnemyStruct)
                    missionComplete = true;
                break;
            case MissionType.Race:
                if (!missionComplete)
                    raceTimer += Time.deltaTime;
                break;
            default:
                break;
        }
    }

    public void CancelMission()
    {
        switch (missionType)
        {
            case MissionType.Exterminate:
                DespawnMissionEnemies();
                break;
            case MissionType.Assassinate:
                break;
            case MissionType.Gather:
                break;
            case MissionType.FedEx:
                break;
            case MissionType.DistressCall:
                DespawnMissionEnemies();
                GameObject.Destroy(currentVictim);
                break;
            case MissionType.DestroyStructure:
                break;
			case MissionType.Race:
				break;
            default:
                break;
        }
    }
    public void GenerateExterminateMission(MissionType missionType,Transform _waypoint, Vector3 _enemySpawnPoint, int _enemiesToKill, AI_Controller.AI_Types _ai_type)
    {
        this.missionType = missionType;
        enemyController.SpawnEnemy(_enemiesToKill, _enemySpawnPoint, _ai_type,player);
        missionComplete = false;
        missionFailed = false;
        enemiesLeftToKill = _enemiesToKill;
    }
    public void GenerateDistressMission(MissionType missionType, Transform _waypoint, Vector3 _victimPos, int _enemiesToKill, AI_Controller.AI_Types _ai_type)
    {
        this.missionType = missionType;
        currentVictim = Instantiate(victimPrefab, _victimPos, Quaternion.identity) as GameObject;
        enemyController.SpawnEnemy(_enemiesToKill, _victimPos, _ai_type, currentVictim.transform);
        missionComplete = false;
        missionFailed = false;
        enemiesLeftToKill = _enemiesToKill;
    }
    public void GenerateDestroyStructureMission(MissionType missionType, Transform _waypoint, Vector3 _structurePos, int _enemiesToKill, AI_Controller.AI_Types _ai_type)
    {
        this.missionType = missionType;
        currentEnemyStruct = Instantiate(enemyStructPrefab, _structurePos, Quaternion.identity) as GameObject;
        enemyController.SpawnEnemy(_enemiesToKill, _structurePos, _ai_type, player);
        missionComplete = false;
        missionFailed = false;
        enemiesLeftToKill = _enemiesToKill;
    }

    public void GenerateRaceMission(MissionType missionType, int raceTotalWaypoints) {
        this.missionType = missionType;
        missionComplete = false;
        missionFailed = false;

        remainingRaceWaypoints = raceTotalWaypoints;
        CreateRaceWaypoint(player.Find("Targeter").transform.position);
    }

    public void RaceWaypointHit(GameObject theWaypoint) {
        remainingRaceWaypoints--;

        if (remainingRaceWaypoints == 0) {
            missionComplete = true;
            print("Race Finished! End time: " + raceTimer);
            // award the player with credits based on their time
        } else {
            CreateRaceWaypoint(theWaypoint.transform.position);
        }
    }

    public void CreateRaceWaypoint(Vector3 prevWaypoint) {
        int rndRange = 500;
        int distanceMax = 1000;
        Vector3 newWaypoint = prevWaypoint + (player.Find("Targeter").transform.forward * Random.Range(500, distanceMax) + new Vector3(Random.Range(-rndRange, rndRange), Random.Range(-rndRange, rndRange), Random.Range(-rndRange, rndRange)));
        activeRaceWaypoint = Instantiate(raceWaypointGameObject, newWaypoint, Quaternion.identity) as GameObject;
        player.GetComponent<PlayerMissionController>().UpdateCurrentWaypoint(activeRaceWaypoint.transform);
    }

    public void DespawnMissionEnemies() {
        foreach (GameObject enemy in missionEnemies.ToArray())
        {
            enemyController.DespawnEnemy(enemy);
            missionEnemies.Remove(enemy);
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerMissionController : MonoBehaviour
{
    public GUISkin guiSkin;
    public Transform waypointExterminate;
    public Transform waypointDistress;
    public Transform waypointDestroyStruct;
    private Transform currentWP;
    public Texture2D waypointIndicatorTexture;
    public Texture2D victimIndicatorTexture;
    public Texture2D enemyStructIndicatorTexture;
    public MissionController.MissionType currentMission;
    public int numEnemiesToSpawn;
    public bool displayWaypoint = false;
    public MissionController missionController;
    public Transform enemySpawnPoint;
    public Transform victimSpawnPoint;
    public Transform enemyStructSpawnPoint;
    public bool onMission = false;
    public Transform targeter;
    private bool showMissionCompleteText = false;
    private bool showMissionFailedText = false;
    private GameObject specialTarget;
    private Texture2D specialTargetTexture;
    private bool showSpecialTargetBox;

	private string playerMission;

    // Use this for initialization
    void Start()
    {
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!onMission)
        {
            if (Input.GetKeyDown(KeyCode.F1))
                GenerateNewExterminateMission();
            if (Input.GetKeyDown(KeyCode.F2))
                GenerateNewDistressMission();
            if (Input.GetKeyDown(KeyCode.F3))
                GenerateNewDestroyStructureMission();
        }

        if (onMission)
        {
            if (missionController.missionComplete)
            {
                showMissionCompleteText = true;
                ClearMissionInfo();
            }
            if (missionController.missionFailed)
            {
                showMissionFailedText = true;
                ClearMissionInfo();
            }
        }
        if (displayWaypoint && Vector3.Distance(transform.position, currentWP.position) <= 700f)
            displayWaypoint = false;
    }
    public void GenerateNewExterminateMission()
    {
        missionController.GenerateExterminateMission(MissionController.MissionType.Exterminate, waypointExterminate, enemySpawnPoint.position, numEnemiesToSpawn, AI_Controller.AI_Types.Assassin);
        SetMissionInfo(waypointExterminate, null, null);
    }
    public void GenerateNewDistressMission()
    {
        missionController.GenerateDistressMission(MissionController.MissionType.DistressCall, waypointDistress, victimSpawnPoint.position, numEnemiesToSpawn, AI_Controller.AI_Types.FlyBy);
        SetMissionInfo(waypointDistress, missionController.currentVictim, victimIndicatorTexture);
    }
    public void GenerateNewDestroyStructureMission()
    {
        missionController.GenerateDestroyStructureMission(MissionController.MissionType.DestroyStructure, waypointDestroyStruct, enemyStructSpawnPoint.position, numEnemiesToSpawn, AI_Controller.AI_Types.Assassin);
        SetMissionInfo(waypointDestroyStruct, missionController.currentEnemyStruct, enemyStructIndicatorTexture);
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, Screen.height - 200, 150, 150));
        GUILayout.BeginVertical();
        if (onMission)
        {
            
            switch (missionController.missionType)
            {
                case MissionController.MissionType.Exterminate:
                    
                    GUILayout.Label("Mission: Exterminate 4 enemy ships");
                    GUILayout.Label("Enemies left: " + missionController.missionEnemies.Count);
                    break;
                case MissionController.MissionType.Assassinate:
                    break;
                case MissionController.MissionType.Gather:
                    break;
                case MissionController.MissionType.FedEx:
                    break;
                case MissionController.MissionType.DistressCall:
                    GUILayout.Label("Mission: Respond to the distress call");
                    if (missionController.currentVictim)
                    {
                        GUILayout.Label("Victim Health: " + missionController.currentVictim.GetComponent<Health>().health);
                        GUILayout.Label("Victim Shields: " + missionController.currentVictim.GetComponent<Health>().shieldStrength);
                    }
                    GUILayout.Label("Enemies left: " + missionController.missionEnemies.Count);
                    break;
                case MissionController.MissionType.DestroyStructure:
                    GUILayout.Label("Mission: Destroy the enemy's space station");
                    if (missionController.currentEnemyStruct)
                    {
                        GUILayout.Label("Station Health: " + missionController.currentEnemyStruct.GetComponent<Health>().health);
                        GUILayout.Label("Station Shields: " + missionController.currentEnemyStruct.GetComponent<Health>().shieldStrength);
                    }
                    GUILayout.Label("Enemies left: " + missionController.missionEnemies.Count);
                    break;

                default:
                    break;
            }

        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        
        if(specialTarget && Vector3.Distance(transform.position, specialTarget.transform.position) < 3000f) 
        {
            showSpecialTargetBox = true;
        }
        if(showSpecialTargetBox && specialTarget)
        {
            float vSize = 10000 / Vector3.Distance(transform.position, specialTarget.transform.position);
            vSize = Mathf.Clamp(vSize, 45f, 112f);
            Vector3 position = Camera.main.WorldToScreenPoint(specialTarget.transform.position);
            position.y = Screen.height - position.y;
            position.x = Mathf.Clamp(position.x, 0, Screen.width - vSize / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - vSize / 2);
            if (Vector3.Angle(targeter.forward, specialTarget.transform.position - transform.position) > 90)
            {
                if (position.x >= Screen.width / 2)
                    position.x = Mathf.Clamp(position.x, Screen.width - vSize / 2, Screen.width - vSize / 2);
                else
                    position.x = Mathf.Clamp(position.x, 0, 0);
                if (position.y >= Screen.height / 2)
                    position.y = Mathf.Clamp(position.y, Screen.height - vSize / 2, Screen.height - vSize / 2);
                else
                    position.y = Mathf.Clamp(position.y, 0, 0);

            }
            GUI.DrawTexture(new Rect((position.x - (vSize / 2)), (position.y - (vSize / 2)), vSize, vSize), specialTargetTexture);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2)), vSize * 20, vSize * 2), "Target = " + specialTarget.name);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 15), vSize * 20, vSize * 2), "Health = " + specialTarget.GetComponent<Health>().health);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 30), vSize * 20, vSize * 2), "Shield = " + specialTarget.GetComponent<Health>().shieldStrength);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 45), vSize * 20, vSize * 2), "Dist = " + Vector3.Distance(transform.position, specialTarget.transform.position));
        }
      
        if (showMissionCompleteText)
        {
            GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height / 2 - 100, 300, 100), "<color=green><size=32>Mission Complete!</size></color>");
        }
        if (showMissionFailedText)
        {
            GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height / 2 - 100, 300, 100), "<color=red><size=32>Mission Failed!</size></color>");
        }
        if (displayWaypoint)
        {
            float size = 10000 / Vector3.Distance(transform.position, currentWP.position);
            size = Mathf.Clamp(size, 45f, 212f);
            Vector3 position = Camera.main.WorldToScreenPoint(currentWP.position);
            position.y = Screen.height - position.y;
            position.x = Mathf.Clamp(position.x, 0, Screen.width - size / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - size / 2);
            if (Vector3.Angle(targeter.forward, currentWP.position - transform.position) > 90)
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
            GUI.DrawTexture(new Rect((position.x - (size / 2)), (position.y - (size / 2)), size, size), waypointIndicatorTexture);
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 45), size * 20, size * 2), "Dist = " + Vector3.Distance(transform.position, currentWP.position));
        }
    }
    void CancelMissionText()
    {
        showMissionCompleteText = false;
        showMissionFailedText = false;
    }
    
    void SetMissionInfo(Transform _waypoint, GameObject _specialTarget, Texture2D _specialTargetTexture)
    {
        specialTarget = _specialTarget;
        specialTargetTexture = _specialTargetTexture;
        showSpecialTargetBox = false;
        currentWP = _waypoint;
        onMission = true;
        displayWaypoint = true;
        
    }
    void ClearMissionInfo()
    {
        specialTarget = null;
        specialTargetTexture = null;
        showSpecialTargetBox = false;
        currentWP = null;
        onMission = false;
        displayWaypoint = false;
        Invoke("CancelMissionText", 2.0f);
        
    }
                
}

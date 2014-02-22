using UnityEngine;
using System.Collections;

public class PlayerMissionController : MonoBehaviour
{

    public Transform waypointExterminate;
    public Transform waypointDistress;
    private Transform currentWP;
    public Texture2D waypointIndicatorTexture;
    public Texture2D victimIndicatorTexture;
    public MissionController.MissionType currentMission;
    public bool displayWaypoint = false;
    public MissionController missionController;
    public Transform enemySpawnPoint;
    public Transform victimSpawnPoint;
    public bool onMission = false;
    public Transform targeter;
    private bool showMissionCompleteText = false;
    private bool showMissionFailedText = false;
    // Use this for initialization
    void Start()
    {
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            GenerateNewExterminateMission();
        if (Input.GetKeyDown(KeyCode.F2))
            GenerateNewDistressMission();


        if (onMission)
        {
            if (missionController.missionComplete)
            {
                onMission = false;
                showMissionCompleteText = true;
                displayWaypoint = false;
                Invoke("CancelMissionText", 2.0f);
            }
            if (missionController.missionFailed)
            {
                onMission = false;
                showMissionFailedText = true;
                displayWaypoint = false;
                Invoke("CancelMissionText", 2.0f);
            }
        }
        if (displayWaypoint && Vector3.Distance(transform.position, currentWP.position) <= 500f)
            displayWaypoint = false;
    }
    void GenerateNewExterminateMission()
    {
        missionController.GenerateExterminateMission(MissionController.MissionType.Exterminate, waypointExterminate, enemySpawnPoint.position, 4, AI_Controller.AI_Types.Assassin);
        currentWP = waypointExterminate;
        onMission = true;
        displayWaypoint = true;
    }
    void GenerateNewDistressMission()
    {
        missionController.GenerateDistressMission(MissionController.MissionType.DistressCall, waypointDistress, victimSpawnPoint.position, 5, AI_Controller.AI_Types.FlyBy);
        currentWP = waypointDistress;
        onMission = true;
        displayWaypoint = true;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, Screen.height - 200, 150, 150));
        GUILayout.BeginVertical();
        //GUILayout.Label("Weapon: " + currentWeapon.ToString());
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
                default:
                    break;
            }

        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        if (onMission && missionController.missionType == MissionController.MissionType.DistressCall && missionController.currentVictim && Vector3.Distance(transform.position, missionController.currentVictim.transform.position) < 3000f)
        {
            
            //show box around victim
            GameObject victim = missionController.currentVictim;
            float vSize =10000 / Vector3.Distance(transform.position, victim.transform.position);
            vSize = Mathf.Clamp(vSize, 45f, 112f);
            Vector3 position = Camera.main.WorldToScreenPoint(victim.transform.position);
            position.y = Screen.height - position.y;
            position.x = Mathf.Clamp(position.x, 0, Screen.width - vSize / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - vSize / 2);
            if (Vector3.Angle(targeter.forward, victim.transform.position - transform.position) > 90)
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
            GUI.DrawTexture(new Rect((position.x - (vSize / 2)), (position.y - (vSize / 2)), vSize, vSize), victimIndicatorTexture);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2)), vSize * 20, vSize * 2), "Victim = " + victim.name);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 15), vSize * 20, vSize * 2), "Health = " + victim.GetComponent<Health>().health);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 30), vSize * 20, vSize * 2), "Shield = " + victim.GetComponent<Health>().shieldStrength);
            GUI.Label(new Rect((position.x - (vSize / 2)), (position.y + (vSize / 2) + 45), vSize * 20, vSize * 2), "Dist = " + Vector3.Distance(transform.position, victim.transform.position));
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
}

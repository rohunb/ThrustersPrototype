using UnityEngine;
using System.Collections;

public class PlayerMissionController : MonoBehaviour {

    public Transform waypoint;
    public Texture2D waypointIndicatorTexture;
    public MissionController.MissionType currentMission;
    public bool displayWaypoint = false;
    public MissionController missionController;
    public Transform enemySpawnPoint;
    public bool onMission = false;
    public Transform targeter;
    private bool showMissionCompleteText=false;
	// Use this for initialization
	void Start () {
        missionController = GameObject.FindGameObjectWithTag("MissionController").GetComponent<MissionController>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
            GenerateNewExterminateMission();
        if(onMission && missionController.missionComplete)
        {
            onMission = false;
            showMissionCompleteText = true;
            Invoke("CancelMissionComplete", 2.0f);
        }
        if (Vector3.Distance(transform.position, waypoint.position) <= 400f)
            displayWaypoint = false;
	}
    void GenerateNewExterminateMission()
    {
        missionController.GenerateExterminateMission(MissionController.MissionType.Exterminate,waypoint,enemySpawnPoint.position,4,AI_Controller.AI_Types.Assassin);
        onMission = true;
        displayWaypoint = true;
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width-100, Screen.height - 100, 150, 150));
        GUILayout.BeginVertical();
        //GUILayout.Label("Weapon: " + currentWeapon.ToString());
        if (onMission)
        {
            GUILayout.Label("Mission: Exterminate 4 enemy ships");
            GUILayout.Label("Enemies left: "+missionController.missionEnemies.Count);
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
        if (showMissionCompleteText)
        {
            GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height / 2 - 100, 300, 100), "<color=green><size=32>Mission Complete!</size></color>");
        }
        if (displayWaypoint)
        {
            float size = 10000 / Vector3.Distance(transform.position, waypoint.position);
            size = Mathf.Clamp(size, 45f, 212f);
            Vector3 position = Camera.main.WorldToScreenPoint(waypoint.position);
            position.y = Screen.height - position.y;
            position.x = Mathf.Clamp(position.x, 0, Screen.width - size / 2);
            position.y = Mathf.Clamp(position.y, 0, Screen.height - size / 2);
            if (Vector3.Angle(targeter.forward, waypoint.position - transform.position) > 90)
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
            GUI.Label(new Rect((position.x - (size / 2)), (position.y + (size / 2) + 45), size * 20, size * 2), "Dist = " + Vector3.Distance(transform.position, waypoint.position));
        }
    }
    void CancelMissionComplete()
    {
        showMissionCompleteText = false;
    }
}

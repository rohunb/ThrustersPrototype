using UnityEngine;
using System.Collections;
public enum WhatControllerAmIUsing { MOUSE_KEYBOARD, KEYBOARD, HYDRA }

public class GOD : MonoBehaviour {

	public static AudioEngine audioengine;
    public static Galaxy galaxy;
	public static PersistentMission godMission;
    public static string currentLocation = "";

    public static WhatControllerAmIUsing whatControllerAmIUsing;

	public static Vector3 cameraPos;
	public static Quaternion cameraRot;

	public GameObject GalaxyPrefab;

    private GameObject sexyShip;

    public static bool firstUpdate;
	public static bool goToRandomPointInGalaxy = false;
    public static bool goToSpecificSystem = false;
	public static bool startAMission = false;

	private static GameObject targetPlanet;
	private float rangeFromPlanet = 2000f;

    public static string currentMission;
    public static string currentMissionLocation;

	//private MissionType mType;

	// Use this for initialization
	void Start () {
		audioengine = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
        galaxy = (Galaxy)FindObjectOfType(typeof(Galaxy));
		godMission = (PersistentMission)FindObjectOfType(typeof(PersistentMission));
        DontDestroyOnLoad(gameObject);
        firstUpdate = true;
        whatControllerAmIUsing = WhatControllerAmIUsing.MOUSE_KEYBOARD;
		GameObject newGalaxy = Instantiate(GalaxyPrefab, new Vector3(0, 0 ,0), Quaternion.identity) as GameObject;
		newGalaxy.name = "Galaxy";

		if (!GOD.goToRandomPointInGalaxy) 
		{
			Invoke("waitForaBit", 0.5f);
		}


	}

    void Update()
    {
        if (firstUpdate)
        {
            if (SixenseInput.IsBaseConnected(0))
            {
                whatControllerAmIUsing = WhatControllerAmIUsing.HYDRA;
                firstUpdate = false;
            }
        }

        switch (Application.loadedLevelName)
        {
            case "DockedScene":
                break;
            case "GameScene":
                sexyShip = GameObject.Find("sexyShip");
			if (GOD.goToRandomPointInGalaxy )
			{
				int randIndex = UnityEngine.Random.Range(1, GameObject.FindGameObjectsWithTag("Moon").Length - 1);
				targetPlanet = GameObject.FindGameObjectsWithTag("Moon")[randIndex];
				
				GameObject.Find("BackgroundCamera").transform.position = targetPlanet.transform.position - new Vector3(rangeFromPlanet, 0, 0);
				GameObject.Find("BackgroundCamera").transform.LookAt(targetPlanet.transform.position);
				GOD.goToRandomPointInGalaxy = false;
			}

            if (GOD.goToSpecificSystem)
            {
                GameObject.Find("BackgroundCamera").transform.position = targetPlanet.transform.position - new Vector3(rangeFromPlanet, 0, 0);
                GameObject.Find("BackgroundCamera").transform.LookAt(targetPlanet.transform.position);
                GOD.goToSpecificSystem = false;
                
            }

            if (currentMissionLocation == currentLocation)
            {
                if (currentMission == "Exterminate")
                {
                    sexyShip.GetComponent<PlayerMissionController>().GenerateNewExterminateMission();
                }
                else if (currentMission == "DistressCall")
                {
                    sexyShip.GetComponent<PlayerMissionController>().GenerateNewDistressMission();
                }
                else if (currentMission == "DestoryStructure")
                {
                    sexyShip.GetComponent<PlayerMissionController>().GenerateNewDestroyStructureMission();
                }
                else if (currentMission == "Gather")
                {
                    //start new Gather Mission
                }
                else if (currentMission == "Fedex")
                {
                    //start new Fedex Mission
                }

                currentMission = "";
                currentLocation = "";
            }
                break;
            case "MainMenu":
                break;
            case "FTLScene":
                //Debug.Log(currentMission + "@" + currentMissionLocation);
                break;
            default:
                break;
        }
    }

	void waitForaBit()
	{
		GOD.goToRandomPointInGalaxy = true;
		
	}

	public static void missionStart(PhysicsObject missionLocation)
	{
        targetPlanet = missionLocation.myGameObject;
        GOD.goToSpecificSystem = true;
	}
}

using UnityEngine;
using System.Collections;
public enum WhatControllerAmIUsing { MOUSE_KEYBOARD, KEYBOARD, HYDRA }

public class GOD : MonoBehaviour {

	public static AudioEngine audioengine;
    public static Galaxy galaxy;
	public static PersistentMission godMission;

    public static WhatControllerAmIUsing whatControllerAmIUsing;

	public static Vector3 cameraPos;
	public static Quaternion cameraRot;

	public GameObject GalaxyPrefab;

    public static bool firstUpdate;
	public static bool goToRandomPointInGalaxy = false;
	public static bool startAMission = false;

	public GameObject targetPlanet;
	private float rangeFromPlanet = 200;

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

		if (GOD.startAMission) 
		{
			Invoke("waitForaBit", 0.5f);
			GOD.startAMission = false;
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
			if (GOD.goToRandomPointInGalaxy )
			{
				int randIndex = UnityEngine.Random.Range(1, GameObject.FindGameObjectsWithTag("Moon").Length - 1);
				targetPlanet = GameObject.FindGameObjectsWithTag("Moon")[randIndex];
				
				GameObject.Find("BackgroundCamera").transform.position = targetPlanet.transform.position - new Vector3(rangeFromPlanet, 0, 0);
				GameObject.Find("BackgroundCamera").transform.LookAt(targetPlanet.transform.position);
				GOD.goToRandomPointInGalaxy = false;
			}
                break;
            case "MainMenu":
                break;
            default:
                break;
        }
    }

	void waitForaBit()
	{
		GOD.goToRandomPointInGalaxy = true;
		
	}

	void missionStart()
	{

	}
}

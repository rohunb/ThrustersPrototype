using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour {
    public enum MissionType
    {
        DestroyStructure,
        DistressCall,
        Exterminate
    }

    MissionType mType;

    bool onGalaxyPoint = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "GalaxyPoint")
        {
            print("GalaxyPoint Mission: " + c.GetComponent<GalaxyPoint>().mType.ToString());
            mType = (MissionType)c.GetComponent<GalaxyPoint>().mType;

            onGalaxyPoint = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.name == "GalaxyPoint")
        {
            onGalaxyPoint = false;            
        }
    }

    void OnGUI()
    {
        if (onGalaxyPoint)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width * .1f, Screen.height * .9f, Screen.width * .2f, Screen.height * .09f), "Start Mission"))
            {
                //Application.LoadLevel("DockedScene");
				GOD.goToRandomPointInGalaxy = true;
		
				Application.LoadLevel("GameScene");
            }
            Rect missionRect = new Rect(Screen.width / 2 - 140, Screen.height / 2 - 60, 280, 120);

            GUI.Box(missionRect, "MISSION");
            GUI.BeginGroup(missionRect);

            switch (mType)
            {
                case MissionType.DestroyStructure:
                    GUI.Label(new Rect(20, 20, 250, 80), "Destroy Structure:");
                    GUI.Label(new Rect(20, 40, 250, 90), "An enemy station has been spotted at the " + "A" + " sector. Destroy it. Be cautious of enemy fighter ships.");
                    GUI.Label(new Rect(20, 90, 2500, 80), "Reward: 200 Credits");
                    break;
                case MissionType.DistressCall:
                    GUI.Label(new Rect(20, 20, 250, 80), "Distress Call:");
                    GUI.Label(new Rect(20, 40, 250, 90), "An allied ship is looking for aid. Signal originated at the " + "B" + " sector. Help them.");
                    GUI.Label(new Rect(20, 90, 250, 80), "Reward: 100 Credits");
                    break;
                case MissionType.Exterminate:
                    GUI.Label(new Rect(20, 20, 250, 80), "Exterminate:");
                    GUI.Label(new Rect(20, 40, 250, 90), "4 enemy ships have been spotted at the " + "C" + " sector. Destroy them.");
                    GUI.Label(new Rect(20, 90, 250, 80), "Reward: 100 Credits");
                    break;
                default:
                    break;
            }

            GUI.EndGroup();
        }
    }

    public bool OnGalaxyPoint()
    {
        return onGalaxyPoint;
    }
}

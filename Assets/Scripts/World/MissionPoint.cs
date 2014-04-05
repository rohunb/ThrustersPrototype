using UnityEngine;
using System.Collections;

public class MissionPoint : MonoBehaviour {
    public enum MissionType
    {
        DestroyStructure,
        DistressCall,
        Exterminate
    }
    public MissionType mType;
    bool showMissionStart = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        showMissionStart = false;
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            showMissionStart = true;
    }

    void OnGUI()
    {
        if (showMissionStart)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width * .15f, Screen.height * .84f, Screen.width * .3f, Screen.height * .15f), "Start Mission"))
            {
                switch (mType)
                {
                      
                    case MissionType.DestroyStructure:
                        break;
                    case MissionType.DistressCall:
                        break;
                    case MissionType.Exterminate:
                        break;
                }
            }
        }
    }
}

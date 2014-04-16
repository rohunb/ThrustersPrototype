using UnityEngine;
using System.Collections;

public class SystemRing : MonoBehaviour {

    public TextMesh ringText;
    public TextMesh missionIndicator;
    public PhysicsObject starSystem;
    MoveToMouse spaceshipMover;
    public GUISkin guiSkin;
    bool showMissionPopup = false;

    void Awake()
    {
        spaceshipMover = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToMouse>();
    }

    void Start()
    {
        iTween.FadeTo(GameObject.Find("Fader"), iTween.Hash("alpha", 0, "time", .5));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.collider.tag == "Player")
        {
            iTween.RotateBy(GameObject.Find("Spaceship"), iTween.Hash("x", 20, "looptype", iTween.LoopType.loop));
            showMissionPopup = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.collider.tag == "Player")
        {
            showMissionPopup = false;
        }

    }
    void OnGUI()
    {
        GUI.skin = guiSkin;
        if(showMissionPopup)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 60, 200, 40), "Enter "+starSystem.name+" "))
            {
                //GoToSystem();
                iTween.FadeTo(GameObject.Find("Fader"), iTween.Hash("alpha", 1, "time", .2, "onComplete", "GoToSystem", "onCompleteTarget", gameObject));
            }
            
        }
        
    }
    void GoToSystem()
    {
        //Debug.Log("Going to system: "+starSystem.name);
        GOD.currentLocation = starSystem.name;
		GOD.audioengine.playSFX("FtlMove");
        Application.LoadLevel("GameScene");

    }
}

﻿using UnityEngine;
using System.Collections;

public class SystemRing : MonoBehaviour {

    public TextMesh ringText;
    public PhysicsObject starSystem;
    MoveToMouse spaceshipMover;

    bool showMissionPopup = false;

    void Awake()
    {
        spaceshipMover = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToMouse>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.collider.tag == "Player")
        {

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
        if(showMissionPopup)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 60, 200, 40), "Enter "+starSystem.name+" "))
            {
                GoToSystem();
            }
            
        }
        
    }
    void GoToSystem()
    {
        GOD.missionStart(starSystem);
        Debug.Log("Going to system: "+starSystem.name);
        Application.LoadLevel("GameScene");

    }
}

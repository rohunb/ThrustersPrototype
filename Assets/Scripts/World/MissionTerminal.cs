using UnityEngine;
using System.Collections;

public class MissionTerminal : MonoBehaviour {
    
    bool displayMissionPrompt = false;
    DockManager dockManager;

    void Awake()
    {
        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
    }

    void Update()
    {

        if (displayMissionPrompt && Input.GetKeyDown(KeyCode.E))
        {
            dockManager.ShowMissionTerminal();
        }

    }
    void OnGUI()
    {
        if (displayMissionPrompt)
        {
            DisplayMissionPrompt();
        }
    }

    void DisplayMissionPrompt()
    {
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 150, 500, 100), "<size=28>Press E to open Mission Terminal</size>");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            displayMissionPrompt = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            displayMissionPrompt = false;
        }
    }
}

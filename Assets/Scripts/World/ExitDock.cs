using UnityEngine;
using System.Collections;

public class ExitDock : MonoBehaviour {

    bool displayExitPrompt = false;
    DockManager dockManager;
    void Awake()
    {
        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
    }
	// Update is called once per frame
	void Update () {
	    if(displayExitPrompt && Input.GetKeyDown(KeyCode.F))
        {
            ScreenFader.EndScene();
			Application.LoadLevel("GameScene");
        }

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			//Application.LoadLevel("TestFTLScene");
		}
	}
    void OnGUI()
    {
        if(displayExitPrompt)
        {
            DisplayExitPrompt();
        }
    }
    void DisplayExitPrompt()
    {
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 150, 500, 100), "<size=28>Press F to Launch to Space</size>");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            displayExitPrompt = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            displayExitPrompt = false;
        }
        dockManager.ExitAllTerminals();
    }
}

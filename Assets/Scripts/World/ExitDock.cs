using UnityEngine;
using System.Collections;

public class ExitDock : MonoBehaviour {

    bool displayExitPrompt = false;
    DockManager dockManager;
    PlayerInventory playerInv;

    void Awake()
    {
        iTween.FadeTo(GameObject.Find("Fader"), iTween.Hash("alpha", 0, "time", .5));

        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
        playerInv = GameObject.Find("DockedShip").GetComponent<PlayerInventory>();
    }
	// Update is called once per frame
	void Update () {
	    if(displayExitPrompt && Input.GetKeyDown(KeyCode.F))
        {
            playerInv.SaveInventory();
            GUI.enabled = false;
            iTween.FadeTo(GameObject.Find("Fader"), iTween.Hash("alpha", 1, "time", .5, "onComplete", "LoadGameScene", "onCompleteTarget", gameObject));
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

    void LoadGameScene()
    {
        Application.LoadLevel("GameScene");
    }
}


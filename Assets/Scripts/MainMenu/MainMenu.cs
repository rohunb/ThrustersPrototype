using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    bool showMainMenu = true;
    bool showOptions = false;
    bool showCredits = false;
    bool showQuit = false;

    bool fadeToGameScene = false;
    bool controlsInverted = false;
    float musicVolume = 75;
    float soundVolume = 75;
    float shipVolume = 75;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnGUI()
    {
        if (showMainMenu)
        {
            if (GUI.Button(new Rect(60, 100, 140, 60), "New Game"))
            {
				Application.LoadLevel(1);
               // fadeToGameScene = true;
            }
            if (GUI.Button(new Rect(60, 180, 140, 60), "Continue"))
            {
                //showMainMenu = false;
            }
            if (GUI.Button(new Rect(60, 260, 140, 60), "Options"))
            {
                showOptions = true;
                showMainMenu = false;
            }
            if (GUI.Button(new Rect(60, 340, 140, 60), "Credits"))
            {
                showCredits = true;
                showMainMenu = false;
            }
            if (GUI.Button(new Rect(60, 420, 140, 60), "Quit"))
            {
                showQuit = true;
                showMainMenu = false;
            }
        }

        if (showOptions)
        {
            Rect optionsRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);

            GUI.Box(optionsRect, "Options");
            GUI.BeginGroup(optionsRect);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 30, 100, 20), "Music Volume");
            musicVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 50, 50, 100, 20), musicVolume, 0, 100);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 70, 100, 20), "Sound Volume");
            soundVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 50, 90, 100, 20), soundVolume, 0, 100);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 110, 100, 20), "Ship Volume");
            shipVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 50, 130, 100, 20), shipVolume, 0, 100);

            controlsInverted = GUI.Toggle(new Rect(20, 150, 100, 30), controlsInverted, "Invert Controls");
            
            if (GUI.Button(new Rect(optionsRect.width / 2 - 25, optionsRect.height - 25, 50, 20), "Return"))
            {
                showOptions = false;
                showMainMenu = true;
            }
            GUI.EndGroup();
        }

        if (showCredits)
        {
            Rect creditsRect = new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400);

            GUI.Box(creditsRect, "Credits");
            GUI.BeginGroup(creditsRect);

            GUI.Label(new Rect(10, 30, 100, 30), "McCann, Alex");
            GUI.Label(new Rect(10, 60, 100, 30), "Banerji, Rohun");
            GUI.Label(new Rect(10, 90, 100, 30), "Vo, David");
            GUI.Label(new Rect(10, 120, 100, 30), "Powers, Geordie");
            GUI.Label(new Rect(10, 150, 100, 30), "Nokes, Kyle");
            GUI.Label(new Rect(10, 180, 100, 30), "Adao, Christian");

            if (GUI.Button(new Rect(creditsRect.width / 2 - 25, creditsRect.height - 25, 50, 20), "Return"))
            {
                showCredits = false;
                showMainMenu = true;
            }
            GUI.EndGroup();
        }

        if (showQuit)
        {
            Rect quitRect = new Rect(Screen.width / 2 - 50, Screen.height / 2 - 20, 100, 60);

            GUI.Box(quitRect, "Quit?");
            GUI.BeginGroup(quitRect);
            if (GUI.Button(new Rect(5, quitRect.height - 25, 45, 20), "Yes"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(quitRect.width - 50, quitRect.height - 25, 45 ,20), "No"))
            {
                showQuit = false;
                showMainMenu = true;
            }
            GUI.EndGroup();
        }
    }
}

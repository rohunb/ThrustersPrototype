using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    bool showMainMenu = true;
    bool showOptions = false;
    bool showCredits = false;
    bool showQuit = false;

    bool controlsInverted = false;
    float musicVolume = .75f;
    float soundVolume = .75f;
    float shipVolume = .75f;

    public GUISkin guiSkin;

    // Use this for initialization
    void Start () {
        showMainMenu = false;
        iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 0, "time", .5, "onComplete", "ShowMenu", "onCompleteTarget", gameObject));
    }
    
    // Update is called once per frame
    void FixedUpdate () {

    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        if (showMainMenu)
        {
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .34f, Screen.width * .2f, Screen.height * .06f), "Play"))
            {
                GOD.audioengine.playSFX("MenuPlayBtn");
                showMainMenu = false;
                iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 1, "time", .5, "onComplete", "LoadGameScene", "onCompleteTarget", gameObject));
            }
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .44f, Screen.width * .2f, Screen.height * .06f), "Options"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showOptions = true;
                showMainMenu = false;
            }
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .54f, Screen.width * .2f, Screen.height * .06f), "Credits"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showCredits = true;
                showMainMenu = false;
            }
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .64f, Screen.width * .2f, Screen.height * .06f), "Quit"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showQuit = true;
                showMainMenu = false;
            }
        }

        if (showOptions)
        {
            Rect optionsRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 260);

            GUI.Box(optionsRect, "Options");
            GUI.BeginGroup(optionsRect);

            GUI.Label(new Rect(optionsRect.width / 2 - 65, 50, 200, 50), "Music Volume");
            musicVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 70, 130, 20), musicVolume, 0, 1);

            GUI.Label(new Rect(optionsRect.width / 2 - 70, 90, 200, 50), "Sound Volume");
            soundVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 110, 130, 20), soundVolume, 0, 1);

            GUI.Label(new Rect(optionsRect.width / 2 - 60, 130, 200, 50), "Ship Volume");
            shipVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 150, 130, 20), shipVolume, 0, 1);

            if (GUI.Button(new Rect(optionsRect.width / 2 - 150, optionsRect.height - 60, 150, 40), "Confirm"))
            {
                GOD.audioengine.playSFX("TerminalBtnYes");
                showOptions = false;
                showMainMenu = true;

                //save changes
                ConfirmChanges();
            }
            if (GUI.Button(new Rect(optionsRect.width / 2, optionsRect.height - 60, 150, 40), "Cancel"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showOptions = false;
                showMainMenu = true;

                //cancel changes
                CancelChanges();
            }
            GUI.EndGroup();
        }

        if (showCredits)
        {
            Rect creditsRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 200, 300, 400);

            GUI.Box(creditsRect, "Credits");
            GUI.BeginGroup(creditsRect);

            string[] names = new string[] { "McCann, Alex", "Banerji, Rohun", "Vo, David", "Powers, Geordie", "Nokes, Kyle", "Adao, Christian" };

            for (int i = 0; i < names.Length; i++)
            {
                GUI.Label(new Rect(creditsRect.width / 2 - 80, 50 + 40 * i, 200, 40), names[i]);
            }

            if (GUI.Button(new Rect(creditsRect.width / 2 - 100, creditsRect.height - 80, 200, 70), "Return"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showCredits = false;
                showMainMenu = true;
            }
            GUI.EndGroup();
        }

        if (showQuit)
        {
            Rect quitRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 20, 200, 100);

            GUI.Box(quitRect, "Quit?");
            GUI.BeginGroup(quitRect);
            if (GUI.Button(new Rect(0, quitRect.height - 45, 100, 40), "Yes"))
            {
                GOD.audioengine.playSFX("TerminalBtnYes");
                Application.Quit();
            }
            if (GUI.Button(new Rect(quitRect.width / 2, quitRect.height - 45, 100 , 40), "No"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                showQuit = false;
                showMainMenu = true;
            }

            GUI.EndGroup();
        }
    }

    void ConfirmChanges()
    {
        //musicVolume, soundVolume, shipVolume
        GOD.audioengine.setAllAudioVolume(musicVolume, soundVolume, shipVolume);
        print("saved changes");
    }

    void CancelChanges()
    {
        print("cancelled changes");
    }

    void ShowMenu()
    {
        showMainMenu = true;
    }

    void LoadGameScene()
    {
        Application.LoadLevel(1);
    }
}

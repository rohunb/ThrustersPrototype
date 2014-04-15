﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    bool showMainMenu = true;
    bool showOptions = false;
    bool showCredits = false;
    bool showQuit = false;

    bool fadeToGameScene = false;
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
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .24f, Screen.width * .2f, Screen.height * .06f), "New Game"))
            {
                GOD.audioengine.playSFX("MenuPlayBtn");
                showMainMenu = false;
                iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 1, "time", .5, "onComplete", "LoadGameScene", "onCompleteTarget", gameObject));
               // fadeToGameScene = true;
            }
            if (GUI.Button(new Rect(Screen.width * .05f, Screen.height * .34f, Screen.width * .2f, Screen.height * .06f), "Continue"))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                //showMainMenu = false;
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
            Rect optionsRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 300);

            GUI.Box(optionsRect, "Options");
            GUI.BeginGroup(optionsRect);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 50, 100, 20), "Music Volume");
            musicVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 70, 130, 20), musicVolume, 0, 1);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 90, 100, 20), "Sound Volume");
            soundVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 110, 130, 20), soundVolume, 0, 1);

            GUI.Label(new Rect(optionsRect.width / 2 - 50, 130, 100, 20), "Ship Volume");
            shipVolume = GUI.HorizontalSlider(new Rect(optionsRect.width / 2 - 65, 150, 130, 20), shipVolume, 0, 1);

            if (GUI.Button(new Rect(optionsRect.width / 2 - 150, optionsRect.height - 100, 150, 40), "Confirm"))
            {
                GOD.audioengine.playSFX("TerminalBtnYes");
                showOptions = false;
                showMainMenu = true;

                //save changes
                ConfirmChanges();
            }
            if (GUI.Button(new Rect(optionsRect.width / 2, optionsRect.height - 100, 150, 40), "Cancel"))
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
                GOD.audioengine.playSFX("TerminalBtn");
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
                GOD.audioengine.playSFX("TerminalBtnYes");
                Application.Quit();
            }
            if (GUI.Button(new Rect(quitRect.width - 50, quitRect.height - 25, 45 ,20), "No"))
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

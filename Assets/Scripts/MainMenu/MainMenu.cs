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
    public string[] alexWork;
    public string[] rohunWork;
    public string[] davidWork;
    public string[] geordieWork;
    public string[] kyleWork;
    public string[] christianWork;
    
    string[] names = new string[] { "McCann, Alex", "Banerji, Rohun", "Vo, David", "Powers, Geordie", "Nokes, Kyle", "Adao, Christian" };
    string[,] work;

    // Use this for initialization
    void Start () {
        showMainMenu = false;
        iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 0, "time", .5, "onComplete", "ShowMenu", "onCompleteTarget", gameObject));
        work = new string[names.Length, rohunWork.Length];

        for (int i = 0; i < alexWork.Length; i++)
        {
            work[0, i] = alexWork[i];
        }
        for (int i = 0; i < rohunWork.Length; i++)
        {
            work[1, i] = rohunWork[i];
        }
        for (int i = 0; i < davidWork.Length; i++)
        {
            work[2, i] = davidWork[i];
        }
        for (int i = 0; i < geordieWork.Length; i++)
        {
            work[3, i] = geordieWork[i];
        }
        for (int i = 0; i < kyleWork.Length; i++)
        {
            work[4, i] = kyleWork[i];
        }
        for (int i = 0; i < christianWork.Length; i++)
        {
            work[5, i] = christianWork[i];
        }

        //for (int i = 0; i < work.GetLength(0); i++)
        //{
        //    for (int j = 0; j < work.GetLength(1); j++)
        //    {
        //        if(work[i,j]!=null && work[i,j]!="")
        //            Debug.Log(work[i, j]);
        //    }
        //}

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
            float creditsWidth = Screen.width / 1.32f;
            float creditsHeight = Screen.height / 1.2f;
            Rect creditsRect = new Rect(Screen.width / 2 - creditsWidth / 2, Screen.height / 2 - creditsHeight / 2, creditsWidth, creditsHeight);
            GUI.Box(creditsRect, "Credits");
            GUI.BeginGroup(creditsRect);
            
            
            float labelWidth = Screen.width / 9.6f;
            float labelHeight = Screen.height / 27f;
            float labelWidthPadding = Screen.width / 96f;
            float labelHeightPadding = 0;// Screen.height / 108f;


            for (int i = 0; i < work.GetLength(0); i++)
            {
                GUI.Label(new Rect(Screen.width / 13.7f + (labelWidthPadding+labelWidth) * i, Screen.height / 13.6f, labelWidth, labelHeight),"<color=yellow>"+names[i]+"</color>");
                for (int j = 0; j < work.GetLength(1); j++)
                {
                    if (work[i, j] != null && work[i, j] != "")
                    {
                        GUI.Label(new Rect(Screen.width / 13.7f + (labelWidthPadding + labelWidth) * i,
                                            Screen.height / 12f + (labelHeight) * (j+1),
                                            labelWidth, labelHeight), work[i, j]);
                    }
                }
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

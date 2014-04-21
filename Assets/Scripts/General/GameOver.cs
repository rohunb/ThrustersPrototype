using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public GUISkin skin;
    bool showReturnBtn = false;

	// Use this for initialization
	void Start () {
        iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 0, "time", .5, "onComplete", "ShowReturnBtn", "onCompleteTarget", gameObject));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = skin;

        if(showReturnBtn)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width * .2f, Screen.height * .8f, Screen.width * .4f, Screen.height * .08f), "Return to Main Menu"))
            {
                showReturnBtn = false;
                iTween.FadeTo(GameObject.Find("Screen Fader"), iTween.Hash("alpha", 1, "time", .5, "onComplete", "LoadMainMenu", "onCompleteTarget", gameObject));
            }
        }
    }

    void LoadMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    void ShowReturnBtn()
    {
        showReturnBtn = true;
    }
}

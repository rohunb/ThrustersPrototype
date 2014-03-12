using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(60, 100, 200, 80), "New Game"))
        {
            print("create new game");

           	ScreenFader.EndScene();
            //if (!ScreenFader.IsTransitioning)
            //{
                    
            //}
            Application.LoadLevel("ThrustersDemo");
        }
        if (GUI.Button(new Rect(60, 180, 200, 80), "Continue"))
        {
            print("continue game");
        }
        if (GUI.Button(new Rect(60, 260, 200, 80), "Options"))
        {
            print("show options");
        }
        if (GUI.Button(new Rect(60, 340, 200, 80), "Credits"))
        {
            print("show credits");
        }
        if (GUI.Button(new Rect(60, 420, 200, 80), "Quit"))
        {
            print("quit game");
        }
    }
}

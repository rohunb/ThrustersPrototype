using UnityEngine;
using System.Collections;
public enum WhatControllerAmIUsing { MOUSE_KEYBOARD, KEYBOARD, HYDRA }

public class GOD : MonoBehaviour {

	public static AudioEngine audioengine;
    public static Galaxy galaxy;

    public static WhatControllerAmIUsing whatControllerAmIUsing;

    private bool firstUpdate;

	// Use this for initialization
	void Start () {
		audioengine = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
        galaxy = (Galaxy)FindObjectOfType(typeof(Galaxy));
        DontDestroyOnLoad(gameObject);
        firstUpdate = true;
        whatControllerAmIUsing = WhatControllerAmIUsing.MOUSE_KEYBOARD;
	}

    void Update()
    {
        if (firstUpdate)
        {
            if (SixenseInput.IsBaseConnected(0))
            {
                whatControllerAmIUsing = WhatControllerAmIUsing.HYDRA;
            } 
        }

        switch (Application.loadedLevelName)
        {
            case "DockedScene":
                break;
            case "GameScene":
                break;
            case "MainMenu":
                break;
            default:
                break;
        }
    }
}

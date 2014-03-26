using UnityEngine;
using System.Collections;
public enum whatControllerAmIUsing { MOUSE_KEYBOARD, KEYBOARD, HYDRA }

public class GOD : MonoBehaviour {

	public static AudioEngine audioengine;
    public static Galaxy galaxy;

	// Use this for initialization
	void Start () {
		audioengine = (AudioEngine)FindObjectOfType(typeof(AudioEngine));
        galaxy = (Galaxy)FindObjectOfType(typeof(Galaxy));
        DontDestroyOnLoad(gameObject);
	}

    void Update()
    {
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

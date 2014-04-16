using UnityEngine;
using System.Collections;

public class WalkingSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName == "DockedScene") {
			if(Input.GetButtonDown( "Horizontal" ) || Input.GetButtonDown( "Vertical" )) {
				GOD.audioengine.playSFX("Walk");
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class PersistentMission : MonoBehaviour {

//	public enum MissionType
//	{
//		Exterminate, //x number of ships at waypoint
//		Assassinate, //kill a particular ship (likely to spawn with cronies)
//		Gather, //mine or gather an item carried by an enemy
//		FedEx, //deliver something to another base/planet
//		DistressCall,
//		DestroyStructure
//	}

	public string[] MissionName = new string[] { 
		"Exterminate", "DistressCall", "DestoryStructure", "Race"};


	public string currentMission;

	// Use this for initialization
	void Start () {
		currentMission = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string getCurrentMission() {
		return currentMission;
	}

	public void setMissionType(string missionChoice) {
		currentMission = missionChoice;
	}
}

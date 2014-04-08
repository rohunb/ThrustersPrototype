using UnityEngine;
using System.Collections;

public class PersistentMission : MonoBehaviour {

	public enum MissionType
	{
		Exterminate, //x number of ships at waypoint
		Assassinate, //kill a particular ship (likely to spawn with cronies)
		Gather, //mine or gather an item carried by an enemy
		FedEx, //deliver something to another base/planet
		DistressCall,
		DestroyStructure
	}

	MissionType myMission;

	// Use this for initialization
	void Start () {
		myMission = MissionType.Assassinate;
	}
	
	// Update is called once per frame
	void Update () {
		listMissionType();
	}

	public void listMissionType() {

	}

	public void setMissionType(MissionType mission) {
		myMission = mission;
	}
}

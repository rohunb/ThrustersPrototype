using UnityEngine;
using System.Collections;

public class WaypointBehavior : MonoBehaviour {
	private MissionController mController;

	// Use this for initialization
	void Start () {
		mController = GameObject.Find("MissionController").GetComponent<MissionController>();
	}
	
	void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.gameObject.tag == "PlayerShip") {
			//mController.CreateRaceWaypoint(this.transform.position);
			mController.RaceWaypointHit(gameObject);
			Destroy(gameObject);
		}
	}
}

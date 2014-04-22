using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DemoShip : MonoBehaviour {

    public List<DemoThruster> thrusters;
	
	void Start () {
        DemoThruster[] demoThrusters =gameObject.GetComponentsInChildren<DemoThruster>();
        thrusters = new List<DemoThruster>(demoThrusters);
	}
	
	
	void Update () {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        
        if(vert!=0.0f)
        {
            thrusters.Where(t => t.CanMove(vert))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if(hori!=0.0f)
        {
            thrusters.Where(t => t.CanStrafe(hori))
                .ToList()
                .ForEach(t => t.Fire());
            
        }

        if(Input.GetKey(KeyCode.Q))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.YawCounterclock))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if (Input.GetKey(KeyCode.E))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.YawClockwise))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if (Input.GetKey(KeyCode.J))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.RollLeft))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if (Input.GetKey(KeyCode.L))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.RollRight))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if (Input.GetKey(KeyCode.I))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.PitchUp))
                .ToList()
                .ForEach(t => t.Fire());
        }
        if (Input.GetKey(KeyCode.K))
        {
            thrusters.Where(t => t.CanTurnInDirection(DemoThruster.TurnDirection.PitchDown))
                .ToList()
                .ForEach(t => t.Fire());
        }
	}
}

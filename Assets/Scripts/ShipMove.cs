using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    public GameObject[] forwardThrusters;
    public GameObject[] reverseThrusters;

    private ThrusterForce thruster;
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        if(moveV>0.0f)
        {
            FireForwardThrusters();

        }
        else if(moveV<=0.0f)
        {
            FireReverseThrusters();
        }
        if (moveH > 0.0f)
        {
            FireStrafeRightThrusters();

        }
        else if (moveH <= 0.0f)
        {
            FireStrafeLeftThrusters();
        }
	}

    void FireForwardThrusters()
    {
        for (int i = 0; i < forwardThrusters.Length; i++)
        {
            thruster = forwardThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
        
    }
    void FireReverseThrusters()
    {

    }
    void FireStrafeRightThrusters()
    {

    }
    void FireStrafeLeftThrusters()
    {

    }

}


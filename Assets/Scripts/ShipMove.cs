using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    public GameObject[] forwardThrusters;
    public GameObject[] reverseThrusters;
    public GameObject[] forwardLeftThrusters;
    public GameObject[] forwardRightThrusters;
    public GameObject[] rearLeftThrusters;
    public GameObject[] rearRightThrusters;

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
        else if(moveV<0.0f)
        {
            FireReverseThrusters();
        }
        if (moveH > 0.0f)
        {
            FireTurnRightThrusters();

        }
        else if (moveH < 0.0f)
        {
            FireTurnLeftThrusters();
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
        for (int i = 0; i < reverseThrusters.Length; i++)
        {
            thruster = reverseThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireTurnRightThrusters()
    {
        for (int i = 0; i < forwardLeftThrusters.Length; i++)
        {
            thruster = forwardLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < rearRightThrusters.Length; i++)
        {
            thruster = rearRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireTurnLeftThrusters()
    {
        for (int i = 0; i < forwardRightThrusters.Length; i++)
        {
            thruster = forwardRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < rearLeftThrusters.Length; i++)
        {
            thruster = rearLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

}


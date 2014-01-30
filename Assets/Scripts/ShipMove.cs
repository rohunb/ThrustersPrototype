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
            StrafeRight();
        }
        else if (moveH < 0.0f)
        {
            StrafeLeft();
        }
        if(Input.GetKey(KeyCode.Q))
        {
            TurnLeft();
        }
        else if(Input.GetKey(KeyCode.E))
        {
            TurnRight();
        }
        if (Input.GetKey(KeyCode.Space))
            Stabilize();
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

    void TurnRight()
    {
        FireForwardLeftThrusters();
        FireRearRightThrusters();
    }
    void TurnLeft()
    {
        FireForwardRightThrusters();
        FireRearLeftThrusters();
    }
    void StrafeRight()
    {
        FireForwardLeftThrusters();
        FireRearLeftThrusters();
    }
    void StrafeLeft()
    {
        FireForwardRightThrusters();
        FireRearRightThrusters();
    }


    void FireForwardLeftThrusters()
    {
        for (int i = 0; i < forwardLeftThrusters.Length; i++)
        {
            thruster = forwardLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireForwardRightThrusters()
    {
        for (int i = 0; i < forwardRightThrusters.Length; i++)
        {
            thruster = forwardRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireRearLeftThrusters()
    {
        for (int i = 0; i < rearLeftThrusters.Length; i++)
        {
            thruster = rearLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireRearRightThrusters()
    {
        for (int i = 0; i < rearRightThrusters.Length; i++)
        {
            thruster = rearRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void Stabilize()
    {
        Vector3 vel=rigidbody.velocity;
        rigidbody.velocity = new Vector3(
            Mathf.Lerp(vel.x,0f,Time.deltaTime),
            Mathf.Lerp(vel.y, 0f, Time.deltaTime),
            Mathf.Lerp(vel.z, 0f, Time.deltaTime));
        Vector3 aVel = rigidbody.angularVelocity;
        rigidbody.angularVelocity = new Vector3(
            Mathf.Lerp(aVel.x, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.y, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.z, 0f, Time.deltaTime));

    }
}


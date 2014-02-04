using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    public float fwdThrustForce = 7000.0f;
    public float revThrustForce = 5000.0f;
    public float turnForce = 1000f;
    public float strafeForce = 1000f;
    public float verticalForce = 1000f;
    public float pitchForce = 1000f;
    public float rollForce = 1000f;

    public GameObject[] forwardThrusters;
    public GameObject[] reverseThrusters;
    public GameObject[] forwardLeftThrusters;
    public GameObject[] forwardRightThrusters;
    public GameObject[] rearLeftThrusters;
    public GameObject[] rearRightThrusters;
    public GameObject[] bottomLeftThrusters;
    public GameObject[] bottomRightThrusters;
    public GameObject[] topLeftThrusters;
    public GameObject[] topRightThrusters;
    public GameObject[] bottomRearThrusters;
    public GameObject[] bottomFwdThrusters;
    public GameObject[] topRearThrusters;
    public GameObject[] topFwdThrusters;
    

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
        
        if (Input.GetKey(KeyCode.U))
            MoveUp();
        if (Input.GetKey(KeyCode.H))
            MoveDown();
        if(Input.GetKey(KeyCode.J))
            RollLeft();
        if(Input.GetKey(KeyCode.L))
            RollRight();
        if(Input.GetKey(KeyCode.I))
            PitchForward();
        if(Input.GetKey(KeyCode.K))
            PitchBack();
        if (Input.GetKey(KeyCode.Space))
            Stabilize();
	}

    void MoveUp()
    {
        FireBottomThrusters(verticalForce);
    }
    void MoveDown()
    {
        FireTopThrusters(verticalForce);
    }
    void RollLeft()
    {
        FireBottomRightThrusters(rollForce);
        FireTopLeftThrusters(rollForce);
    }

    
    void RollRight()
    {
        FireBottomLeftThrusters(rollForce);
        FireTopRightThrusters(rollForce);
    }

    
    void PitchForward()
    {
        FireBottomRearThrusters(pitchForce);
        FireTopFwdThrusters(pitchForce);
    }

   
    void PitchBack()
    {
        FireBottomFwdThrusters(pitchForce);
        FireTopRearThrusters(pitchForce);
    }

    void FireForwardThrusters()
    {
        for (int i = 0; i < forwardThrusters.Length; i++)
        {
            thruster = forwardThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = fwdThrustForce;
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
                thruster.maxThrustForce = revThrustForce;
                thruster.FireThruster();
            }
        }
    }

    void TurnRight()
    {
        FireForwardLeftThrusters(turnForce);
        FireRearRightThrusters(turnForce);
    }
    void TurnLeft()
    {
        FireForwardRightThrusters(turnForce);
        FireRearLeftThrusters(turnForce);
    }
    void StrafeRight()
    {
        FireForwardLeftThrusters(strafeForce);
        FireRearLeftThrusters(strafeForce);
    }
    void StrafeLeft()
    {
        FireForwardRightThrusters(strafeForce);
        FireRearRightThrusters(strafeForce);
    }


    void FireForwardLeftThrusters(float thrustForce)
    {
        for (int i = 0; i < forwardLeftThrusters.Length; i++)
        {
            thruster = forwardLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    void FireForwardRightThrusters(float thrustForce)
    {
        for (int i = 0; i < forwardRightThrusters.Length; i++)
        {
            thruster = forwardRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    void FireRearLeftThrusters(float thrustForce)
    {
        for (int i = 0; i < rearLeftThrusters.Length; i++)
        {
            thruster = rearLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    void FireRearRightThrusters(float thrustForce)
    {
        for (int i = 0; i < rearRightThrusters.Length; i++)
        {
            thruster = rearRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
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

    void FireBottomThrusters(float thrustForce)
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    void FireTopThrusters(float thrustForce)
    {
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopLeftThrusters(float thrustForce)
    {
        for (int i = 0; i < topLeftThrusters.Length; i++)
        {
            thruster = topLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRightThrusters(float thrustForce)
    {
        for (int i = 0; i < bottomRightThrusters.Length; i++)
        {
            thruster = bottomRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRightThrusters(float thrustForce)
    {
        for (int i = 0; i < topRightThrusters.Length; i++)
        {
            thruster = topRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomLeftThrusters(float thrustForce)
    {
        for (int i = 0; i < bottomLeftThrusters.Length; i++)
        {
            thruster = bottomLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopFwdThrusters(float thrustForce)
    {
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRearThrusters(float thrustForce)
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRearThrusters(float thrustForce)
    {
        
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomFwdThrusters(float thrustForce)
    {
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.FireThruster();
            }
        }
    }
}


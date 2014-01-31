using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    public GameObject[] forwardThrusters;
    public GameObject[] reverseThrusters;
    public GameObject[] forwardLeftThrusters;
    public GameObject[] forwardRightThrusters;
    public GameObject[] rearLeftThrusters;
    public GameObject[] rearRightThrusters;

    //new
    public GameObject[] bottomLeftThrusters;
    public GameObject[] bottomRightThrusters;
    public GameObject[] topLeftThrusters;
    public GameObject[] topRightThrusters;
    public GameObject[] bottomRearThrusters;
    public GameObject[] bottomFwdThrusters;
    public GameObject[] topRearThrusters;
    public GameObject[] topFwdThrusters;
    //new



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

        //new
        //move up
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




        //new
	}

    //new
    void MoveUp()
    {
        FireBottomThrusters();
    }
    void MoveDown()
    {
        FireTopThrusters();
    }
    void RollLeft()
    {
        FireBottomRightThrusters();
        FireTopLeftThrusters();
    }

    
    void RollRight()
    {
        FireBottomLeftThrusters();
        FireTopRightThrusters();
    }

    
    void PitchForward()
    {
        FireBottomRearThrusters();
        FireTopFwdThrusters();
    }

   
    void PitchBack()
    {
        FireBottomFwdThrusters();
        FireTopRearThrusters();
    }

    
    //new



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

    //new

    /*
     * public GameObject[] bottomLeftThrusters;
    public GameObject[] bottomRightThrusters;
    public GameObject[] topLeftThrusters;
    public GameObject[] topRightThrusters;
    public GameObject[] bottomRearThrusters;
    public GameObject[] bottomFwdThrusters;
    public GameObject[] topRearThrusters;
    public GameObject[] topFwdThrusters;
     * */
     
    
    void FireBottomThrusters()
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    void FireTopThrusters()
    {
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    private void FireTopLeftThrusters()
    {
        for (int i = 0; i < topLeftThrusters.Length; i++)
        {
            thruster = topLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRightThrusters()
    {
        for (int i = 0; i < bottomRightThrusters.Length; i++)
        {
            thruster = bottomRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRightThrusters()
    {
        for (int i = 0; i < topRightThrusters.Length; i++)
        {
            thruster = topRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomLeftThrusters()
    {
        for (int i = 0; i < bottomLeftThrusters.Length; i++)
        {
            thruster = bottomLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    private void FireTopFwdThrusters()
    {
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRearThrusters()
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRearThrusters()
    {
        
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomFwdThrusters()
    {
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.FireThruster();
            }
        }
    }

    //new
}


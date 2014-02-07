using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

    //Movement Force
    public float fwdThrustForce = 7000.0f;
    public float revThrustForce = 5000.0f;
    public float turnForce = 1000f;
    public float strafeForce = 1000f;
    public float verticalForce = 1000f;
    public float pitchForce = 1000f;
    public float rollForce = 1000f;

    //Thrusters
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

    //control variables
    public enum ControlModes { Keyboard, Mouse, Hydra };
    public ControlModes controlMode;
    private bool stablizing=false;
    private Vector2 mousePos;
    public Vector2 mouseDeadZone=new Vector2(0.0f,0.0f);
    private Quaternion initRot;
    void Start()
    {
        initRot = transform.rotation;
    }
	// Update is called once per frame
    void Update()
    {
        
        switch (controlMode)
        {
            case ControlModes.Hydra:

                break;

            case ControlModes.Keyboard:
                UniversalKeyboardControls();
                if (Input.GetKey(KeyCode.Q))
                {
                    TurnLeft(1f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    TurnRight(1f);
                }
                if (Input.GetKey(KeyCode.U))
                    MoveUp();
                if (Input.GetKey(KeyCode.H))
                    MoveDown();
                if (Input.GetKey(KeyCode.J))
                    RollLeft();
                if (Input.GetKey(KeyCode.L))
                    RollRight();
                if (Input.GetKey(KeyCode.I))
                    PitchForward(1f);
                if (Input.GetKey(KeyCode.K))
                    PitchBack(1f);

                //temp mouse
                
                //temp mouse

                break;

            case ControlModes.Mouse:
                UniversalKeyboardControls();
                mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (!stablizing)
                {
                    if (mousePos.x > 0.5f + mouseDeadZone.x)
                    {
                        TurnRight(mousePos.x - 0.5f - mouseDeadZone.x);
                    }
                    if (mousePos.x < 0.5f - mouseDeadZone.x)
                    {
                        TurnLeft(mousePos.x + (0.5f - mouseDeadZone.x));
                    }
                    if (mousePos.y > 0.5f + mouseDeadZone.y)
                    {
                        PitchBack(mousePos.y - 0.5f - mouseDeadZone.y);
                    }
                    if (mousePos.y < 0.5f - mouseDeadZone.y)
                    {
                        PitchForward(mousePos.y + 0.5f - mouseDeadZone.y);
                    }
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    RollLeft();
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    RollRight();
                }


                break;

        }

        VelocityStabilize();


        //mouse controls





    }

    private void VelocityStabilize()
    {
        
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Velocity: " + rigidbody.velocity.ToString());
        GUILayout.Label("Angular Velocity: " + rigidbody.angularVelocity.ToString());
        GUILayout.Label("Rotation: " + transform.rotation.ToString());
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void UniversalKeyboardControls()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        if (moveV > 0.0f)
        {
            FireForwardThrusters();

        }
        else if (moveV < 0.0f)
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
        if (Input.GetKey(KeyCode.U))
            MoveUp();
        if (Input.GetKey(KeyCode.H))
            MoveDown();
        if (Input.GetKey(KeyCode.Space))
            Stabilize();
        if (Input.GetKey(KeyCode.R))
        {
            stablizing = true;
            OrientZero();
        }
        else
        {
            stablizing = false;
        }
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

    
    void PitchForward(float pitchSpeed)
    {
        FireBottomRearThrusters(pitchForce * pitchSpeed);
        FireTopFwdThrusters(pitchForce * pitchSpeed);
    }


    void PitchBack(float pitchSpeed)
    {
        FireBottomFwdThrusters(pitchForce * pitchSpeed);
        FireTopRearThrusters(pitchForce * pitchSpeed);
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

    void TurnRight(float turnSpeed)
    {
        FireForwardLeftThrusters(turnForce*turnSpeed);
        FireRearRightThrusters(turnForce*turnSpeed);
    }
    void TurnLeft(float turnSpeed)
    {
        FireForwardRightThrusters(turnForce * turnSpeed);
        FireRearLeftThrusters(turnForce * turnSpeed);
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
    void OrientZero()
    {
        Vector3 aVel = rigidbody.angularVelocity;
        rigidbody.angularVelocity = new Vector3(
            Mathf.Lerp(aVel.x, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.y, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.z, 0f, Time.deltaTime));
        Vector3 rot=new Vector3(initRot.eulerAngles.x,transform.rotation.eulerAngles.y,initRot.eulerAngles.z);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), Time.deltaTime);
        
        
        
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


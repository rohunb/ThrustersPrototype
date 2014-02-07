using UnityEngine;
using System.Collections;

public class AI_ShipMove : MonoBehaviour
{

    #region Movement Fields
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
    #endregion

    Transform target;
    public float turnSpeed = 4f;
    public float moveSpeed = 100f;
    public enum AI_Types { FlyBy,Assassin,Vulture}
    public AI_Types ai_type;
    public enum AI_States { Hunting,Fleeing,Attacking}
    public AI_States ai_state;

    void Start()
    {
        initRot = transform.rotation;
        target = GameObject.FindGameObjectWithTag("AI_Dest").transform;
        
    }
	// Update is called once per frame
    void Update()
    {


        #region Player Controls
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
                    MoveUp(1f);
                if (Input.GetKey(KeyCode.H))
                    MoveDown(1f);
                if (Input.GetKey(KeyCode.J))
                    RollLeft(1f);
                if (Input.GetKey(KeyCode.L))
                    RollRight(1f);
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
                    RollLeft(1f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    RollRight(1f);
                }


                break;

        }
        #endregion


        switch (ai_type)
        {
            case AI_Types.FlyBy:
                break;
            case AI_Types.Assassin:
                break;
            case AI_Types.Vulture:
                break;
            default:
                break;
        }

        LookAtTarget();
        MoveForward();
        
        
        
    }
    void LookAtTarget()
    {
        //Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, Time.deltaTime*turnSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turnSpeed * Time.deltaTime);
    }
    void MoveForward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 150, 150));
        GUILayout.BeginVertical();
        GUILayout.Label("Velocity: " + rigidbody.velocity.ToString());
        GUILayout.Label("Angular Velocity: " + rigidbody.angularVelocity.ToString());
        GUILayout.Label("Rotation0: " + transform.rotation.ToString());
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }


    #region Movement Methods
    private void UniversalKeyboardControls()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");
        if (moveV > 0.0f)
        {
            FireForwardThrusters(1f);

        }
        else if (moveV < 0.0f)
        {
            FireReverseThrusters(1f);
        }
        if (moveH > 0.0f)
        {
            StrafeRight(1f);
        }
        else if (moveH < 0.0f)
        {
            StrafeLeft(1f);
        }
        if (Input.GetKey(KeyCode.U))
            MoveUp(1f);
        if (Input.GetKey(KeyCode.H))
            MoveDown(1f);
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

    void MoveUp(float amount)
    {
        FireBottomThrusters(verticalForce*amount);
    }
    void MoveDown(float amount)
    {
        FireTopThrusters(verticalForce * amount);
    }
    void RollLeft(float amount)
    {
        FireBottomRightThrusters(rollForce*amount);
        FireTopLeftThrusters(rollForce * amount);
    }

    
    void RollRight(float amount)
    {
        FireBottomLeftThrusters(rollForce * amount);
        FireTopRightThrusters(rollForce * amount);
    }

    
    void PitchForward(float amount)
    {
        FireBottomRearThrusters(pitchForce * amount);
        FireTopFwdThrusters(pitchForce * amount);
    }


    void PitchBack(float amount)
    {
        FireBottomFwdThrusters(pitchForce * amount);
        FireTopRearThrusters(pitchForce * amount);
    }

    void FireForwardThrusters(float amount)
    {
        for (int i = 0; i < forwardThrusters.Length; i++)
        {
            thruster = forwardThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = fwdThrustForce * amount;
                thruster.FireThruster();
            }
        }
        
    }
    void FireReverseThrusters(float amount)
    {
        for (int i = 0; i < reverseThrusters.Length; i++)
        {
            thruster = reverseThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = revThrustForce * amount;
                thruster.FireThruster();
            }
        }
    }

    void TurnRight(float amount)
    {
        FireForwardLeftThrusters(turnForce * amount);
        FireRearRightThrusters(turnForce * amount);
    }
    void TurnLeft(float amount)
    {
        FireForwardRightThrusters(turnForce * amount);
        FireRearLeftThrusters(turnForce * amount);
    }
    void StrafeRight(float amount)
    {
        FireForwardLeftThrusters(strafeForce * amount);
        FireRearLeftThrusters(strafeForce * amount);
    }
    void StrafeLeft(float amount)
    {
        FireForwardRightThrusters(strafeForce * amount);
        FireRearRightThrusters(strafeForce * amount);
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
    #endregion

}


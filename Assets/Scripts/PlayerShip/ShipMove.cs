using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
    
    //afterburner
    public float afterburnerForce = 21000f;
    public float maxAfterburnerLevel = 100f;
    public float currentAfterburnerLevel;
    public float afterburnerDrain = 4f;
    public float afterburnerRecharge = 2f;
    
    //camera vars
    float normalFov = 60.0f;
    float highFov = 100.0f;
    float currentFov;
    public float fovChangeSpeed=10f;
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
    public GameObject[] afterburnerThrusters;
    private ThrusterForce thruster;

    void Start()
    {   
        currentAfterburnerLevel = maxAfterburnerLevel;
        currentFov = normalFov;
    }
	// Update is called once per frame
    void Update()
    {
      
        currentAfterburnerLevel += afterburnerRecharge*Time.deltaTime;
        currentAfterburnerLevel = Mathf.Clamp(currentAfterburnerLevel, 0f, maxAfterburnerLevel);
        Camera.main.fieldOfView = currentFov;
    }

    
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 150, 170));
        GUILayout.BeginVertical();
        GUILayout.Label("Velocity: " + rigidbody.velocity.ToString());
        GUILayout.Label("Angular Velocity: " + rigidbody.angularVelocity.ToString());
        GUILayout.Label("Rotation: " + transform.rotation.ToString());
        GUILayout.Label("Mouse Pos: " + Camera.main.ScreenToViewportPoint(Input.mousePosition).ToString());
        GUILayout.Label("FOV: " + currentFov);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public void FireAfterburner()
    {
        if (currentAfterburnerLevel > afterburnerDrain * Time.deltaTime)
        {
            FireAfterburnerThrusters(afterburnerForce, 1f);
            currentAfterburnerLevel -= afterburnerDrain * Time.deltaTime;
            currentFov = Mathf.Lerp(currentFov, highFov, Time.deltaTime * fovChangeSpeed);
        }
        else
            StopFiringAfterburners();
        

    }
    public void StopFiringAfterburners()
    {
        currentFov = Mathf.Lerp(currentFov, normalFov, Time.deltaTime * fovChangeSpeed);
    }
    public void MoveFoward(float amount)
    {
        FireForwardThrusters(fwdThrustForce, amount);
    }
    public void MoveBack(float amount)
    {
        FireReverseThrusters(revThrustForce,amount);
    }

    public void TurnRight(float amount)
    {
        FireForwardLeftThrusters(turnForce , amount);
        FireRearRightThrusters(turnForce , amount);
    }
    public void TurnLeft(float amount)
    {
        FireForwardRightThrusters(turnForce , amount);
        FireRearLeftThrusters(turnForce , amount);
    }
    public void StrafeRight(float amount)
    {
        FireForwardLeftThrusters(strafeForce , amount);
        FireRearLeftThrusters(strafeForce , amount);
    }
    public void StrafeLeft(float amount)
    {
        FireForwardRightThrusters(strafeForce , amount);
        FireRearRightThrusters(strafeForce , amount);
    }
    public void MoveUp(float amount)
    {
        FireBottomThrusters(verticalForce,amount);
    }
    public void MoveDown(float amount)
    {
        FireTopThrusters(verticalForce , amount);
    }
    public void RollLeft(float amount)
    {
        FireBottomRightThrusters(rollForce,amount);
        FireTopLeftThrusters(rollForce , amount);
    }


    public void RollRight(float amount)
    {
        FireBottomLeftThrusters(rollForce , amount);
        FireTopRightThrusters(rollForce , amount);
    }


    public void PitchDown(float amount)
    {
        FireBottomRearThrusters(pitchForce , amount);
        FireTopFwdThrusters(pitchForce , amount);
    }


    public void PitchUp(float amount)
    {
        FireBottomFwdThrusters(pitchForce , amount);
        FireTopRearThrusters(pitchForce , amount);
    }
    

    void FireAfterburnerThrusters(float force, float amount)
    {
        for (int i = 0; i < afterburnerThrusters.Length; i++)
        {
            thruster = afterburnerThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = force * amount;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    void FireForwardThrusters(float force, float amount)
    {
        for (int i = 0; i < forwardThrusters.Length; i++)
        {
            thruster = forwardThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = force * amount;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
        
    }
    void FireReverseThrusters(float force, float amount)
    {
        for (int i = 0; i < reverseThrusters.Length; i++)
        {
            thruster = reverseThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = force * amount;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }



    void FireForwardLeftThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < forwardLeftThrusters.Length; i++)
        {
            thruster = forwardLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    void FireForwardRightThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < forwardRightThrusters.Length; i++)
        {
            thruster = forwardRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    void FireRearLeftThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < rearLeftThrusters.Length; i++)
        {
            thruster = rearLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    void FireRearRightThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < rearRightThrusters.Length; i++)
        {
            thruster = rearRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }

    void FireBottomThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    void FireTopThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopLeftThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < topLeftThrusters.Length; i++)
        {
            thruster = topLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRightThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < bottomRightThrusters.Length; i++)
        {
            thruster = bottomRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRightThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < topRightThrusters.Length; i++)
        {
            thruster = topRightThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomLeftThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < bottomLeftThrusters.Length; i++)
        {
            thruster = bottomLeftThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopFwdThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < topFwdThrusters.Length; i++)
        {
            thruster = topFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomRearThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < bottomRearThrusters.Length; i++)
        {
            thruster = bottomRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    private void FireTopRearThrusters(float thrustForce, float amount)
    {
        
        for (int i = 0; i < topRearThrusters.Length; i++)
        {
            thruster = topRearThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }

    private void FireBottomFwdThrusters(float thrustForce, float amount)
    {
        for (int i = 0; i < bottomFwdThrusters.Length; i++)
        {
            thruster = bottomFwdThrusters[i].GetComponent<ThrusterForce>();
            if (!thruster.damaged)
            {
                thruster.maxThrustForce = thrustForce;
                thruster.thrustAmount = amount;
                thruster.FireThruster();
            }
        }
    }
    public void KillLinearVelocity()
    {
        Vector3 vel = rigidbody.velocity;
        rigidbody.velocity = new Vector3(
            Mathf.Lerp(vel.x, 0f, Time.deltaTime),
            Mathf.Lerp(vel.y, 0f, Time.deltaTime),
            Mathf.Lerp(vel.z, 0f, Time.deltaTime));
 

    }
    public void KillAngularVelocity()
    {
        Vector3 aVel = rigidbody.angularVelocity;
        rigidbody.angularVelocity = new Vector3(
            Mathf.Lerp(aVel.x, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.y, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.z, 0f, Time.deltaTime));
        //Vector3 rot = new Vector3(initRot.eulerAngles.x, transform.rotation.eulerAngles.y, initRot.eulerAngles.z);

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), Time.deltaTime);



    }
}


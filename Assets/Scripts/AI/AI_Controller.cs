using UnityEngine;
using System.Collections;

public class AI_Controller : MonoBehaviour
{
    //Movement Force
    public float fwdThrustForce = 7000.0f;
    public float revThrustForce = 5000.0f;
    //Thrusters
    public GameObject[] AIFrwardThrusters;
    public GameObject[] AIReverseThrusters;
    private ThrusterForce AIThruster;

    //movement variables
    public Transform target;
    public float turnSpeed = 2f;
    public float moveSpeed = 100f;

    //states
    public enum AI_Types { FlyBy,Assassin,Vulture}
    public AI_Types ai_type;
    public enum AI_States { Hunting,Fleeing,AttackMove,StopAndAttack,Waiting}
    public AI_States ai_state;
    public bool attacking;
    //ai behaviour variables
    private float distToTarget;
    float angle;
    public float weaponsRange = 250f;
    public float maxRange = 1000f;
    public float minRange = 75f;
    public float timeToBreakaway = 6f;
    //flyby
    public bool breakingAway = false;

    //assassin
    public float sightRange = 350f;
    public bool spotted = false;

    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("PlayerShip").transform;
        switch (ai_type)
        {
            case AI_Types.FlyBy:
                ai_state = AI_States.Hunting;
                break;
            case AI_Types.Assassin:
                ai_state = AI_States.Waiting;
                break;
            case AI_Types.Vulture:
                ai_state = AI_States.Waiting;
                break;
            default:
                break;
        }
        attacking = false;

        
    }
	// Update is called once per frame
    void Update()
    {
        distToTarget = Vector3.Distance(target.position, transform.position);
        angle = Vector3.Angle(rigidbody.velocity, target.position - transform.position);
        switch (ai_type)
        {
            case AI_Types.FlyBy:
               
                
                if (breakingAway)
                {
                    if (distToTarget < maxRange)
                        ai_state = AI_States.Fleeing;
                    else
                    {
                        ai_state = AI_States.Hunting;
                        breakingAway = false;
                    }
                }
                else
                {
                    if (distToTarget > weaponsRange)
                        ai_state = AI_States.Hunting;
                    if (distToTarget <= weaponsRange)
                    {
                        if (distToTarget <= minRange)
                        {
                            ai_state = AI_States.StopAndAttack;
                            Invoke("BreakAway", timeToBreakaway);
                        }
                        else
                        {
                            ai_state = AI_States.AttackMove;
                            Invoke("BreakAway", timeToBreakaway);
                        }
                    }
                }
                break;
            case AI_Types.Assassin:
                if (distToTarget < sightRange)
                    spotted = true;
                if (spotted)
                {
                    ai_type = AI_Types.FlyBy;
                    //if (distToTarget > weaponsRange)
                    //    ai_state = AI_States.Hunting;
                    //if (distToTarget <= weaponsRange)
                    //{
                    //    if (distToTarget <= minRange)
                    //    {
                    //        ai_state = AI_States.StopAndAttack;
                    //        Invoke("BreakAway", timeToBreakaway);
                    //    }
                    //    else
                    //    {
                    //        ai_state = AI_States.AttackMove;
                    //        Invoke("BreakAway", timeToBreakaway);
                    //    }
                    //}
                }
                break;
            case AI_Types.Vulture:
                break;
            default:
                break;
        }


        switch (ai_state)
        {
            case AI_States.Hunting:
                LookAtTarget();
                if (angle > 90 && distToTarget <= maxRange)
                    Brake();
                if (angle > 90)
                {
                    MoveForward();
                    Brake();
                }
                else MoveForward();
                break;
            case AI_States.Fleeing:
                LookAwayFromTarget();
                if (angle <= 90)
                {
                    MoveForward();
                    Brake();
                }
                else
                    MoveForward();
                break;
            case AI_States.AttackMove:
                //Debug.Log("attacking");
                LookAtTarget();
                MoveForward();
                break;
            case AI_States.StopAndAttack:
                LookAtTarget();
                Brake();
                //Debug.Log("attacking");
                break;
            case AI_States.Waiting:
                //Debug.Log("waiting");
                break;
            default:
                break;
        }

        if (ai_state == AI_States.AttackMove || ai_state == AI_States.StopAndAttack)
            attacking = true;
        else
            attacking = false;
    }

    void Brake()
    {
        Vector3 vel = rigidbody.velocity;
        rigidbody.velocity = new Vector3(
            Mathf.Lerp(vel.x, 0f, Time.deltaTime),
            Mathf.Lerp(vel.y, 0f, Time.deltaTime),
            Mathf.Lerp(vel.z, 0f, Time.deltaTime));
        Vector3 aVel = rigidbody.angularVelocity;
        rigidbody.angularVelocity = new Vector3(
            Mathf.Lerp(aVel.x, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.y, 0f, Time.deltaTime),
            Mathf.Lerp(aVel.z, 0f, Time.deltaTime));

    }
        
    void BreakAway()
    {
        ai_state = AI_States.Fleeing;
        breakingAway = true;
    }
    
    void LookAtTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turnSpeed * Time.deltaTime);
    }
    void LookAwayFromTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-target.position + transform.position), turnSpeed * Time.deltaTime);
    }
    void MoveForward()
    {
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //rigidbody.AddForce(transform.forward * fwdThrustForce);
        FireForwardThrusters(1.0f);
    }
    void MoveBack()
    {
        FireReverseThrusters(1f);
    }
    void FireForwardThrusters(float amount)
    {
        for (int i = 0; i < AIFrwardThrusters.Length; i++)
        {
            AIThruster = AIFrwardThrusters[i].GetComponent<ThrusterForce>();
            if (!AIThruster.damaged)
            {
                AIThruster.maxThrustForce = fwdThrustForce * amount;
                AIThruster.FireThruster();
            }
        }

    }
    void FireReverseThrusters(float amount)
    {
        for (int i = 0; i < AIReverseThrusters.Length; i++)
        {
            AIThruster = AIReverseThrusters[i].GetComponent<ThrusterForce>();
            if (!AIThruster.damaged)
            {
                AIThruster.maxThrustForce = revThrustForce * amount;
                AIThruster.FireThruster();
            }
        }
    }
    //void OnGUI()
    //{
        //GUILayout.BeginArea(new Rect(10, 10, 150, 150));
        //GUILayout.BeginVertical();
        //GUILayout.Label("Velocity: " + rigidbody.velocity.ToString());
        //GUILayout.Label("Angular Velocity: " + rigidbody.angularVelocity.ToString());
        //GUILayout.Label("Rotation: " + transform.rotation.ToString());
        //GUILayout.Label("Distance: " + distToTarget.ToString());
        //GUILayout.Label("Angle: " + angle.ToString());
        //GUILayout.EndVertical();
        //GUILayout.EndArea();
    //}

}


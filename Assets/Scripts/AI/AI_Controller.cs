using UnityEngine;
using System.Collections;

public class AI_Controller : MonoBehaviour
{

    
    //movement variables
    public Transform target;
    public float turnSpeed = 4f;
    public float moveSpeed = 100f;

    //states
    public enum AI_Types { FlyBy,Assassin,Vulture}
    public AI_Types ai_type;
    public enum AI_States { Hunting,Fleeing,AttackMove,StopAndAttack,Waiting}
    public AI_States ai_state;

    //ai behaviour variables
    private float distToTarget;
    
    public float weaponsRange = 250f;
    public float maxRange = 1000f;
    public float minRange = 75f;

    //flyby
    public bool breakingAway = false;

    //assassin
    public float sightRange = 350f;
    public bool spotted = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("AI_Dest").transform;
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


        
    }
	// Update is called once per frame
    void Update()
    {
        distToTarget = Vector3.Distance(target.position, transform.position);
        //float angle = Vector3.Angle(transform.forward, target.position - transform.position);
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
                    if (distToTarget <= minRange)
                    {
                        ai_state = AI_States.StopAndAttack;
                        Invoke("BreakAway", 3f);
                    }
                    else if (distToTarget <= weaponsRange)
                    {
                        ai_state = AI_States.AttackMove;
                        Invoke("BreakAway", 3f);
                    }
                }
                break;
            case AI_Types.Assassin:
                if (distToTarget < sightRange)
                    spotted = true;
                if (spotted)
                {
                    if (distToTarget < weaponsRange)
                        ai_state = AI_States.Hunting;
                    if (distToTarget <= minRange)
                        ai_state = AI_States.StopAndAttack;
                    else if (distToTarget <= weaponsRange)
                        ai_state = AI_States.AttackMove;
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
                MoveForward();
                break;
            case AI_States.Fleeing:
                LookAwayFromTarget();
                MoveForward();
                break;
            case AI_States.AttackMove:
                Debug.Log("attacking");
                LookAtTarget();
                MoveForward();
                break;
            case AI_States.StopAndAttack:
                LookAtTarget();
                Debug.Log("attacking");
                break;
            case AI_States.Waiting:
                Debug.Log("waiting");
                break;
            default:
                break;
        }

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

}


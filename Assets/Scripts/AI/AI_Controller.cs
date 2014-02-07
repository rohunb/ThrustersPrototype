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
    public enum AI_States { Hunting,Fleeing,Attacking}
    public AI_States ai_state;

    //ai behaviour variables
    private float distToTarget;
    //flyby
    public float minRange = 20f;
    public float maxRange = 200f;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("AI_Dest").transform;
        
    }
	// Update is called once per frame
    void Update()
    {
        distToTarget = Vector3.Distance(target.position,transform.position);

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

        switch (ai_state)
        {
            case AI_States.Hunting:
                break;
            case AI_States.Fleeing:
                break;
            case AI_States.Attacking:
                break;
            default:
                break;
        }

        LookAtTarget();
        
        MoveForward();
        
        
        
    }
    void LookAtTarget()
    {
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

}


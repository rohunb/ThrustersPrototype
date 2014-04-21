using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]

public class DemoThruster : MonoBehaviour {

    public float maxThrustForce = 1000f;
    public bool damaged;
    
    private Rigidbody spaceship;
    private ParticleSystem[] afterburners;
    private bool firing;

    private Vector3 dirToCOM;
    private Vector3 torque;

    public enum TurnDirection { YawClockwise, YawCounterclock, RollRight, RollLeft, PitchUp, PitchDown }

	// Use this for initialization
	void Start () {
        spaceship = GameObject.Find("DemoShip").rigidbody;
        dirToCOM = spaceship.centerOfMass - transform.localPosition;
        torque = GetTorque();

        afterburners = gameObject.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem afterburner in afterburners)
        {
            afterburner.enableEmission = false;
        }
        firing = false;

	}
	
    public bool CanTurnInDirection(TurnDirection direction)
    {
        switch (direction)
        {
            case TurnDirection.YawClockwise:
                if (torque.y < -.9f)
                    return true;
                break;
            case TurnDirection.YawCounterclock:
                if (torque.y > .9f)
                    return true;
                break;
            case TurnDirection.RollRight:
                if (torque.z > .9f)
                    return true;
                break;
            case TurnDirection.RollLeft:
                if (torque.z < -.9f)
                    return true;
                break;
            case TurnDirection.PitchUp:
                if (torque.x > .9f)
                    return true;
                break;
            case TurnDirection.PitchDown:
                if (torque.x < -.9f)
                    return true;
                break;
            default:
                break;
        }
        return false;
    }
    public bool CanMove(float direction)
    {
        Vector3 relative = transform.InverseTransformDirection(spaceship.transform.forward);
        if (direction > 0f && relative.z > .9f)
            return true;
        else if (direction < 0f && relative.z <-0.9f)
            return true;
        else
            return false;
    }
    public bool CanStrafe(float direction)
    {
        Vector3 relative = transform.InverseTransformDirection(spaceship.transform.right);
        
        if (direction > 0 && relative.z > .9f)
            return true;
        else if (direction < 0 && relative.z < -.9f)
            return true;
        else
            return false;
        
    }

    Vector3 GetTorque()
    {
        Vector3 force = transform.forward * maxThrustForce;
        Vector3 torque = Vector3.Cross(dirToCOM, force);
        return torque;
    }

	void Update () {
        if (!firing)
        {
            foreach (ParticleSystem afterburner in afterburners)
            {
                afterburner.enableEmission = false;
            }
        }
	}
    public void Fire()
    {
        if (!damaged)
        {
            foreach (ParticleSystem afterburner in afterburners)
            {
                afterburner.enableEmission = true;
            }
            firing = true;
            spaceship.AddForceAtPosition(transform.forward * maxThrustForce, transform.position, ForceMode.Force);

            Invoke("StopAfterburner", 0.5f);
        }
    }
    private void StopAfterburner()
    {
        firing = false;

    }
}

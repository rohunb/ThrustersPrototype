using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour 
{
    public float rotationSpeed = 0;
    public float rotateAngle = 0;
    public GameObject target;
    public PhysicsObject myObject;

	void Start () 
    {
        
	}

    void Update()
    {
        if (target)
        {
            transform.Rotate(Vector3.up, rotateAngle);
        }
        else
        {
            transform.Rotate(Vector3.up, rotateAngle);
        }

        if (myObject != null)
        {
            myObject.Update();
            rotateAngle = (float)myObject.Angle;
            Debug.Log(rotateAngle);
        }
    }
}

using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour 
{
    public float rotationSpeed = 0;
    public float rotateAngle = 0;
    public PhysicsObject myObject;

	void Start () 
    {
        
	}

    void Update()
    {
            transform.Rotate(Vector3.up, rotateAngle);

        if (myObject != null)
        {
            myObject.Update();
            rotateAngle = (float)myObject.Angle;
        }
    }
}

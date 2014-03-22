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
        if (myObject != null)
        {
            myObject.Update();
            transform.position = new Vector3(myObject.Position.x, 0, myObject.Position.y);
            //rotateAngle = (float)myObject.Angle; 
            //transform.Rotate(new Vector3(myObject.GravityWell.x, myObject.GravityWell.y, 0), );
            //transform.Rotate(new Vector3(myObject.GravityWell.x, myObject.GravityWell.y, 0), Time.deltaTime * rotateAngle, Space.World);
        }
    }
}

﻿using UnityEngine;
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
        }
    }
}

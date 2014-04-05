using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Transform lookAtShipPos;
    public float cameraMoveSpeed = 4.0f;
    bool lookingAtShip = false;
    bool returningToPos = false;
    Vector3 initialPos;
    Quaternion initialRot;

    DockManager dockManager;

    void Awake()
    {
        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
    }
	
	
    void Update()
    {
        //Debug.Log("returning: "+returningToPos);
        //Debug.Log("looking: " + lookingAtShip);
        if (lookingAtShip)
        {
            transform.position = Vector3.Lerp(transform.position, lookAtShipPos.position, Time.deltaTime*cameraMoveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookAtShipPos.rotation, Time.deltaTime * cameraMoveSpeed);
            if (Vector3.Distance(transform.position, lookAtShipPos.position) < 0.3f)
            {
                transform.position = lookAtShipPos.position;
                transform.rotation = lookAtShipPos.rotation;
                lookingAtShip = false;

            }
        }
        if(returningToPos)
        {
            transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime * cameraMoveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRot, Time.deltaTime * cameraMoveSpeed);
            if(Vector3.Distance(transform.position,initialPos)<0.3f)
            {
                transform.position = initialPos;
                transform.rotation = initialRot;
                returningToPos = false;
                dockManager.PlayerCanMove(true);
                

            }
        }
    }

    public void CameraLookAtShip(Transform initialTrans)
    {
        lookingAtShip = true;
        returningToPos = false;
        initialPos = initialTrans.position;
        initialRot = initialTrans.rotation;
    }
    public void CameraReturnToPos()
    {
        lookingAtShip = false;
        returningToPos = true;
    }
}

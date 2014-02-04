using UnityEngine;
using System.Collections;

public class AimCannon : MonoBehaviour
{
    //Vector3 angles = Vector3.zero;
    private Camera myCamera;
    float zDistance = 200.0f;

    public GameObject goTarget;
    public float maxDegreesPerSecond = 30.0f;
    private Quaternion qTo;

    private Vector3 inputRotation;
    private Vector3 mousePlacement;
    private Vector3 screenCentre;
    private RaycastHit[] hits;
    private Vector3 aimPoint;
    private float closestDistFromCamera = Mathf.Infinity;
    // Use this for initialization
    void Start()
    {
        
        myCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        qTo = goTarget.transform.localRotation;
        aimPoint = Vector3.zero;

    }

    // Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKey("i") && transform.localEulerAngles.y > 1.0f)
    //        transform.Rotate(0.0f, -0.05f, 0.0f);
    //    //aim cannon down
    //    if (Input.GetKey("k") && transform.localEulerAngles.y < 90.0f)
    //        transform.Rotate(0.0f, 0.05f, 0.0f);
    //    angles.y = Mathf.Clamp(transform.localEulerAngles.y, 0.0f, 90.0f);





    //    //var mousePos = Input.mousePosition;
    //    //transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance)));


    //}




    void Update()
    {
        //var v3T = goTarget.transform.position - transform.position;
        //Vector3 v3Aim;
        //v3Aim.x = 0.0f;
        //v3Aim.y = v3T.y;
        //v3T.y = 0.0f;
        //v3Aim.z = v3T.magnitude;
        //qTo = Quaternion.LookRotation(v3Aim, Vector3.up);
        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, qTo, maxDegreesPerSecond * Time.deltaTime);

        //Vector3 mousePos = Input.mousePosition;

        //Vector3 v3T = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance)) - transform.position;
        ////Debug.Log(v3T.y);
        //v3T.y = Mathf.Clamp(v3T.y, 0.0f, 70.0f);
        //qTo = Quaternion.LookRotation(v3T, Vector3.up);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);

        //hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition),Color.red);

        //Debug.Log("hits count: " + hits.Length);
        //for (var i = 0; i < hits.Length; i++)
        //{

        //    RaycastHit hit = hits[i];
        //    if (Vector3.Distance(hit.point, Camera.main.ScreenPointToRay(Input.mousePosition).origin) < closestDistFromCamera)
        //    {
        //        aimPoint = hit.point;
        //        closestDistFromCamera = Vector3.Distance(hit.point, Camera.main.ScreenPointToRay(Input.mousePosition).origin);
        //        //Debug.Log("hits count: " + aimPoint);
        //    }
        //}
        //transform.rotation = Quaternion.LookRotation(aimPoint - transform.position);
        Ray targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(targetRay, out hit))
        {
            qTo = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, maxDegreesPerSecond * Time.deltaTime);
        }
    }
}

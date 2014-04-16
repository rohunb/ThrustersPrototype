using UnityEngine;
using System.Collections;

public class cameraControls : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        MouseOrbit mouseOrbitScript = GameObject.Find("Main Camera").GetComponent<MouseOrbit>() as MouseOrbit;

        if (Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            if (mouseOrbitScript.distance > 10)
            {
                mouseOrbitScript.distance -= 10;
            }
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            if (mouseOrbitScript.distance < 100)
            {
                mouseOrbitScript.distance += 10;
            }
            
        }
    }
}

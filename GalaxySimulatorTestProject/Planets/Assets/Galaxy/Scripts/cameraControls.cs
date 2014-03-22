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
        GameObject camera = GameObject.Find("Camera");
        MouseOrbit mouseOrbitScript = camera.GetComponent<MouseOrbit>() as MouseOrbit;

        if (Input.GetAxis("Mouse ScrollWheel") > 0.1)
        {
            mouseOrbitScript.distance -= 1000;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.1)
        {
            mouseOrbitScript.distance += 1000;
        }
    }
}

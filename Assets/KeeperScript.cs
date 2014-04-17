using UnityEngine;
using System.Collections;

public class KeeperScript : MonoBehaviour {
    private static GameObject[] moons;
    private static GameObject[] planets;

    public static GameObject targetPlanet;

    private static int currMoon = 0;
    private static int currPlanet = 0;

    private static Camera camera;
    private static MouseOrbit camMOscript;

    private static float inputTimer = 0f;
    private static float inputIgnoreTime = 0.25f;
    private static bool ignoreInput = false;

	// Use this for initialization
	void Start () 
    {
        moons = GameObject.FindGameObjectsWithTag("Moon");
        planets = GameObject.FindGameObjectsWithTag("Planet");
        targetPlanet = moons[0];
        camera = Camera.main;
        camMOscript = camera.GetComponent<MouseOrbit>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (!ignoreInput)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                if (currMoon < moons.Length - 1)
                {
                    targetPlanet = moons[++currMoon];
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                if (currMoon > 0)
                {
                    targetPlanet = moons[--currMoon];
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (currPlanet < planets.Length - 1)
                {
                    targetPlanet = planets[++currPlanet];
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (currPlanet > 0)
                {
                    targetPlanet = planets[--currPlanet];
                }
            }

            camMOscript.target = targetPlanet.transform;
            ignoreInput = true;
        }
        else
        {
            inputTimer += Time.deltaTime;

            if (inputTimer > inputIgnoreTime)
            {
                inputTimer = 0;
                ignoreInput = false;
            }
        }
	}
}

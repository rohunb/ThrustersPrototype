using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FTL_GalaxyInfo : MonoBehaviour
{
	Galaxy myGalaxy;
    public GameObject systemRingPrefab;
    List<GameObject> systemRings;
    Vector4 galaxyDimensions; //left, right, top, bottom
    Vector2 galaxyCenterPos = new Vector2(0.0f, 0.0f);
    List<Vector2> starRelativePos;
    float scaleConversionFactor;
    public float galaxyMapSize = 50.0f;


    void Awake()
    {
        myGalaxy = GameObject.Find("Galaxy").GetComponent<Galaxy>();

    }
	void Start ()
	{
        Screen.showCursor = true;
        systemRings = new List<GameObject>();
        starRelativePos = new List<Vector2>();


		//Debug.Log ("[SMBH] " + myGalaxy.superMassiveBlackHole.transform.position + " , " + myGalaxy.superMassiveBlackHole.name);
		
        //foreach (PhysicsObject star in myGalaxy.poStars)
        //{
        //    Debug.Log ("[Star] " + star.Position + " , " + star.name);
        //}

        
        //foreach (PhysicsObject rockPlanet in myGalaxy.poTPlanets)
        //{
        //    Debug.Log ("[RockP] " + rockPlanet.Position + " , " + rockPlanet.name);
        //}
		
        //foreach (PhysicsObject rockMoon in myGalaxy.poTMoons)
        //{
        //    Debug.Log ("[RockM] " + rockMoon.Position + " , " + rockMoon.name);
        //}

        //foreach (PhysicsObject gasGiant in myGalaxy.poGPlanets)
        //{
        //    Debug.Log ("[GasP] " + gasGiant.Position + " , " + gasGiant.name);
        //}
		
        //foreach (PhysicsObject gasGiantMoon in myGalaxy.poGMoons)
        //{
        //    Debug.Log ("[GasM] " + gasGiantMoon.Position + " , " + gasGiantMoon.name);
        //}
        //systemRing = Instantiate(systemRingPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        
        
        galaxyDimensions = new Vector4(0.0f,0.0f,0.0f,0.0f);
        
        foreach (PhysicsObject star in myGalaxy.poStars)
        {
            //getting avg position
            galaxyCenterPos += star.Position;

            if(star.Position.x<galaxyDimensions.x)
            {
                galaxyDimensions.x = star.Position.x;
            }
            else if(star.Position.x>galaxyDimensions.y)
            {
                galaxyDimensions.y = star.Position.x;
            }
            if(star.Position.y<galaxyDimensions.z)
            {
                galaxyDimensions.z = star.Position.y;
            }
            else if(star.Position.y>galaxyDimensions.w)
            {
                galaxyDimensions.w = star.Position.y;
            }

        }

        int numStars = myGalaxy.poStars.Count;
        galaxyCenterPos = numStars > 0 ? galaxyCenterPos / numStars : galaxyCenterPos;

        for (int i = 0; i < numStars; i++)
        {
            GameObject systemRing = Instantiate(systemRingPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            systemRings.Add(systemRing);
        }
        
        float x = Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.x) + Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.y);
        float y = Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.z) + Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.w);
        x/=2.0f;
        y/=2.0f;
        float avg = (x + y) / 2;
        scaleConversionFactor = 5000.0f/ avg;

        for (int i = 0; i < numStars; i++)
        {
            Vector2 relativeDir = myGalaxy.poStars[i].Position - galaxyCenterPos;
            relativeDir *= scaleConversionFactor;
            starRelativePos.Add(relativeDir);
            systemRings[i].transform.position = new Vector3(relativeDir.x,2000.5f,relativeDir.y);
            //systemRings[i].transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
            SystemRing systemRing = systemRings[i].GetComponent<SystemRing>();
            systemRing.ringText.text = myGalaxy.poStars[i].name;
            systemRing.starSystem = myGalaxy.poStars[i];
        }

	}
	
	void Update ()
	{
        galaxyDimensions = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);

        foreach (PhysicsObject star in myGalaxy.poStars)
        {
            //getting avg position
            galaxyCenterPos += star.Position;

            if (star.Position.x < galaxyDimensions.x)
            {
                galaxyDimensions.x = star.Position.x;
            }
            else if (star.Position.x > galaxyDimensions.y)
            {
                galaxyDimensions.y = star.Position.x;
            }
            if (star.Position.y < galaxyDimensions.z)
            {
                galaxyDimensions.z = star.Position.y;
            }
            else if (star.Position.y > galaxyDimensions.w)
            {
                galaxyDimensions.w = star.Position.y;
            }

        }

        int numStars = myGalaxy.poStars.Count;
        galaxyCenterPos = numStars > 0 ? galaxyCenterPos / numStars : galaxyCenterPos;

        float x = Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.x) + Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.y);
        float y = Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.z) + Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.w);
        x /= 2.0f;
        y /= 2.0f;
        float avg = (x + y) / 2;
        scaleConversionFactor = 100.0f / avg;

        for (int i = 0; i < numStars; i++)
        {
            Vector2 relativeDir = myGalaxy.poStars[i].Position - galaxyCenterPos;
            relativeDir *= scaleConversionFactor;
            starRelativePos.Add(relativeDir);
            systemRings[i].transform.position = new Vector3(relativeDir.x, 2000.5f, relativeDir.y);
            //systemRings[i].transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
            SystemRing systemRing = systemRings[i].GetComponent<SystemRing>();
            systemRing.ringText.text = myGalaxy.poStars[i].name;
            systemRing.starSystem = myGalaxy.poStars[i];
        }
	}
}

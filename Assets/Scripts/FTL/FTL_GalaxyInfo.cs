using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FTL_GalaxyInfo : MonoBehaviour
{
	Galaxy myGalaxy;
    public GameObject systemRingPrefab;
    //GameObject systemRing;
    List<GameObject> systemRings;
    Vector4 galaxyDimensions; //left, right, top, bottom
    Vector2 galaxyCenterPos = new Vector2(0.0f, 0.0f);
    List<Vector2> starRelativePos;
    float scaleConversionFactor;
    public float galaxyMapSize = 50.0f;
	// Use this for initialization
	void Start ()
	{
        Screen.showCursor = true;
		myGalaxy = GameObject.Find ("Galaxy").GetComponent<Galaxy>();

		Debug.Log ("[SMBH] " + myGalaxy.superMassiveBlackHole.transform.position + " , " + myGalaxy.superMassiveBlackHole.name);
		
		foreach (PhysicsObject star in myGalaxy.poStars)
		{
			//Debug.Log ("[Star] " + star.Position + " , " + star.name);
		}

        systemRings = new List<GameObject>();
        starRelativePos = new List<Vector2>();
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
        //for (int i = 0; i < myGalaxy.poStars.Count; i++)
        //{
        //    if(myGalaxy )
        //}
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
        //Debug.Log("galaxyDimensions: "+galaxyDimensions);
        //Debug.Log("galaxyCenterPos: "+galaxyCenterPos);
        //Debug.Log("diff xMin: " + (galaxyCenterPos.x - galaxyDimensions.x));
        //Debug.Log("diff xMax: " + (galaxyCenterPos.x - galaxyDimensions.y));
        //Debug.Log("diff yMin: " + (galaxyCenterPos.y - galaxyDimensions.z));
        //Debug.Log("diff yMax: " + (galaxyCenterPos.y - galaxyDimensions.w));
        Debug.Log(myGalaxy.poStars.Count);
        
        float x = Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.x) + Mathf.Abs(galaxyCenterPos.x - galaxyDimensions.y);
        float y = Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.z) + Mathf.Abs(galaxyCenterPos.y - galaxyDimensions.w);
        x/=2.0f;
        y/=2.0f;
        float avg = (x + y) / 2;
        scaleConversionFactor = 35.0f / avg;

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
	
	// Update is called once per frame
	void Update ()
	{
        //Debug.Log(systemRing.transform.position);
        //Debug.Log(systemRings.Count);
	}
}

using UnityEngine;
using System.Collections;

public class FTL_GalaxyInfo : MonoBehaviour
{
	Galaxy myGalaxy;
	// Use this for initialization
	void Start ()
	{
		myGalaxy = GameObject.Find ("Galaxy").GetComponent<Galaxy>();

		Debug.Log ("[SMBH] " + myGalaxy.superMassiveBlackHole.transform.position + " , " + myGalaxy.superMassiveBlackHole.name);
		
		foreach (PhysicsObject star in myGalaxy.poStars)
		{
			Debug.Log ("[Star] " + star.Position + " , " + star.name);
		}
		
		foreach (PhysicsObject rockPlanet in myGalaxy.poTPlanets)
		{
			Debug.Log ("[RockP] " + rockPlanet.Position + " , " + rockPlanet.name);
		}
		
		foreach (PhysicsObject rockMoon in myGalaxy.poTMoons)
		{
			Debug.Log ("[RockM] " + rockMoon.Position + " , " + rockMoon.name);
		}

		foreach (PhysicsObject gasGiant in myGalaxy.poGPlanets)
		{
			Debug.Log ("[GasP] " + gasGiant.Position + " , " + gasGiant.name);
		}
		
		foreach (PhysicsObject gasGiantMoon in myGalaxy.poGMoons)
		{
			Debug.Log ("[GasM] " + gasGiantMoon.Position + " , " + gasGiantMoon.name);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{



	}
}

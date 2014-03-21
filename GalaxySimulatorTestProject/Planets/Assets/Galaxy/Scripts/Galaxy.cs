using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Galaxy : MonoBehaviour 
{
    
    public GameObject theCore;

    public GameObject superMassiveBlackHole;
    public GameObject[] stars;
    public GameObject[] rockPlanets;
    public GameObject[] rockMoons;
    public GameObject[] gasGiants;
    public GameObject[] ggMoons;

    private List<PhysicsObject> poStars;
    private List<PhysicsObject> poTPlanets;
    private List<PhysicsObject> poTMoons;
    private List<PhysicsObject> poGPlanets;
    private List<PhysicsObject> poGMoons;

    public string debugText = "";

    private const int minNumStars = 10;
    private const int maxNumStars = 15;
    private int minStarRange = 100;
    private int maxStarRange = 1000;
    private const int StarRangeIncr = 2000;

    private const int minNumRockPlanetsPerStar = 1;
    private const int maxNumRockPlanetsPerStar = 4;
    private int minRockPlanetRange = 4;
    private int maxRockPlanetRange = 20;
    private const int RockPlanetRangeIncr = 40;

    private const int minNumMoonsPerRockPlanet = 1;
    private const int maxNumMoonsPerRockPlanet = 2;
    private int minRockMoonRange = 4;
    private int maxRockMoonRange = 6;
    private const int RockMoonRangeIncr = 12;

    private const int minNumGasGiants = 1;
    private const int maxNumGasGiants = 6;
    private int minGasGiantRange = 70;
    private int maxGasGiantRange = 150;
    private const int GasGiantRangeIncr = 300;

    private const int minNumMoonsPerGG = 1;
    private const int maxNumMoonsPerGG = 8;
    private int minGGMoonRange = 4;
    private int maxGGMoonRange = 6;
    private const int GGMoonRangeIncr = 12;

    //for adding randomness
    System.Random random = new System.Random();
    int starCount = 0;
    int rockPlanetCount = 0;
    int rockMoonCount = 0;
    int gasGiantCount = 0;
    int gasGiantMoonCount = 0;
    float randX = 0;
    float randY = 0;

    private PhysicsObject objectsInSimulation;

    public PhysicsObject getMainObject()
    {
        return objectsInSimulation;
    }
    PhysicsObject smbh_phy = new PhysicsObject();
    PhysicsObject star_phy, rockPlanet_phy, rockMoon_phy, gasGiant_phy, gasGiantMoon_phy;

    private void Start()
    {

        objectsInSimulation = new PhysicsObject(Vector2.ZERO, 0, PhysicsObjectType.GALAXY);
        objectsInSimulation.addComponent(smbh_phy);
    GameObject blackHole = GameObject.Instantiate(superMassiveBlackHole, Vector3.zero, Quaternion.LookRotation(Vector3.left)) as GameObject;
    blackHole.transform.parent = theCore.transform;

	// add stars
	starCount = random.Next (maxNumStars - minNumStars + 1) + minNumStars;

	for (int i = 0; i < starCount; i++) 
	{
		randX = random.Next (-maxStarRange, maxStarRange);
		while (randX > -minStarRange && randX < minStarRange) 
		{
			randX = random.Next (-maxStarRange, maxStarRange);
		}

		randY = random.Next (-maxStarRange, maxStarRange);
		while (randY > -minStarRange && randY < minStarRange)
		{
			randY = random.Next (-maxStarRange, maxStarRange);
		}

        minStarRange += StarRangeIncr;
        maxStarRange += StarRangeIncr;

			star_phy = new PhysicsObject (new Vector2 (randX, randY), 11, PhysicsObjectType.STAR);
            star_phy.calcRadius(smbh_phy);
            smbh_phy.addComponent(star_phy);
			objectsInSimulation.addComponent (star_phy);
            GameObject starSystem = Instantiate(stars[0], new Vector3(randX, 0, randY), Quaternion.LookRotation(Vector3.left)) as GameObject;
            starSystem.transform.parent = theCore.transform;
            starSystem.GetComponent<rotate>().myObject = star_phy;
            star_phy.rotateScript = starSystem.GetComponent<rotate>();

			//add rock planets to the stars
			rockPlanetCount = random.Next (maxNumRockPlanetsPerStar - minNumRockPlanetsPerStar + 1) + minNumRockPlanetsPerStar;
				for (int j = 0; j < rockPlanetCount; j++) 
				{
				// calculate randX between two ranges on equadistant sides of the centre
				randX = random.Next (-maxRockPlanetRange, maxRockPlanetRange);
				while (randX > -minRockPlanetRange && randX < minRockPlanetRange) {
						randX = random.Next (-maxRockPlanetRange, maxRockPlanetRange);
				}

				// same for randY
				randY = random.Next (-maxRockPlanetRange, maxRockPlanetRange);
				while (randY > -minRockPlanetRange && randY < minRockPlanetRange) {
						randY = random.Next (-maxRockPlanetRange, maxRockPlanetRange);
				}

                minRockPlanetRange += RockPlanetRangeIncr;
                maxRockPlanetRange += RockPlanetRangeIncr;
                
				// create the planet then add it to the parent (star in this case) then add it to the objects the sim tracks
				rockPlanet_phy = new PhysicsObject (new Vector2 (randX + star_phy.Position.X, randY + star_phy.Position.Y), 222, PhysicsObjectType.ROCK_PLANET);
				rockPlanet_phy.calcRadius (star_phy);
				star_phy.addComponent (rockPlanet_phy);
				objectsInSimulation.addComponent(rockPlanet_phy);
                GameObject RockPlanet = Instantiate(rockPlanets[0], new Vector3(rockPlanet_phy.Position.X, 0, rockPlanet_phy.Position.Y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                RockPlanet.transform.parent = starSystem.transform;
                rockPlanet_phy.rotateScript = RockPlanet.GetComponent<rotate>();
                RockPlanet.GetComponent<rotate>().myObject = rockPlanet_phy;

				//add moons to the rock planets
				rockMoonCount = random.Next (maxNumMoonsPerRockPlanet - minNumMoonsPerRockPlanet + 1) + minNumMoonsPerRockPlanet;
				for (int k = 0; k < rockMoonCount; k++)
				{
					randX = random.Next (-maxRockMoonRange, maxRockMoonRange);
					while (randX > -minRockMoonRange && randX < minRockMoonRange)
					{
						randX = random.Next (-maxRockMoonRange, maxRockMoonRange);
					}

					randY = random.Next (-maxRockMoonRange, maxRockMoonRange);
					while (randY > -minRockMoonRange && randY < minRockMoonRange) 
					{
						randY = random.Next (-maxRockMoonRange, maxRockMoonRange);
					}

                    minRockMoonRange += RockMoonRangeIncr;
                    maxRockMoonRange += RockMoonRangeIncr;

					rockMoon_phy = new PhysicsObject (new Vector2 (randX + rockPlanet_phy.Position.X, randY + rockPlanet_phy.Position.Y), 3333, PhysicsObjectType.ROCK_MOON);
					rockMoon_phy.calcRadius (rockPlanet_phy);
					rockPlanet_phy.addComponent (rockMoon_phy);
					objectsInSimulation.addComponent(rockMoon_phy);
                    GameObject RockMoon = Instantiate(rockMoons[0], new Vector3(rockMoon_phy.Position.X, 0, rockMoon_phy.Position.Y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                    RockMoon.transform.parent = RockPlanet.transform;
                    rockMoon_phy.rotateScript = RockMoon.GetComponent<rotate>();
                    RockMoon.GetComponent<rotate>().myObject = rockMoon_phy;
				}
			}

			//add gas planets to the stars
			gasGiantCount = random.Next (maxNumGasGiants - minNumGasGiants + 1) + minNumGasGiants;
			for (int l = 0; l < gasGiantCount; l++) 
			{
				randX = random.Next (-maxGasGiantRange, maxGasGiantRange);
				while (randX > -minGasGiantRange && randX < minGasGiantRange) 
				{
					randX = random.Next (-maxGasGiantRange, maxGasGiantRange);
				}

				randY = random.Next (-maxGasGiantRange, maxGasGiantRange);
				while (randY > -minGasGiantRange && randY < minGasGiantRange) 
				{
					randY = random.Next (-maxGasGiantRange, maxGasGiantRange);
				}

                minGasGiantRange += GasGiantRangeIncr;
                maxGasGiantRange += GasGiantRangeIncr;

				gasGiant_phy = new PhysicsObject (new Vector2 (randX + star_phy.Position.X, randY + star_phy.Position.Y), 44444, PhysicsObjectType.GAS_GIANT);
				gasGiant_phy.calcRadius (star_phy);
				star_phy.addComponent (gasGiant_phy);
				objectsInSimulation.addComponent(gasGiant_phy);
                GameObject GasGiant = Instantiate(gasGiants[0], new Vector3(gasGiant_phy.Position.X, 0, gasGiant_phy.Position.Y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                GasGiant.transform.parent = starSystem.transform;
                gasGiant_phy.rotateScript = GasGiant.GetComponent<rotate>();
                GasGiant.GetComponent<rotate>().myObject = gasGiant_phy;

				//add moons to the gas planets
				gasGiantMoonCount = random.Next (maxNumMoonsPerGG - minNumMoonsPerGG + 1) + minNumMoonsPerGG;
				for (int m = 0; m < gasGiantMoonCount; m++) 
				{

                    randX = random.Next(-maxGGMoonRange, minGGMoonRange);
                    while (randX > -minGGMoonRange && randX < minGGMoonRange) 
					{
                        randX = random.Next(-maxGGMoonRange, minGGMoonRange);
					}

                    randY = random.Next(-maxGGMoonRange, maxGGMoonRange);
                    while (randY > -minGGMoonRange && randY < minGGMoonRange)
					{
                        randY = random.Next(-maxGGMoonRange, maxGGMoonRange);
					}

                    minGGMoonRange += GGMoonRangeIncr;
                    maxGGMoonRange += GGMoonRangeIncr;

					gasGiantMoon_phy = new PhysicsObject (new Vector2 (randX + gasGiant_phy.Position.X, randY + gasGiant_phy.Position.Y), 555555, PhysicsObjectType.G_G_MOON);
					gasGiantMoon_phy.calcRadius (gasGiant_phy);
					gasGiant_phy.addComponent (gasGiantMoon_phy);
					objectsInSimulation.addComponent(gasGiantMoon_phy);
                    GameObject GGMoon = Instantiate(ggMoons[0], new Vector3(gasGiantMoon_phy.Position.X, 0, gasGiantMoon_phy.Position.Y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                    GGMoon.transform.parent = GasGiant.transform;
                    gasGiantMoon_phy.rotateScript = GGMoon.GetComponent<rotate>();
                    GGMoon.GetComponent<rotate>().myObject = gasGiantMoon_phy;
				}
			}
		}
	}
}

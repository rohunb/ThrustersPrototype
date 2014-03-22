using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Galaxy : MonoBehaviour 
{
    private const int BACKGROUND_LAYER = 8;
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

    private GameObject camera;
    public GameObject PlanetImOrbitting;

    public string debugText = "";

    private const int minNumStars = 10;
    private const int maxNumStars = 15;
    private int minStarRange = 8000;
    private int maxStarRange = 10000;
    private int StarRangeIncr;

    private const int minNumRockPlanetsPerStar = 0;
    private const int maxNumRockPlanetsPerStar = 4;
    private int minRockPlanetRange = 100;
    private int maxRockPlanetRange = 150;
    private int RockPlanetRangeIncr;

    private const int minNumMoonsPerRockPlanet = 0;
    private const int maxNumMoonsPerRockPlanet = 2;
    private int minRockMoonRange = 20;
    private int maxRockMoonRange = 30;
    private int RockMoonRangeIncr;

    private const int minNumGasGiants = 0;
    private const int maxNumGasGiants = 6;
    private int minGasGiantRange = 300;
    private int maxGasGiantRange = 750;
    private int GasGiantRangeIncr;

    private const int minNumMoonsPerGG = 1;
    private const int maxNumMoonsPerGG = 8;
    private int minGGMoonRange = 10;
    private int maxGGMoonRange = 25;
    private int GGMoonRangeIncr;

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
        camera = GameObject.Find("BackgroundCamera");

        createGalaxy();

        int randomObjectIndex = UnityEngine.Random.Range(1, objectsInSimulation.getNumComponents() - 1);
        PhysicsObject po;

        po = objectsInSimulation.getComponent(1);
        PlanetImOrbitting = po.myGameObject;

        camera.transform.position = new Vector3(po.Position.x + 200, 0, po.Position.y);
        camera.transform.LookAt(new Vector3(po.Position.x, 0, po.Position.y));

        debugText = "physicsobject: " + po.Position.ToString() + ", target: " + PlanetImOrbitting.transform.position.ToString();
    } //end method

    private void createGalaxy()
    {
        StarRangeIncr = maxStarRange * 2;
        RockPlanetRangeIncr = maxRockPlanetRange * 2;
        RockMoonRangeIncr = maxRockMoonRange * 2;
        GasGiantRangeIncr = maxGasGiantRange * 2;
        GGMoonRangeIncr = maxGGMoonRange * 2;

        objectsInSimulation = new PhysicsObject(Vector2.zero, 0, PhysicsObjectType.GALAXY, random.Next(0, 360));
        objectsInSimulation.addComponent(smbh_phy);
        GameObject blackHole = GameObject.Instantiate(superMassiveBlackHole, new Vector3(-20000, 0, 0), Quaternion.LookRotation(Vector3.left)) as GameObject;
        blackHole.layer = BACKGROUND_LAYER;
        smbh_phy.myGameObject = blackHole;
        blackHole.transform.parent = theCore.transform;

        // add stars
        starCount = random.Next(maxNumStars - minNumStars + 1) + minNumStars;

        int tempMinStarRange = minStarRange;
        int tempMaxStarRanger = maxStarRange;
        for (int i = 0; i < starCount; i++)
        {
            minStarRange += StarRangeIncr;
            maxStarRange += StarRangeIncr;

            randX = random.Next((maxStarRange * -1), maxStarRange);
            while (randX > (minStarRange * -1) && randX < minStarRange)
            {
                randX = random.Next((maxStarRange * -1), maxStarRange);
            }

            randY = random.Next((maxStarRange * -1), maxStarRange);
            while (randY > (minStarRange * -1) && randY < minStarRange)
            {
                randY = random.Next((maxStarRange * -1), maxStarRange);
            }

            star_phy = new PhysicsObject(new Vector2(blackHole.transform.position.x + randX, blackHole.transform.position.y + randY), 11, PhysicsObjectType.STAR, random.Next(0, 360));
            star_phy.calcRadius(smbh_phy);
            smbh_phy.addComponent(star_phy);
            objectsInSimulation.addComponent(star_phy);
            GameObject starSystem = Instantiate(stars[UnityEngine.Random.Range(0, stars.Length)], new Vector3(star_phy.Position.x, 0, star_phy.Position.y), Quaternion.LookRotation(Vector3.left)) as GameObject;
            starSystem.layer = BACKGROUND_LAYER;
            star_phy.myGameObject = starSystem;
            starSystem.transform.parent = theCore.transform;
            starSystem.GetComponent<rotate>().myObject = star_phy;
            star_phy.rotateScript = starSystem.GetComponent<rotate>();

            //add rock planets to the stars
            rockPlanetCount = random.Next(maxNumRockPlanetsPerStar - minNumRockPlanetsPerStar + 1) + minNumRockPlanetsPerStar;

            int tempMinRPRange = minRockPlanetRange;
            int tempMaxRPRange = maxRockPlanetRange;

            for (int j = 0; j < rockPlanetCount; j++)
            {

                minRockPlanetRange += RockPlanetRangeIncr;
                maxRockPlanetRange += RockPlanetRangeIncr;

                // calculate randX between two ranges on equadistant sides of the centre
                randX = random.Next((maxRockPlanetRange * -1), maxRockPlanetRange);
                while (randX > (minRockPlanetRange * -1) && randX < minRockPlanetRange)
                {
                    randX = random.Next((maxRockPlanetRange * -1), maxRockPlanetRange);
                }

                // same for randY
                randY = random.Next((maxRockPlanetRange * -1), maxRockPlanetRange);
                while (randY > (minRockPlanetRange * -1) && randY < minRockPlanetRange)
                {
                    randY = random.Next((maxRockPlanetRange * -1), maxRockPlanetRange);
                }

                // create the planet then add it to the parent (star in this case) then add it to the objects the sim tracks
                rockPlanet_phy = new PhysicsObject(new Vector2(randX + star_phy.Position.x, randY + star_phy.Position.y), 222, PhysicsObjectType.ROCK_PLANET, random.Next(0, 360));
                rockPlanet_phy.calcRadius(star_phy);
                star_phy.addComponent(rockPlanet_phy);
                objectsInSimulation.addComponent(rockPlanet_phy);
                GameObject RockPlanet = Instantiate(rockPlanets[UnityEngine.Random.Range(0, rockPlanets.Length - 1)], new Vector3(rockPlanet_phy.Position.x, 0, rockPlanet_phy.Position.y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                RockPlanet.layer = BACKGROUND_LAYER;
                RockPlanet.tag = "Planet";
                rockPlanet_phy.myGameObject = RockPlanet;
                RockPlanet.transform.parent = starSystem.transform;
                rockPlanet_phy.rotateScript = RockPlanet.GetComponent<rotate>();
                RockPlanet.GetComponent<rotate>().myObject = rockPlanet_phy;

                //add moons to the rock planets
                rockMoonCount = random.Next(maxNumMoonsPerRockPlanet - minNumMoonsPerRockPlanet + 1) + minNumMoonsPerRockPlanet;

                int tempMinRMRange = minRockMoonRange;
                int tempMaxRMRange = maxRockMoonRange;
                for (int k = 0; k < rockMoonCount; k++)
                {
                    minRockMoonRange += RockMoonRangeIncr;
                    maxRockMoonRange += RockMoonRangeIncr;

                    randX = random.Next((maxRockMoonRange * -1), maxRockMoonRange);
                    while (randX > (minRockMoonRange * -1) && randX < minRockMoonRange)
                    {
                        randX = random.Next((maxRockMoonRange * -1), maxRockMoonRange);
                    }

                    randY = random.Next((maxRockMoonRange * -1), maxRockMoonRange);
                    while (randY > (minRockMoonRange * -1) && randY < minRockMoonRange)
                    {
                        randY = random.Next((maxRockMoonRange * -1), maxRockMoonRange);
                    }

                    rockMoon_phy = new PhysicsObject(new Vector2(randX + rockPlanet_phy.Position.x, randY + rockPlanet_phy.Position.y), 3333, PhysicsObjectType.ROCK_MOON, random.Next(0, 360));
                    rockMoon_phy.calcRadius(rockPlanet_phy);
                    rockPlanet_phy.addComponent(rockMoon_phy);
                    objectsInSimulation.addComponent(rockMoon_phy);
                    GameObject RockMoon = Instantiate(rockMoons[UnityEngine.Random.Range(0, rockMoons.Length - 1)], new Vector3(rockMoon_phy.Position.x, 0, rockMoon_phy.Position.y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                    RockMoon.layer = BACKGROUND_LAYER;
                    rockMoon_phy.myGameObject = RockMoon;
                    RockMoon.transform.parent = RockPlanet.transform;
                    rockMoon_phy.rotateScript = RockMoon.GetComponent<rotate>();
                    RockMoon.GetComponent<rotate>().myObject = rockMoon_phy;
                }// end rock moon for loop
                minRockMoonRange = tempMinRMRange;
                maxRockMoonRange = tempMaxRMRange;
            }// end rock planet for loop
            minRockPlanetRange = tempMinRPRange;
            maxRockPlanetRange = tempMaxRPRange;
            //add gas planets to the stars
            gasGiantCount = random.Next(maxNumGasGiants - minNumGasGiants + 1) + minNumGasGiants;

            int tempMinGGPlanet = minGasGiantRange;
            int tempMaxGGPlanet = maxGasGiantRange;
            for (int l = 0; l < gasGiantCount; l++)
            {
                minGasGiantRange += GasGiantRangeIncr;
                maxGasGiantRange += GasGiantRangeIncr;

                randX = random.Next((maxGasGiantRange * -1), maxGasGiantRange);
                while (randX > (minGasGiantRange * -1) && randX < minGasGiantRange)
                {
                    randX = random.Next((maxGasGiantRange * -1), maxGasGiantRange);
                }

                randY = random.Next((maxGasGiantRange * -1), maxGasGiantRange);
                while (randY > (minGasGiantRange * -1) && randY < minGasGiantRange)
                {
                    randY = random.Next((maxGasGiantRange * -1), maxGasGiantRange);
                }

                gasGiant_phy = new PhysicsObject(new Vector2(randX + star_phy.Position.x, randY + star_phy.Position.y), 44444, PhysicsObjectType.GAS_GIANT, random.Next(0, 360));
                gasGiant_phy.calcRadius(star_phy);
                star_phy.addComponent(gasGiant_phy);
                objectsInSimulation.addComponent(gasGiant_phy);
                GameObject GasGiant = Instantiate(gasGiants[UnityEngine.Random.Range(0, gasGiants.Length - 1)], new Vector3(gasGiant_phy.Position.x, 0, gasGiant_phy.Position.y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                GasGiant.layer = BACKGROUND_LAYER;
                gasGiant_phy.myGameObject = GasGiant;
                GasGiant.transform.parent = starSystem.transform;
                gasGiant_phy.rotateScript = GasGiant.GetComponent<rotate>();
                GasGiant.GetComponent<rotate>().myObject = gasGiant_phy;

                //add moons to the gas planets
                gasGiantMoonCount = random.Next(maxNumMoonsPerGG - minNumMoonsPerGG + 1) + minNumMoonsPerGG;

                int tempMinGGMoonRange = minGGMoonRange;
                int tempMaxGGMoonRange = maxGGMoonRange;
                for (int m = 0; m < gasGiantMoonCount; m++)
                {
                    minGGMoonRange += GGMoonRangeIncr;
                    maxGGMoonRange += GGMoonRangeIncr;

                    randX = random.Next((maxGGMoonRange * -1), minGGMoonRange);
                    while (randX > (minGGMoonRange * -1) && randX < minGGMoonRange)
                    {
                        randX = random.Next((maxGGMoonRange * -1), minGGMoonRange);
                    }

                    randY = random.Next((maxGGMoonRange * -1), maxGGMoonRange);
                    while (randY > (minGGMoonRange * -1) && randY < minGGMoonRange)
                    {
                        randY = random.Next((maxGGMoonRange * -1), maxGGMoonRange);
                    }

                    gasGiantMoon_phy = new PhysicsObject(new Vector2(randX + gasGiant_phy.Position.x, randY + gasGiant_phy.Position.y), 555555, PhysicsObjectType.G_G_MOON, random.Next(0, 360));
                    gasGiantMoon_phy.calcRadius(gasGiant_phy);
                    gasGiant_phy.addComponent(gasGiantMoon_phy);
                    objectsInSimulation.addComponent(gasGiantMoon_phy);
                    GameObject GGMoon = Instantiate(ggMoons[UnityEngine.Random.Range(0, ggMoons.Length - 1)], new Vector3(gasGiantMoon_phy.Position.x, 0, gasGiantMoon_phy.Position.y), Quaternion.LookRotation(Vector3.left)) as GameObject;
                    GGMoon.layer = BACKGROUND_LAYER;
                    GGMoon.tag = "Planet";
                    gasGiantMoon_phy.myGameObject = GGMoon;
                    GGMoon.transform.parent = GasGiant.transform;
                    gasGiantMoon_phy.rotateScript = GGMoon.GetComponent<rotate>();
                    GGMoon.GetComponent<rotate>().myObject = gasGiantMoon_phy;
                }// end gas giant moon for loop
                minGGMoonRange = tempMinGGMoonRange;
                maxGGMoonRange = tempMaxGGMoonRange;
            }// end gas giant for loop
            minGasGiantRange = tempMinGGPlanet;
            maxGasGiantRange = tempMaxGGPlanet;
        } //end star for loop
        minStarRange = tempMinStarRange;
        maxStarRange = tempMaxStarRanger;
    }
} //end class

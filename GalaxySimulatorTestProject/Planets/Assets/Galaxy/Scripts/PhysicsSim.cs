using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PhysicsSim
{
//the sim keeps running until isRunning == false;
public bool isRunning = true;

//setup variables
private const int minNumStars = 1;
private const int maxNumStars = 1;
private const int minStarRange = 10;
private const int maxStarRange = 100;

private const int minNumRockPlanetsPerStar = 1;
private const int maxNumRockPlanetsPerStar = 1;
private const int minRockPlanetRange = 1;
private const int maxRockPlanetRange = 10;

private const int minNumMoonsPerRockPlanet = 1;
private const int maxNumMoonsPerRockPlanet = 1;
private const int minRockMoonRange = 1;
private const int maxRockMoonRange = 10;

private const int minNumGasGiants = 1;
private const int maxNumGasGiants = 1;
private const int minGasGiantRange = 10;
private const int maxGasGiantRange = 100;

private const int minNumMoonsPerGG = 1;
private const int maxNumMoonsPerGG = 1;
private const int minGGMoonRange = 1;
private const int maxGGMoonRange = 10;

//for adding randomness
Random random = new Random();
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

//smbh is the root node. all other physics objects are within it.
PhysicsObject superMassiveBlackHole = new PhysicsObject();

//these vars get reused to create all the new Objects;
PhysicsObject star, rockPlanet, rockMoon, gasGiant, gasGiantMoon;

//public string debugText = "test debug text";

public PhysicsSim()
{
    objectsInSimulation = new PhysicsObject(Vector2.ZERO, 0, PhysicsObjectType.GALAXY);
    objectsInSimulation.addComponent(superMassiveBlackHole);

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

			star = new PhysicsObject (new Vector2 (randX, randY), 11, PhysicsObjectType.STAR);
			star.calcRadius (superMassiveBlackHole);
			star.calcPeriodInSeconds ();
			star.calcTheRest ();
			superMassiveBlackHole.addComponent (star);
			objectsInSimulation.addComponent (star);

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

				// create the planet then add it to the parent (star in this case) then add it to the objects the sim tracks
				rockPlanet = new PhysicsObject (new Vector2 (randX + star.Position.X, randY + star.Position.Y), 222, PhysicsObjectType.ROCK_PLANET);
				rockPlanet.calcRadius (star);
				rockPlanet.calcPeriodInSeconds ();
				rockPlanet.calcTheRest ();
				star.addComponent (rockPlanet);
				objectsInSimulation.addComponent(rockPlanet);

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

					rockMoon = new PhysicsObject (new Vector2 (randX + rockPlanet.Position.X, randY + rockPlanet.Position.Y), 3333, PhysicsObjectType.ROCK_MOON);
					rockMoon.calcRadius (rockPlanet);
					rockMoon.calcPeriodInSeconds ();
					rockMoon.calcTheRest ();
					rockPlanet.addComponent (rockMoon);
					objectsInSimulation.addComponent(rockMoon);
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

				gasGiant = new PhysicsObject (new Vector2 (randX + star.Position.X, randY + star.Position.Y), 44444, PhysicsObjectType.GAS_GIANT);
				gasGiant.calcRadius (star);
				gasGiant.calcPeriodInSeconds ();
				gasGiant.calcTheRest ();
				star.addComponent (gasGiant);
				objectsInSimulation.addComponent(gasGiant);

				//add moons to the gas planets
				gasGiantMoonCount = random.Next (maxNumMoonsPerGG - minNumMoonsPerGG + 1) + minNumMoonsPerGG;
				for (int m = 0; m < gasGiantMoonCount; m++) 
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

					gasGiantMoon = new PhysicsObject (new Vector2 (randX + gasGiant.Position.X, randY + gasGiant.Position.Y), 555555, PhysicsObjectType.G_G_MOON);
					gasGiantMoon.calcRadius (gasGiant);
					gasGiantMoon.calcPeriodInSeconds ();
					gasGiantMoon.calcTheRest ();
					gasGiant.addComponent (gasGiantMoon);
					objectsInSimulation.addComponent(gasGiantMoon);
				}
			}
		}
	}

	public void Update()
	{
	    
	}

    public void DisplayText()
    {
        PhysicsObject contianer = objectsInSimulation;

        //debugText += contianer.Text();
        foreach (PhysicsObject SMBH in contianer.getComponents())
        {
            //debugText += SMBH.Text();
            foreach (PhysicsObject star in SMBH.getComponents())
            {
                //debugText += star.Text();
                foreach (PhysicsObject planet in star.getComponents())
                {
                    //debugText += planet.Text();
                    foreach (PhysicsObject moon in planet.getComponents())
                    {
                        //debugText += moon.Text();  
                    }
                }
            }
        }
    }

}


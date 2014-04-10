using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentInventory : MonoBehaviour
{
    //public List<Weapon> availableWeapons;
    //public List<Weapon> equippedWeaponsList;
    public int numberOfHardpoints = 4;
    //public Weapon[] equippedWeapons;

    public GameObject laser;
    public GameObject railgun;
    public GameObject missile;
    public GameObject clusterMissile;
    public GameObject torpedo;
    public GameObject miningLaser;

    public List<string> availableWeapons;
    public string[] equippedWeapons;


    /*
    Laser Cannon
    Mining Laser
    Missile Launcher
    Railgun
    Torpedo Launcher
     */

	
	void Start () {
        //availableWeapons = new List<string>();
        //equippedWeapons = new string[numberOfHardpoints];
        //equippedWeapons[0] = "Railgun";
		for (int i = 0; i < numberOfHardpoints; i++)
		{
			//equippedWeapons[i] = "";
            Debug.Log(equippedWeapons[i]);
		}

        //equippedWeapons = new Weapon[numberOfHardpoints];
        //int i = 0;
        //////for (int i = 0; i < equippedWeaponsList.Count; i++)
        //while (i < equippedWeaponsList.Count)
        //{
        //    equippedWeapons[i] = equippedWeaponsList[i];
        //    i++;
        //}
        
        //equippedWeapons[1] = "Railgun";
        Debug.Log("start persistent");
        //equippedWeapons[0] = "Laser";
//        equippedWeapons[1] = "Mining Laser";
//        equippedWeapons[2] = "Mining Laser";
//        equippedWeapons[3] = "Mining Laser";

       


	}
    //void OnLevelWasLoaded(int level)
    //{
    //    if (Application.loadedLevelName == "GameScene" || Application.loadedLevelName == "DockedScene")
    //    {
    //        int i = 0;
    //        //for (int i = 0; i < equippedWeaponsList.Count; i++)
    //        while (i < equippedWeaponsList.Count)
    //        {
    //            equippedWeapons[i] = equippedWeaponsList[i];
    //            i++;
    //        }
    //    }
    //}
    //public void SaveInventory(Weapon[] _equippedWeapons, List<Weapon> _availableWeapons)
    //{
    //    equippedWeapons = _equippedWeapons;
    //    availableWeapons = _availableWeapons;
    //    foreach (Weapon weapon in equippedWeapons)
    //    {
    //        weapon.transform.parent = transform;
    //    }
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        weapon.transform.parent = transform;
    //    }
    //}
	// Update is called once per frame
	//void Update () {
        //for (int i = 0; i < equippedWeapons.Length; i++)
        //{
        //    if(equippedWeapons[i])
        //    {
        //       // Debug.Log(equippedWeapons[i].gameObject.name);
        //    }
        //}	 
	//}
    /*
     * if (equippedWeapons[hardpointIndex])
        {
            //equippedWeapons[hardpointSelected].gameObject.SetActive(false);
            availableWeapons.Add(equippedWeapons[hardpointIndex]);
            availableWeapons[availableWeapons.Count - 1].transform.position = inventoryLoc.position;
            //availableWeapons[availableWeapons.Count - 1].gameObject.SetActive(false);
            availableWeapons[availableWeapons.Count - 1].gameObject.GetComponent<Weapon>().enabled = false;
        }

        equippedWeapons[hardpointIndex] = availableWeapons[availableWeaponsIndex];
        equippedWeapons[hardpointIndex].transform.position = hardPoints[hardpointIndex].position;
        //equippedWeapons[hardpointSelected].gameObject.SetActive(true);
        equippedWeapons[hardpointIndex].gameObject.GetComponent<Weapon>().enabled = true;
        availableWeapons.Remove(availableWeapons[availableWeaponsIndex]);*/
}

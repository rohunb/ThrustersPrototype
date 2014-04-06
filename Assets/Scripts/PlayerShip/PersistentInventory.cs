using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentInventory : MonoBehaviour {
    
    public List<Weapon> availableWeapons;
    public List<Weapon> equippedWeaponsList;
    public int numberOfHardpoints = 4;
    public Weapon[] equippedWeapons;


	// Use this for initialization
	void Start () {
        equippedWeapons = new Weapon[numberOfHardpoints];
        int i = 0;
        //for (int i = 0; i < equippedWeaponsList.Count; i++)
        while(i<equippedWeaponsList.Count)
        {
            equippedWeapons[i] = equippedWeaponsList[i];
            i++;
        }
        

	}
    void OnLevelWasLoaded(int level)
    {
        if (Application.loadedLevelName == "GameScene" || Application.loadedLevelName == "DockedScene")
        {
            int i = 0;
            //for (int i = 0; i < equippedWeaponsList.Count; i++)
            while (i < equippedWeaponsList.Count)
            {
                equippedWeapons[i] = equippedWeaponsList[i];
                i++;
            }
        }
    }
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

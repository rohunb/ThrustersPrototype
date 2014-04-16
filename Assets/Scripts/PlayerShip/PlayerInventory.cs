using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public GUISkin guiSkin;

    public List<Weapon> availableWeapons;

    public int numberOfHardpoints = 4;
    public Weapon[] equippedWeapons;
    public Transform[] hardPoints;
    public Transform inventoryLoc;
    public Transform weaponTransform;

    public GameObject laser;
    public GameObject railgun;
    public GameObject missile;
    public GameObject clusterMissile;
    public GameObject torpedo;
    public GameObject miningLaser;

    PersistentInventory godInventory;
    GameObject god;

    //inventory GUI
    private bool displayInventory = false;
    private bool showAvailableWeapons = false;
    private bool showMissionLog = true;
    private int hardpointSelected;

    private Rect equippedWindow;
    private Rect availableWindow;
    public Vector2 buttonSize = new Vector2(140, 20);

    //economy
    private int credits = 400;



    void Awake()
    {
        god = GameObject.Find("GOD");
        godInventory = god.GetComponent<PersistentInventory>();

    }
    void Start()
    {

        credits = 400;
        //availableWeapons = godInventory.availableWeapons;

        equippedWeapons = new Weapon[numberOfHardpoints];

        equippedWindow = new Rect(10, Screen.height / 2 - 200, 175, numberOfHardpoints * 30);

        LoadInventory();
        //availableWindow = new Rect(240, Screen.height / 2 - 200, 175, availableWeapons.Count * 30);

        //foreach (Weapon weapon in availableWeapons)
        //{
        //    weapon.transform.position = inventoryLoc.position;
        //    weapon.GetComponent<Weapon>().enabled = false;
        //    weapon.origin = gameObject;
        //}
        //for (int i = 0; i < equippedWeapons.Length; i++)
        //{

        //    if (!equippedWeapons[i] && godInventory.equippedWeapons[i])
        //    {
        //        GameObject weaponObj = Instantiate(godInventory.equippedWeapons[i].gameObject, hardPoints[i].position, hardPoints[i].rotation) as GameObject;
        //        Weapon weapon = weaponObj.GetComponent<Weapon>();
        //        weapon.Init();
        //        weapon.origin = gameObject;
        //        equippedWeapons[i] = weapon;
        //        weapon.transform.parent = weaponTransform;
        //        weapon.transform.position = hardPoints[i].position;
        //        //weapon.enabled = true;

        //    }
        //}

        //foreach (Weapon weapon in availableWeapons)
        //{
        //    weapon.origin = gameObject;
        //}
    }
    public void SaveInventory()
    {
        //godInventory.equippedWeapons = equippedWeapons;
        //godInventory.availableWeapons = availableWeapons;

        //Debug.Log("save inventory");
        //Debug.Log(Application.loadedLevelName);
        //Debug.Log(gameObject.name);
        //godInventory.availableWeapons.Clear();
        //foreach (Weapon weapon in availableWeapons)
        //{
        //    GameObject wpnObj = Instantiate(weapon.gameObject) as GameObject;
        //    //wpnObj.transform.parent = godInventory.gameObject.transform;
        //    wpnObj.transform.parent = god.transform;
        //    //wpnObj.transform.position = Vector3.zero;
        //    godInventory.availableWeapons.Add(wpnObj.GetComponent<Weapon>());
        //}
        //godInventory.equippedWeaponsList.Clear();
        //for (int i = 0; i < equippedWeapons.Length; i++)
        //{
        //    if (equippedWeapons[i])
        //    {
        //        GameObject wpnObj = Instantiate(equippedWeapons[i].gameObject) as GameObject;
        //        wpnObj.transform.parent = god.transform;
        //        wpnObj.transform.position = Vector3.zero;
        //        godInventory.equippedWeapons[i] = (wpnObj.GetComponent<Weapon>());
        //    }
        //    else
        //    {
        //        godInventory.equippedWeapons[i] = null;
        //    }
        //}
        //godInventory.equippedWeapons = equippedWeapons;
        //godInventory.availableWeapons = availableWeapons;

        //godInventory.SaveInventory(equippedWeapons, availableWeapons);

        godInventory.availableWeapons.Clear();
        foreach (Weapon weapon in availableWeapons)
        {
            godInventory.availableWeapons.Add(weapon.wpnName);
        }

        for (int i = 0; i < numberOfHardpoints; i++)
        {
            //Debug.Log(equippedWeapons[i].name);
            if(equippedWeapons[i]==null)
            {
				//Debug.Log("Saving to " + i + "none");
				godInventory.equippedWeapons[i] = "";

            }
            else
            {
				//Debug.Log("Saving to "+i+" " +equippedWeapons[i].wpnName);
				godInventory.equippedWeapons[i] = equippedWeapons[i].wpnName; 
			}
		}
		
	}
	public void LoadInventory()
    {
		//Debug.Log("loading");
        //availableWeapons = godInventory.availableWeapons;
        availableWindow = new Rect(240, Screen.height / 2 - 200, 175, availableWeapons.Count * 30);

        //foreach (Weapon weapon in godInventory.availableWeapons)
        //{
        //    GameObject weaponObj = Instantiate(weapon.gameObject, inventoryLoc.position, inventoryLoc.rotation) as GameObject;
        //    weaponObj.transform.parent = weaponTransform;
        //    weaponObj.transform.position = Vector3.zero;
        //    Weapon wpn = weapon.GetComponent<Weapon>();
        //    wpn.enabled = false;
        //    wpn.origin = gameObject;
        //    availableWeapons.Add(wpn);

        //}
        //for (int i = 0; i < equippedWeapons.Length; i++)
        //{

        //    if (godInventory.equippedWeapons[i])
        //    {
        //        GameObject weaponObj = Instantiate(godInventory.equippedWeapons[i].gameObject, hardPoints[i].position, hardPoints[i].rotation) as GameObject;
        //        Weapon weapon = weaponObj.GetComponent<Weapon>();
        //        weapon.Init();
        //        weapon.origin = gameObject;
        //        equippedWeapons[i] = weapon;
        //        weapon.transform.parent = weaponTransform;
        //        weapon.transform.position = hardPoints[i].position;
        //        //weapon.enabled = true;
        //    }
        //    else
        //    {
        //        equippedWeapons[i] = null;
        //    }
        //}
        foreach (string wpnName in godInventory.availableWeapons)
        {
            GameObject weaponObj = Instantiate(GetWpnObj(wpnName)) as GameObject;
            weaponObj.transform.parent = weaponTransform;
            weaponObj.transform.position = Vector3.zero;
            Weapon wpn = weaponObj.GetComponent<Weapon>();
            wpn.enabled = false;
            wpn.origin = gameObject;
            availableWeapons.Add(wpn);
        }
        for (int i = 0; i < numberOfHardpoints; i++)
        {
            if(godInventory.equippedWeapons[i]!="")
            {

                GameObject weaponObj = Instantiate(GetWpnObj(godInventory.equippedWeapons[i])) as GameObject;
				weaponObj.transform.parent = weaponTransform;
				weaponObj.transform.position = hardPoints[i].position;
				weaponObj.transform.rotation = hardPoints[i].rotation;
				Weapon weapon = weaponObj.GetComponent<Weapon>();
                weapon.Init();
                weapon.origin = gameObject;
                equippedWeapons[i] = weapon;
                
                //weapon.enabled = true;
            }
            else
            {
                equippedWeapons[i] = null;
            }
        }

    }
    void OnDisable()
    {
        //godInventory.availableWeapons.Clear();
        //foreach (Weapon weapon in availableWeapons)
        //{
        //    GameObject wpnObj = Instantiate(weapon.gameObject)
        //}
        //godInventory.equippedWeapons = equippedWeapons;
        //godInventory.availableWeapons = availableWeapons;
        //SaveInventory();
    }
    GameObject GetWpnObj(string wpnName)
    {
        /*
    Laser Cannon
    Mining Laser
    Missile Launcher
    Railgun
    Torpedo Launcher
     */

        switch (wpnName)
        {
            case "Laser Cannon":
                return laser;
            case "Railgun":
                return railgun;
            case "Missile Launcher":
                return missile;
            case "Cluster Missiles":
                return clusterMissile;
            case "Torpedo Launcher":
                return torpedo;
            case "Mining Laser":
                return miningLaser;
            default:
                return laser;
        }
    }
    public int GetCredits()
    {
        return credits;
    }

    public bool CreateTransaction(int amount)
    {
        if (credits + amount >= 0)
        {
            credits += amount;
            return true;
        }
        else
            return false;
    }
    
    public void EquipWeaponFromHold(int hardpointIndex, int availableWeaponsIndex)
    {
        Debug.Log(hardpointIndex);
        if (equippedWeapons[hardpointIndex])
        {
            //equippedWeapons[hardpointSelected].gameObject.SetActive(false);
            availableWeapons.Add(equippedWeapons[hardpointIndex]);
            availableWeapons[availableWeapons.Count - 1].transform.position = inventoryLoc.position;
            //availableWeapons[availableWeapons.Count - 1].gameObject.SetActive(false);
            availableWeapons[availableWeapons.Count - 1].gameObject.GetComponent<Weapon>().enabled = false;
        }

        equippedWeapons[hardpointIndex] = availableWeapons[availableWeaponsIndex];
        equippedWeapons[hardpointIndex].transform.position = hardPoints[hardpointIndex].position;
        equippedWeapons[hardpointIndex].transform.rotation = hardPoints[hardpointIndex].rotation;
        equippedWeapons[hardpointSelected].gameObject.SetActive(true);
        equippedWeapons[hardpointIndex].gameObject.GetComponent<Weapon>().enabled = true;
        availableWeapons.Remove(availableWeapons[availableWeaponsIndex]);
    }
    public void UnequipWeapon(int hardpointIndex)
    {
        if(equippedWeapons[hardpointIndex])
        {
            availableWeapons.Add(equippedWeapons[hardpointIndex]);
            availableWeapons[availableWeapons.Count - 1].transform.position = inventoryLoc.position;
            availableWeapons[availableWeapons.Count - 1].gameObject.GetComponent<Weapon>().enabled = false;
            equippedWeapons[hardpointIndex] = null;
        }
    }
    public void AddWeaponToCargo(GameObject _weapon)
    {
        Weapon weapon = _weapon.GetComponent<Weapon>();
        weapon.transform.parent = weaponTransform;
        weapon.transform.position = Vector3.zero;
        weapon.transform.rotation = Quaternion.identity;

        availableWeapons.Add(weapon);

    }

    void OnGUI()
    {
        GUI.skin = guiSkin;
        if (Application.loadedLevelName == "GameScene")
        {
            DisplayEquippedWeapons();
            DisplayMinorOptions();
            if (showMissionLog) DisplayMissions();
        }

        if (displayInventory)
        {

            equippedWindow = GUI.Window(0, equippedWindow, EquippedWindow, "Equipped Weapons");
            if (showAvailableWeapons)
            {
                availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Available Weapons");
            }
        }

    }

    void DisplayEquippedWeapons()
    {
        Rect equippedWeaponsRect = new Rect(Screen.width / 2 - 210, Screen.height - 80, 420, 70);
        GUI.Box(equippedWeaponsRect, "Weapons");
        GUI.BeginGroup(equippedWeaponsRect);

        for (int i = 0; i < 4; i++)
        {
            string wpnName;
            if (equippedWeapons[i])
                wpnName = equippedWeapons[i].wpnName;
            else
                wpnName = "------";

            GUI.Button(new Rect(10 + (100 * i), 25, 100, 40), "Wpn " + (i + 1) + ":\n" + wpnName);
        }
        GUI.EndGroup();
    }

    void DisplayMissions()
    {
        Rect missionsRect = new Rect(Screen.width - 260, Screen.height / 2 - 200, 250, 400);
        GUI.Box(missionsRect, "Missions");
        GUI.BeginGroup(missionsRect);
        GUI.EndGroup();
    }

    void DisplayMinorOptions()
    {
        if (GUI.Button(new Rect(Screen.width - 90, 0, 30, 20), "M"))
        {

        }
        if (GUI.Button(new Rect(Screen.width - 60, 0, 30, 20), "S"))
        {

        }
        if (GUI.Button(new Rect(Screen.width - 30, 0, 30, 20), "O"))
        {

        }
    }

    void EquippedWindow(int windowID)
    {
        string buttonText;
        for (int i = 0; i < equippedWeapons.Length; i++)
        {
            GUI.Label(new Rect(5, 20 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (equippedWeapons[i])
                buttonText = equippedWeapons[i].wpnName;
            else
                buttonText = "------";
            if (GUI.Button(new Rect(15, 20 + i * 20, 150, 20), buttonText))
            {
                showAvailableWeapons = true;
                hardpointSelected = i;
            }

        }

    }
    void ShowAvailableWeapons()
    {
        equippedWindow = GUI.Window(1, equippedWindow, AvailableWindow, "Equipped Weapons");
    }
    void AvailableWindow(int windowID)
    {
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            GUI.Label(new Rect(5, 20 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (GUI.Button(new Rect(15, 20 + i * 20, 150, 20), availableWeapons[i].wpnName))
            {
                if (equippedWeapons[hardpointSelected])
                {
                    //equippedWeapons[hardpointSelected].gameObject.SetActive(false);
                    availableWeapons.Add(equippedWeapons[hardpointSelected]);
                    availableWeapons[availableWeapons.Count - 1].transform.position = inventoryLoc.position;
                    //availableWeapons[availableWeapons.Count - 1].gameObject.SetActive(false);
                    availableWeapons[availableWeapons.Count - 1].gameObject.GetComponent<Weapon>().enabled = false;
                }

                equippedWeapons[hardpointSelected] = availableWeapons[i];
                equippedWeapons[hardpointSelected].transform.position = hardPoints[hardpointSelected].position;
                //equippedWeapons[hardpointSelected].gameObject.SetActive(true);
                equippedWeapons[hardpointSelected].gameObject.GetComponent<Weapon>().enabled = true;
                availableWeapons.Remove(availableWeapons[i]);

            }
        }
    }



    public void ToggleInventory()
    {
        displayInventory = !displayInventory;

    }

    public void ToggleMissionLog()
    {
        showMissionLog = !showMissionLog;
    }
}

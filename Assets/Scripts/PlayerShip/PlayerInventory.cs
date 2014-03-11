using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{

    //equipped weapons
    //public Weapon primaryWeapon;
    //public Weapon secondaryWeapon;
    //public Weapon tertiaryWeapon;
    //public Weapon utilityWeapon;
    
    public List<Weapon> availableWeapons;

    public int numberOfHardpoints = 4;
    public Weapon[] equippedWeapons;

    //inventory GUI
    private bool displayInventory = false;
    private bool showAvailableWeapons = false;
    private bool showMissionLog = true;
    private int hardpointSelected;
    //public Rect primaryWindow = new Rect(10, Screen.height / 2 - 200, 200, 100);
    //public Rect secondaryWindow = new Rect(10, Screen.height / 2 - 100, 200, 100);
    //public Rect tertiaryWindow = new Rect(10, Screen.height / 2 , 200, 100);
    //public Rect utilityWindow = new Rect(10, Screen.height / 2+100, 200, 100);
    private Rect equippedWindow;
    private Rect availableWindow;
    public Vector2 buttonSize=new Vector2(140,20);
    ////lasers
    //public int laserDamage = 10;
    //public float laserSpeed = 1000;
    //public float laserReloadTimer = 0.2f;
    //public float laserRange = 2000f;

    ////railgun
    //public int railDamage = 1000;
    //public float railSpeed = 3000f;
    //public float railReloadTimer = 2.0f;
    //public float railRange = 2500f;

    ////missiles
    //public int missileDamage = 300;
    //public float missileReloadTimer = 3.0f;

    ////cluster missiles
    //public int clusterMissileDamage = 15;
    //public float clusterMissileReloadTimer = 3.0f;
    //public Transform target;

    ////torpedoes
    //public int torpedoDamage = 3000;
    //public float torpedoReloadTimer = 3.0f;

    ////mining laser
    //public float miningLaserRange = 1000f;
    //public int mineAmount = 10;
    //public float mineInterval = 1.0f;

    //economy
    private int credits;

    
    public int GetCredits()
    {
        return credits;
    }

	
    public bool CreateTransaction(int amount)
    {
        if(credits+amount>=0)
        {
            credits+=amount;
            return true;
        }
        else
            return false;
    }
    
    void OnGUI()
    {
        DisplayEquippedWeapons();
        DisplayMinorOptions();
        if (showMissionLog) DisplayMissions();

        if (displayInventory)
        {
            //primaryWindow = GUI.Window(0, primaryWindow, PrimaryWindow, "Primary Weapon");
            //secondaryWindow = GUI.Window(1, secondaryWindow, SecondaryWindow, "Secondary Weapon");
            //tertiaryWindow = GUI.Window(2, tertiaryWindow, TertiaryWindow, "Tertiary Weapon");
            //utilityWindow = GUI.Window(3, utilityWindow, UtilityWindow, "Utlity Weapon");
            equippedWindow = GUI.Window(0, equippedWindow, EquippedWindow, "Equipped Weapons");
            if(showAvailableWeapons)
            {
                availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Available Weapons");
            }
        }
        
    }

    void DisplayEquippedWeapons()
    {
        Rect equippedWeaponsRect = new Rect(Screen.width / 2 - 200, Screen.height - 110, 400, 100);
        GUI.Box(equippedWeaponsRect, "Weapons");
        GUI.BeginGroup(equippedWeaponsRect);

        for (int i = 0; i < 4; i++)
        {
            string wpnName;
            if (equippedWeapons[i])
                wpnName = equippedWeapons[i].name;
            else
                wpnName = "------";
            
            GUI.Button(new Rect(0 + (100 * i), 20, 100, 60), "Wpn " + (i + 1) + ":\n" + wpnName);
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
                buttonText = equippedWeapons[i].name;
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
            if (GUI.Button(new Rect(15, 20 + i * 20, 150, 20), availableWeapons[i].name)) 
            {
                if (equippedWeapons[hardpointSelected])
                {
                    availableWeapons.Add(equippedWeapons[hardpointSelected]);
                    
                }
                equippedWeapons[hardpointSelected] = availableWeapons[i];
                availableWeapons.Remove(availableWeapons[i]);
                
            }
        }
    }
    //void PrimaryWindow(int windowID)
    //{
    //    if (GUI.Button(new Rect(10, 20, 120, 20), "Lasers"))
    //    {
    //        print("Lasers");
    //        SetPrimaryToLaser();
    //    }
    //    if(GUI.Button(new Rect(10,50,120,20),"Railgun"))
    //    {
    //        print("Railgun");
    //        SetPrimaryToRail();
    //    }
    //}
    //void SecondaryWindow(int windowID)
    //{
    //    if (GUI.Button(new Rect(10, 20, 120, 20), "Missiles"))
    //    {
    //        print("Missiles");
    //        SetSecondaryToMissiles();
    //    }
    //    if (GUI.Button(new Rect(10, 50, 120, 20), "Cluster Missiles"))
    //    {
    //        print("Cluster Missiles");
    //        SetSecondaryToClusterMissiles();
    //    }
    //}
    //void TertiaryWindow(int windowID)
    //{
    //    if (GUI.Button(new Rect(10, 20, 120, 20), "Torpedo"))
    //    {
    //        print("Torpedo");
    //        SetTertiaryToTorpedo();
    //    }
      
    //}
    //void UtilityWindow(int windowID)
    //{
    //    if (GUI.Button(new Rect(10, 20, 120, 20), "Mining Laser"))
    //    {
    //        print("Mining Laser");
    //        SetUtilityToMiningLaser();
    //    }
        
    //}
    //void SetPrimaryToLaser()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_Lasers)
    //            primaryWeapon = weapon;
    //    }
    //}
    //void SetPrimaryToRail()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_Railgun)
    //            primaryWeapon = weapon;
    //    }
    //}
    //void SetSecondaryToMissiles()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_Missile)
    //            secondaryWeapon = weapon;
    //    }
    //}

    //void SetSecondaryToClusterMissiles()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_ClusterMissile)
    //            secondaryWeapon = weapon;
    //    }
    //}
    //    void SetTertiaryToTorpedo()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_Torpedo)
    //            tertiaryWeapon = weapon;
    //    }
    //}

    //    void SetUtilityToMiningLaser()
    //{
    //    foreach (Weapon weapon in availableWeapons)
    //    {
    //        if (weapon is Weapon_MiningLaser)
    //            utilityWeapon = weapon;
    //    }
    //}
    void Start()
    {
        credits = 0;

        equippedWeapons = new Weapon[numberOfHardpoints];

        equippedWindow = new Rect(10, Screen.height / 2 - 200, 175, numberOfHardpoints*30);
        availableWindow = new Rect(240, Screen.height / 2 - 200, 175, availableWeapons.Count*30);

        //if (primaryWeapon is Weapon_Lasers)
        //{
        //    primaryWeapon.damage = laserDamage;
        //    primaryWeapon.projectileSpeed = laserSpeed;
        //    primaryWeapon.reloadTimer = laserReloadTimer;
        //    primaryWeapon.range = laserRange;
        //}
        //else if (primaryWeapon is Weapon_Railgun)
        //{
        //    primaryWeapon.damage = railDamage;
        //    primaryWeapon.projectileSpeed = railSpeed;
        //    primaryWeapon.reloadTimer = railReloadTimer;
        //    primaryWeapon.range = railRange;
        //}

        //if (secondaryWeapon is Weapon_ClusterMissile)
        //{
        //    secondaryWeapon.damage = clusterMissileDamage;
        //    secondaryWeapon.target = this.target;
        //    secondaryWeapon.reloadTimer = clusterMissileReloadTimer;
        //}
        //else if (secondaryWeapon is Weapon_Missile)
        //{
        //    secondaryWeapon.damage = missileDamage;
        //    secondaryWeapon.target = this.target;
        //    secondaryWeapon.reloadTimer = clusterMissileReloadTimer;
        //}

        //if (tertiaryWeapon is Weapon_Torpedo)
        //{
        //    tertiaryWeapon.damage = torpedoDamage;
        //    tertiaryWeapon.reloadTimer = torpedoReloadTimer;
        //}

        //if (utilityWeapon is Weapon_MiningLaser)
        //{
        //    utilityWeapon.range = miningLaserRange;
        //    Weapon_MiningLaser miningLaser = (Weapon_MiningLaser)utilityWeapon;
        //    miningLaser.mineAmount = mineAmount;
        //    miningLaser.mineInterval = mineInterval;
        //}

        foreach (Weapon weapon in availableWeapons)
        {
            weapon.origin = gameObject;
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

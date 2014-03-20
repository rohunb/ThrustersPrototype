using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DockManager : MonoBehaviour {

    public EquipmentTerminal equipTerm;
    public MissionTerminal missionTerm;
    public VendorTerminal vendorTerm;
    
    PlayerInventory playerInv;
    CameraMove cameraMove;
    GameObject player;
    CharacterController playerCharController;
    CharacterMotor playerMotor;
    FPSInputController playerFPS;
    MouseLook playerMouseLook;

    public int numHardpoints=4;

    bool showEquipTerm=false;
    bool showVendorTerm = false;
    bool showMissionTerm = false;

    Rect equippedWindow;
    Rect availableWindow;

    bool attachingWeapon = false;
    public int hardpointsLayer = 9;
    int weaponSelected;
    public GameObject weaponOutlinePrefab;
    GameObject[] weaponOutlines;
    public GameObject weaponPrefab;
    GameObject weapon;
    GameObject[] weapons;

    void Awake()
    {
        playerInv = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerInventory>();
        player=GameObject.FindGameObjectWithTag("Player");
        cameraMove = player.GetComponent<CameraMove>();
        playerCharController = player.GetComponent<CharacterController>();
        playerMotor = player.GetComponent<CharacterMotor>();
        playerFPS = player.GetComponent<FPSInputController>();
        playerMouseLook = player.GetComponent<MouseLook>();
    }
	// Use this for initialization
    void Start()
    {
        equippedWindow = new Rect(10, Screen.height / 2 - 200, 175, numHardpoints * 30);
        availableWindow = new Rect(Screen.width - 200, Screen.height / 2 - 200, 175, playerInv.availableWeapons.Count * 30);
        weaponOutlines = new GameObject[playerInv.numberOfHardpoints];
        weapons = new GameObject[playerInv.numberOfHardpoints];
        //foreach (Transform hardpoint in playerInv.hardPoints)
        for (int i = 0; i < playerInv.numberOfHardpoints; i++)
        {
            weaponOutlines[i] = Instantiate(weaponOutlinePrefab, playerInv.hardPoints[i].position, playerInv.hardPoints[i].rotation) as GameObject;
            weaponOutlines[i].SetActive(false);
            weapons[i] = Instantiate(weaponPrefab, playerInv.hardPoints[i].position, playerInv.hardPoints[i].rotation) as GameObject;
            weapons[i].SetActive(false);
        }
        weapon = Instantiate(weaponPrefab) as GameObject;
        weapon.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitAllTerminals();
            cameraMove.CameraReturnToPos();
        }
        if(attachingWeapon)
        {
            bool placeWeapon = false;
            ShowHardPoints();
            if(Input.GetMouseButtonDown(0))
            {
                placeWeapon = true;
            }
            Ray ray = player.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,1<<hardpointsLayer))
            {
                weapon.SetActive(true);
                int hardpointSelected;
                switch(hit.collider.name)
                {
                    case "HardPoint1":
                        hardpointSelected = 0;
                        if (!placeWeapon)
                        {
                            weapon.transform.position = playerInv.hardPoints[hardpointSelected].position;
                            weapon.transform.rotation = playerInv.hardPoints[hardpointSelected].rotation;
                        }
                        else
                        {
                            playerInv.AddWeaponToHardpoint(hardpointSelected, weaponSelected);
                            weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                    case "HardPoint2":
                        hardpointSelected = 1;
                        if (!placeWeapon)
                        {
                            weapon.transform.position = playerInv.hardPoints[hardpointSelected].position;
                            weapon.transform.rotation = playerInv.hardPoints[hardpointSelected].rotation;
                        }
                        else
                        {
                            playerInv.AddWeaponToHardpoint(hardpointSelected, weaponSelected);
                            weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                    case "HardPoint3":
                        hardpointSelected = 2;
                        if (!placeWeapon)
                        {
                            weapon.transform.position = playerInv.hardPoints[hardpointSelected].position;
                            weapon.transform.rotation = playerInv.hardPoints[hardpointSelected].rotation;
                        }
                        else
                        {
                            playerInv.AddWeaponToHardpoint(hardpointSelected, weaponSelected);
                            weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                    case "HardPoint4":
                        hardpointSelected = 3;
                        if (!placeWeapon)
                        {
                            weapon.transform.position = playerInv.hardPoints[hardpointSelected].position;
                            weapon.transform.rotation = playerInv.hardPoints[hardpointSelected].rotation;
                        }
                        else
                        {
                            playerInv.AddWeaponToHardpoint(hardpointSelected, weaponSelected);
                            weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                }

                //weaponOutline.transform.position=
            }
            
        }
	}


    void OnGUI()
    {
        if(showEquipTerm)
        {
            equippedWindow = GUI.Window(0, equippedWindow, EquippedWindow, "Hardpoints");
            availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Cargo Hold");
            if(GUI.Button(new Rect(Screen.width/2-50,Screen.height-100,100,50),"Exit Terminal"))
            {
                ExitAllTerminals();
                cameraMove.CameraReturnToPos();
            }
        }
    }

    void EquippedWindow(int windowID)
    {
        string buttonText;
        for (int i = 0; i < playerInv.equippedWeapons.Length; i++)
        {
            GUI.Label(new Rect(5, 20 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (playerInv.equippedWeapons[i])
                buttonText = playerInv.equippedWeapons[i].name;
            else
                buttonText = "------";
            if(GUI.Button(new Rect(15, 20+i*20,150,20),buttonText))
            {
                
            }
        }
    }
    void AvailableWindow(int windowID)
    {
        for (int i = 0; i < playerInv.availableWeapons.Count; i++)
        {
            GUI.Label(new Rect(5, 20 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (GUI.Button(new Rect(15, 20 + i * 20, 150, 20), playerInv.availableWeapons[i].name))
            {
                attachingWeapon = true;
                weaponSelected = i;
            }
        }
    }
    
    void ShowHardPoints()
    {
        foreach (GameObject weaponOutline in weaponOutlines)
        {
            weaponOutline.SetActive(true);
        }
    }
    void DontShowHardPoints()
    {
        foreach (GameObject weaponOutline in weaponOutlines)
        {
            weaponOutline.SetActive(false);
        }
    }
    public void ExitAllTerminals()
    {
        showEquipTerm = false;
        showMissionTerm = false;
        showVendorTerm = false;
        
    }
    public void ShowEquipTerminal()
    {
        showEquipTerm = true;
        PlayerCanMove(false);
        cameraMove.CameraLookAtShip(player.transform);
    }
    public void ShowMissionTerminal()
    {
        showMissionTerm = true;
    }
    public void ShowVendorTerminal()
    {
        showVendorTerm = true;
    }
    public void PlayerCanMove(bool fix)
    {
        playerCharController.enabled = fix;
        playerMotor.enabled = fix;
        playerFPS.enabled = fix;
        playerMouseLook.enabled = fix;
        
    }


}

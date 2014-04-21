using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DockManager : MonoBehaviour
{

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

    public int numHardpoints = 4;

    public bool showEquipTerm = false;
    public bool showVendorTerm = false;
    public bool showMissionTerm = false;
    bool showPopup = false;
	bool showMissionPopup = false;

    Rect equippedWindow;
    Rect availableWindow;
    Rect vendorWindow;
	Rect missionWindow;
    Rect vendorScrollRect;
    Rect popupRect;
    Vector2 scrollPosition = Vector2.zero;
    string popUpText;

    bool attachingWeapon = false;
    public int hardpointsLayer = 9;
    int weaponSelected;
    public GameObject weaponOutlinePrefab;
    GameObject[] weaponOutlines;
    public GameObject weaponPrefab;
    GameObject weapon;
    //GameObject[] weapons;
    public List<GameObject> vendorWeapons;
    int vendorWpnSelected;
	string selectedMission;

    PersistentInventory godInventory;
    public Vector2 buttonSize= new Vector2(100,30);

    public GUISkin guiSkin;

    void Awake()
    {
        godInventory = GameObject.Find("GOD").GetComponent<PersistentInventory>();
        playerInv = GameObject.FindWithTag("PlayerShip").GetComponent<PlayerInventory>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraMove = player.GetComponent<CameraMove>();
        playerCharController = player.GetComponent<CharacterController>();
        playerMotor = player.GetComponent<CharacterMotor>();
        playerFPS = player.GetComponent<FPSInputController>();
        playerMouseLook = player.GetComponent<MouseLook>();
    }
    // Use this for initialization
    void Start()
    {

        Screen.showCursor = true;
        equippedWindow = new Rect(10, Screen.height / 2 - 200, 175, (numHardpoints+1) * (buttonSize.y+10));
        vendorWindow = new Rect(50, Screen.height / 2 - 200, 200, /*vendorWeapons.Count * 30*/ 400);
        vendorScrollRect = new Rect(2, 20, 200, /*vendorWeapons.Count * 30*/ 400);
		//missionWindow = new Rect(50, Screen.height / 2 - 200, 200, 250);
        missionWindow = new Rect(50, Screen.height / 2 - 200, 200, 50 + GOD.godMission.MissionName.Length * (buttonSize.y+50));
        //availableWindow = new Rect(Screen.width - 200, Screen.height / 2 - 200, 175, 10+playerInv.availableWeapons.Count * 20);
        popupRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 225, 100);
        weaponOutlines = new GameObject[playerInv.numberOfHardpoints];
        //weapons = new GameObject[playerInv.numberOfHardpoints];
        //foreach (Transform hardpoint in playerInv.hardPoints)
        for (int i = 0; i < playerInv.numberOfHardpoints; i++)
        {
            weaponOutlines[i] = Instantiate(weaponOutlinePrefab, playerInv.hardPoints[i].position, playerInv.hardPoints[i].rotation) as GameObject;
            weaponOutlines[i].SetActive(false);
            //weapons[i] = Instantiate(weaponPrefab, playerInv.hardPoints[i].position, playerInv.hardPoints[i].rotation) as GameObject;
            //weapons[i].SetActive(false);
        }
        weapon = Instantiate(weaponPrefab) as GameObject;
        weapon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        availableWindow = new Rect(Screen.width - 200, Screen.height / 2 - 200, 175, 60 + playerInv.availableWeapons.Count * (buttonSize.y+2));

        if ((showEquipTerm) &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            ExitAllTerminals();
            cameraMove.CameraReturnToPos();
        }
        if ((showVendorTerm || showMissionTerm)
            && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitAllTerminals();
            PlayerCanMove(true);
        }
        if (attachingWeapon)
        {

            bool placeWeapon = false;
            ShowHardPoints();
            if (Input.GetMouseButtonDown(0))
            {
                placeWeapon = true;
            }
            Ray ray = player.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << hardpointsLayer))
            {
                
                weapon.SetActive(true);
                int hardpointSelected;
                switch (hit.collider.name)
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
                            weaponOutlines[hardpointSelected].SetActive(false);
                            //weapons[hardpointSelected].SetActive(true);
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
                            weaponOutlines[hardpointSelected].SetActive(false);
                            //weapons[hardpointSelected].SetActive(true);
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
                            weaponOutlines[hardpointSelected].SetActive(false);
                           // weapons[hardpointSelected].SetActive(true);
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
                            weaponOutlines[hardpointSelected].SetActive(false);
                            //weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                }
                ShowHardPoints();
            }

        }
    }


    void OnGUI()
    {
        GUI.skin = guiSkin;
        if (showEquipTerm)
        {
            equippedWindow = GUI.Window(0, equippedWindow, EquippedWindow, "Hardpoints");
            availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Cargo Hold");
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Exit Terminal"))
            {
                GOD.audioengine.playSFX("TerminalExit");
                ExitAllTerminals();
                cameraMove.CameraReturnToPos();
            }
        }

        if (showVendorTerm)
        {
            vendorWindow = GUI.Window(2, vendorWindow, VendorWindow, "Vendor");
            availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Cargo Hold");
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Exit Terminal"))
            {
                GOD.audioengine.playSFX("TerminalExit");
                ExitAllTerminals();
                PlayerCanMove(true);
            }
        }
		if (showMissionTerm)
		{
			missionWindow = GUI.Window(2, missionWindow, MissionWindow, "Missions");
			//availableWindow = GUI.Window(1, availableWindow, AvailableWindow, "Cargo Hold");
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 50), "Exit Terminal"))
			{
				GOD.audioengine.playSFX("TerminalExit");
				ExitAllTerminals();
				PlayerCanMove(true);
			}
		}
        if (showPopup)
        {
            //GUI.Box(popupRect, popUpText);
            popupRect = GUI.Window(3, popupRect, PopupWindow, "");
        }
		if (showMissionPopup)
		{
			//GUI.Box(popupRect, popUpText);
			popupRect = GUI.Window(3, popupRect, MissionPopupWindow, "");
		}
    }

    void EquippedWindow(int windowID)
    {
        string buttonText;
        
        for (int i = 0; i < playerInv.equippedWeapons.Length; i++)
        {
            GUI.Label(new Rect(5, 30 + i * buttonSize.y, 120, buttonSize.y), "" + (i + 1) + ": ");
            if (playerInv.equippedWeapons[i])
                buttonText = playerInv.equippedWeapons[i].wpnName;
            else
                buttonText = "------";
            if (GUI.Button(new Rect(15, 20 + i * buttonSize.y, 150, buttonSize.y), buttonText))
            {

            }
        }
        if (GUI.Button(new Rect(15,  30 + 4 * buttonSize.y, 150, buttonSize.y), "Clear All Hardpoints"))
        {
            //Debug.Log("clear all");
            //for (int i = 0; i < playerInv.equippedWeapons.Length; i++)
            //{
            //    if (playerInv.equippedWeapons[i])
            //    {
            //        playerInv.UnequipWeapon(i);
            //    }
            //}
            playerInv.UnequipWeapons();
            for (int i = 0; i < weaponOutlines.Length; i++)
            {
                //weapons[i].SetActive(false);
                weaponOutlines[i].SetActive(true);
            }
        }

    }
    void VendorWindow(int windowID)
    {
        scrollPosition = GUI.BeginScrollView(vendorScrollRect, scrollPosition, new Rect(0, 0, 200, vendorWeapons.Count * (buttonSize.y + 7)));
        for (int i = 0; i < vendorWeapons.Count; i++)
        {
            GUI.Label(new Rect(1, 10 + i * buttonSize.y, 120, buttonSize.y), "" + (i + 1) + ": ");
            Weapon vendorWpn = vendorWeapons[i].GetComponent<Weapon>();
            if (GUI.Button(new Rect(17, 0 + i * buttonSize.y, 150, buttonSize.y), vendorWpn.wpnName))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                vendorWpnSelected = i;
                showPopup = true;
                popUpText = "Buy Weapon: " +
                            vendorWpn.wpnName + " for " + vendorWpn.cost + "?";

            }

        }
        GUI.EndScrollView();
    }
    void AvailableWindow(int windowID)
    {
        GUI.Label(new Rect(5, 20, 120, buttonSize.y), "Credits: " + playerInv.GetCredits());
        for (int i = 0; i < playerInv.availableWeapons.Count; i++)
        {
            GUI.Label(new Rect(5, 53 + i * buttonSize.y, 120, buttonSize.y), "" + (i + 1) + ": ");
            if (GUI.Button(new Rect(15, 43 + i * buttonSize.y, 150, buttonSize.y), playerInv.availableWeapons[i].wpnName))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                attachingWeapon = true;
                weaponSelected = i;
            }
        }
    }
	void MissionWindow(int windowID)
	{
        GUI.Label(new Rect(5, 20, 250, buttonSize.y), "Current Mission: ");
        GUI.Label(new Rect(5, 40, 190, buttonSize.y), GOD.godMission.currentMission);
        GUI.Label(new Rect(5, 70, 120, buttonSize.y + 50), "Optional Missions: ");

		for (int i = 0; i < GOD.godMission.MissionName.Length; i++) {
            //Debug.Log(GOD.godMission.MissionName[i].ToString());
            if (GUI.Button(new Rect(5, 120 + i * (buttonSize.y + 15), 190, buttonSize.y), GOD.godMission.MissionName[i].ToString()))
            {
				GOD.audioengine.playSFX("TerminalBtn");
				selectedMission = GOD.godMission.MissionName[i].ToString();
				showMissionPopup = true;
				popUpText = "<color=red>One mission at a time!</color> \n Confirm Mission: \n" + GOD.godMission.MissionName[i].ToString();
			}
		}
	}
    void PopupWindow(int windowID)
    {
        GUI.Label(new Rect(10, 5, popupRect.width, 120), popUpText);
        if (GUI.Button(new Rect(5, popupRect.height - 40, popupRect.width / 2 - 10, 40), "Yes"))
        {
            if (playerInv.CreateTransaction(-vendorWeapons[vendorWpnSelected].GetComponent<Weapon>().cost))
            {
                playerInv.AddWeaponToCargo(Instantiate(vendorWeapons[vendorWpnSelected]) as GameObject);
                vendorWeapons.RemoveAt(vendorWpnSelected);
            }
            GOD.audioengine.playSFX("TerminalBtnYes");
            ResetPopup();
        }
        if (GUI.Button(new Rect(popupRect.width - popupRect.width / 2 + 10, popupRect.height - 40, popupRect.width / 2 - 10, 40), "No"))
        {
            GOD.audioengine.playSFX("TerminalBtn");
            ResetPopup();
        }
    }
	void MissionPopupWindow(int windowID)
	{
		GUI.Label(new Rect(10, 0, popupRect.width, 120), popUpText);
		if (GUI.Button(new Rect(5, popupRect.height - 40, popupRect.width / 2 - 10, 40), "Yes"))
		{
			GOD.audioengine.playSFX("TerminalBtnYes");
			GOD.godMission.setMissionType(selectedMission);
			ResetPopup();
			//Debug.Log("Set current mission to: " + selectedMission);
            GOD.currentMission = selectedMission;

            Galaxy myGalaxy = GameObject.Find("Galaxy").GetComponent<Galaxy>();
            GOD.currentMissionLocation = myGalaxy.GetRandomSystem();


		}
		if (GUI.Button(new Rect(popupRect.width - popupRect.width / 2 + 10, popupRect.height - 40, popupRect.width / 2 - 10, 40), "No"))
		{
			GOD.audioengine.playSFX("TerminalBtn");
			selectedMission = null;
			ResetPopup();
		}
	}
    void ShowHardPoints()
    {
        //foreach (GameObject weaponOutline in weaponOutlines)
        for (int i = 0; i < weaponOutlines.Length; i++)
        {
            if(!playerInv.equippedWeapons[i])
                weaponOutlines[i].SetActive(true);

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
        showPopup = false;
		showMissionPopup = false;
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
		PlayerCanMove(false);
    }
    public void ShowVendorTerminal()
    {
        showVendorTerm = true;
        PlayerCanMove(false);
    }
    public void PlayerCanMove(bool fix)
    {
        playerCharController.enabled = fix;
        playerMotor.enabled = fix;
        playerFPS.enabled = fix;
        playerMouseLook.enabled = fix;

    }
    void ResetPopup()
    {
		showMissionPopup = false;
        showPopup = false;
        popUpText = "";
    }

}

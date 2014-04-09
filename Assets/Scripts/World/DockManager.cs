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

    bool showEquipTerm = false;
    bool showVendorTerm = false;
    bool showMissionTerm = false;
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
    GameObject[] weapons;
    public List<GameObject> vendorWeapons;
    int vendorWpnSelected;
	string selectedMission;

    PersistentInventory godInventory;

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
        equippedWindow = new Rect(10, Screen.height / 2 - 200, 175, numHardpoints * 30);
        vendorWindow = new Rect(50, Screen.height / 2 - 200, 200, /*vendorWeapons.Count * 30*/ 400);
        vendorScrollRect = new Rect(2, 20, 200, /*vendorWeapons.Count * 30*/ 400);
		missionWindow = new Rect(50, Screen.height / 2 - 200, 200, 200);
        //availableWindow = new Rect(Screen.width - 200, Screen.height / 2 - 200, 175, 10+playerInv.availableWeapons.Count * 20);
        popupRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100);
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
    void Update()
    {
        availableWindow = new Rect(Screen.width - 200, Screen.height / 2 - 200, 175, 60+playerInv.availableWeapons.Count * 20);

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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
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
                            GOD.audioengine.playSFX("TerminalBtnYes");
                            playerInv.EquipWeaponFromHold(hardpointSelected, weaponSelected);
                            weapons[hardpointSelected].SetActive(true);
                        }
                        break;
                }

            }

        }
    }


    void OnGUI()
    {
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
            GUI.Label(new Rect(5, 20 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (playerInv.equippedWeapons[i])
                buttonText = playerInv.equippedWeapons[i].wpnName;
            else
                buttonText = "------";
            if (GUI.Button(new Rect(15, 20 + i * 20, 150, 20), buttonText))
            {

            }
        }
    }
    void VendorWindow(int windowID)
    {
        scrollPosition = GUI.BeginScrollView(vendorScrollRect, scrollPosition, new Rect(0, 0, 200, vendorWeapons.Count * 20));
        for (int i = 0; i < vendorWeapons.Count; i++)
        {
            GUI.Label(new Rect(1, 0 + i * 20, 120, 20), "" + (i + 1) + ": ");
            Weapon vendorWpn = vendorWeapons[i].GetComponent<Weapon>();
            if (GUI.Button(new Rect(17, 0 + i * 20, 150, 20), vendorWpn.wpnName))
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
        GUI.Label(new Rect(5, 20, 120, 20), "Credits: " + playerInv.GetCredits());
        for (int i = 0; i < playerInv.availableWeapons.Count; i++)
        {
            GUI.Label(new Rect(5, 43 + i * 20, 120, 20), "" + (i + 1) + ": ");
            if (GUI.Button(new Rect(15, 43 + i * 20, 150, 20), playerInv.availableWeapons[i].wpnName))
            {
                GOD.audioengine.playSFX("TerminalBtn");
                attachingWeapon = true;
                weaponSelected = i;
            }
        }
    }
	void MissionWindow(int windowID)
	{
		GUI.Label(new Rect(5, 20, 250, 20), "Current Mission: ");
		GUI.Label(new Rect(5, 40, 190, 20), GOD.godMission.currentMission);
		GUI.Label(new Rect(5, 60, 120, 20), "Optional Missions: ");

		for (int i = 0; i < GOD.godMission.MissionName.Length; i++) {
			if(GUI.Button(new Rect(5, 80 + i * 20, 190, 20), GOD.godMission.MissionName[i].ToString())) {
				GOD.audioengine.playSFX("TerminalBtn");
				selectedMission = GOD.godMission.MissionName[i].ToString();
				showMissionPopup = true;
				popUpText = "<color=red>One mission at a time!</color> \n Confirm Mission: \n" + GOD.godMission.MissionName[i].ToString();
			}
		}
	}
    void PopupWindow(int windowID)
    {
        GUI.Label(new Rect(10, 15, popupRect.width, 120), popUpText);
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
		GUI.Label(new Rect(10, 15, popupRect.width, 120), popUpText);
		if (GUI.Button(new Rect(5, popupRect.height - 40, popupRect.width / 2 - 10, 40), "Yes"))
		{
			GOD.audioengine.playSFX("TerminalBtnYes");
			GOD.godMission.setMissionType(selectedMission);
			ResetPopup();
			Debug.Log("Set current mission to: " + selectedMission);
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

using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

	bool displayVendorPrompt = false;
    PlayerInventory playerInv;

	void Awake()
    {
        playerInv = GameObject.Find("sexyShip").GetComponent<PlayerInventory>();
    }
	
	
	// Update is called once per frame
	void Update () {
		if (displayVendorPrompt && Input.GetKeyDown(KeyCode.F))
		{
            playerInv.SaveInventory();
			ScreenFader.EndScene();
			GOD.cameraPos = GameObject.Find ("BackgroundCamera").transform.position;
			GOD.cameraRot = GameObject.Find ("BackgroundCamera").transform.rotation;
			Application.LoadLevel("DockedScene");
		}
	}
	
	void OnGUI()
	{
		if (displayVendorPrompt)
		{
			DisplayVendorPrompt();
		}
	}
	void DisplayVendorPrompt()
	{
		GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 150, 500, 100), "<size=28>Press F to Dock with Station</size>");
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PlayerShip")
		{
			displayVendorPrompt = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "PlayerShip")
		{
			displayVendorPrompt = false;
		}
	}
}

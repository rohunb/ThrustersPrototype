using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

	bool displayVendorPrompt = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (displayVendorPrompt && Input.GetKeyDown(KeyCode.F))
		{
			print("Entering Dock Scene");
			ScreenFader.EndScene();
			Application.LoadLevel(2);
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
		GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 150, 500, 100), "<size=28>Press F to Go to Dock</size>");
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

using UnityEngine;
using System.Collections;

public class EquipmentTerminal : MonoBehaviour {
    
    bool displayEquipPrompt = false;
    DockManager dockManager;
    
    void Awake()
    {
        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
    }
    void Update () {

        if (displayEquipPrompt && Input.GetKeyDown(KeyCode.F))
        {
            GOD.audioengine.playSFX("TerminalEnter");
            dockManager.ShowEquipTerminal();
        }
    
    }
    void OnGUI()
    {
        if(displayEquipPrompt)
        {
            DisplayEquipPrompt();
        }
    }

    void DisplayEquipPrompt()
    {
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 150, 500, 100), "<size=28>Press F to open Equipment Terminal</size>");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            displayEquipPrompt = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            displayEquipPrompt = false;
        }
        dockManager.ExitAllTerminals();
    }

}

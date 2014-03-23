﻿using UnityEngine;
using System.Collections;

public class VendorTerminal : MonoBehaviour {

    bool displayVendorPrompt = false;
    DockManager dockManager;

    void Awake()
    {
        dockManager = GameObject.FindGameObjectWithTag("DockManager").GetComponent<DockManager>();
    }

    void Update()
    {

        if (displayVendorPrompt && Input.GetKeyDown(KeyCode.E))
        {
            dockManager.ShowVendorTerminal();
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
        GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height - 150, 500, 100), "<size=28>Press E to open Vendor Terminal</size>");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            displayVendorPrompt = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            displayVendorPrompt = false;
        }
    }
}
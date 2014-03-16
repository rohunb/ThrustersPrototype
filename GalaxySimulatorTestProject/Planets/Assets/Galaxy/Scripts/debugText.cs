using UnityEngine;
using System.Collections;

public class debugText : MonoBehaviour {

    TextMesh textMesh;

    Galaxy sourceScript;
    PhysicsSim sim;

	void Start () 
    {
        textMesh = GetComponent<TextMesh>();
        sourceScript = GameObject.Find("GalaxyCore").GetComponent<Galaxy>(); 
	}
	
	void Update () 
    {
        textMesh.text = sourceScript.debugText;
	}
}

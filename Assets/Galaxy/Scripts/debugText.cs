using UnityEngine;
using System.Collections;

public class debugText : MonoBehaviour {

    TextMesh textMesh;
    Galaxy sourceScript;

	void Start () 
    {
        textMesh = GetComponent<TextMesh>();
        sourceScript = GameObject.Find("Galaxy").GetComponent<Galaxy>(); 
	}
	
	void Update () 
    {
        textMesh.text = sourceScript.debugText;
	}
}

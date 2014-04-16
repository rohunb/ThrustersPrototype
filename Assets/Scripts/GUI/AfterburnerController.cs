using UnityEngine;
using System.Collections;

public class AfterburnerController : MonoBehaviour
{
    float cutoff = 0, maxAF;
	bool playAlert = false;

    ShipMove shipMove;

    // Use this for initialization
    void Awake()
    {
        shipMove = GameObject.Find("sexyShip").GetComponent<ShipMove>();
    }

    void Start()
    {
        renderer.material.SetFloat("_Cutoff", cutoff);
        maxAF = shipMove.maxAfterburnerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        float currAF = shipMove.currentAfterburnerLevel;

		if(currAF < 30.0f && currAF > 15.0f && !playAlert) {
			GOD.audioengine.playSFX("Warning");
			playAlert = true;
		} else if(currAF < 15.0f && currAF > 5.0f && playAlert) {
			GOD.audioengine.playSFX("Warning");
			playAlert = false;
		} else if(currAF < 5.0f) {
			GOD.audioengine.playSFX("Warning");
			playAlert = false;
		} else if(currAF > 30.0f) {
			playAlert = false;
		}

        cutoff = 1f - (currAF / maxAF);
		//Debug.Log(currAF);
        //print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
}

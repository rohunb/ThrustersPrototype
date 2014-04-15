using UnityEngine;
using System.Collections;

public class AfterburnerController : MonoBehaviour
{
    float cutoff = 0, maxAF;

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

        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1, "time", .5, "delay", 2));
    }

    // Update is called once per frame
    void Update()
    {
        float currAF = shipMove.currentAfterburnerLevel;
        cutoff = 1f - (currAF / maxAF);
        print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
}

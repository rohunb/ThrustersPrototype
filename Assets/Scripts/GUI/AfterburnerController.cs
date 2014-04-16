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

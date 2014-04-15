using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
    float cutoff = 0, maxShield;

    Health shipHp;

    // Use this for initialization
    void Awake()
    {
        shipHp = GameObject.Find("sexyShip").GetComponent<Health>();
    }

    void Start()
    {
        renderer.material.SetFloat("_Cutoff", cutoff);
        maxShield = shipHp.maxShield;

        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1, "time", .5, "delay", 2));
    }

    // Update is called once per frame
    void Update()
    {
        float currSh = shipHp.shieldStrength;
        cutoff = 1f - (currSh / maxShield);
        print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
}

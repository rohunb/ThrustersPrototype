using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
    float cutoff = 0, maxHp;

    Health shipHp;

    // Use this for initialization
    void Awake()
    {
        shipHp = GameObject.Find("sexyShip").GetComponent<Health>();
    }

    void Start()
    {
        renderer.material.SetFloat("_Cutoff", cutoff);
        maxHp = shipHp.health;

        iTween.FadeTo(gameObject, iTween.Hash("alpha", 1, "time", .5, "delay", 2));
    }

    // Update is called once per frame
    void Update()
    {
        float currHp = shipHp.health;
        cutoff = 1f - (currHp / maxHp);
        print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
}

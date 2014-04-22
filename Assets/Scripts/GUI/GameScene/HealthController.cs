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
    }

    // Update is called once per frame
    void Update()
    {
        float currHp = shipHp.health;
        cutoff = 1f - (currHp / maxHp);
        //print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 60f, Screen.height - Screen.height / 4.3f, Screen.width / 2.84f, Screen.height / 10.8f), "<size=14>Hull</size>");
    }
}

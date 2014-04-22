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
        if (Screen.width > 1500)
        {
            transform.position = new Vector3(-7.94f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(-5.75f, transform.position.y, transform.position.z);
        }
        renderer.material.SetFloat("_Cutoff", cutoff);
        maxShield = shipHp.maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        float currSh = shipHp.shieldStrength;
        cutoff = 1f - (currSh / maxShield);
        //print(cutoff);

        renderer.material.SetFloat("_Cutoff", cutoff);
    }
}

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
        Debug.Log(Screen.width);
        if(Screen.width >1500)
        {
            transform.position = new Vector3(-8.42f, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(-6.2f, transform.position.y, transform.position.z);
        }
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
}

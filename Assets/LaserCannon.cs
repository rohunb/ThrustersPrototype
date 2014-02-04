using UnityEngine;
using System.Collections;

public class LaserCannon : MonoBehaviour {
    public GameObject laser;
    public Transform shootPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Fire(float damage)
    {
        Instantiate(laser, shootPoint.position, shootPoint.rotation);
    }
}

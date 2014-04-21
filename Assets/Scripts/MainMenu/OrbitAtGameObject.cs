using UnityEngine;
using System.Collections;

public class OrbitAtGameObject : MonoBehaviour {
    public GameObject targetObject;
    public float orbitSpd;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        ShakeCamera();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.LookAt(targetObject.transform);
        gameObject.transform.RotateAround(Vector3.zero, Vector3.up, orbitSpd * Time.deltaTime);
	}

    void ShakeCamera()
    {
        iTween.ShakePosition(gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, .2f), "delay", 4, "onComplete", "ShakeCamera", "islocal", true, "onStart", "AddExplosions"));
    }

    void AddExplosions()
    {
        Instantiate(explosion, new Vector3(0, 7.75f, 0), Quaternion.identity);
    }
}

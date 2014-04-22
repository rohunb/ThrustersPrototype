using UnityEngine;
using System.Collections;

public class GameOverCamera : MonoBehaviour
{
    public GameObject GOtargetObject;
    public float GOorbitSpd;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        ShakeCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.LookAt(GOtargetObject.transform);
        gameObject.transform.RotateAround(Vector3.zero, Vector3.up, GOorbitSpd * Time.deltaTime);
    }

    void ShakeCamera()
    {
        iTween.ShakePosition(gameObject, iTween.Hash("amount", new Vector3(.2f, .2f, .2f), "delay", 2, "onComplete", "ShakeCamera", "islocal", true, "onStart", "AddExplosions"));
    }

    void AddExplosions()
    {
		GOD.audioengine.playSFX("gameOverShipExplosion");
        Instantiate(explosion, new Vector3(0, 7.75f, 0), Quaternion.identity);
    }
}

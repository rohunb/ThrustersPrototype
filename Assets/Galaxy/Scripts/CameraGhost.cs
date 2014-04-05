using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour 
{
    public GameObject targetPlanet;
    private float rangeFromPlanet = 200;
	// Use this for initialization
	void Start () 
    {
		if (!GOD.goToRandomPointInGalaxy) 
		{
			Invoke("waitForaBit", 2.0f);
				}

        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GOD.goToRandomPointInGalaxy)
        {
            int randIndex = UnityEngine.Random.Range(1, GameObject.FindGameObjectsWithTag("Moon").Length - 1);
            targetPlanet = GameObject.FindGameObjectsWithTag("Moon")[randIndex];

            GameObject.Find("BackgroundCamera").transform.position = targetPlanet.transform.position - new Vector3(rangeFromPlanet, 0, 0);
            GameObject.Find("BackgroundCamera").transform.LookAt(targetPlanet.transform.position);
			GOD.goToRandomPointInGalaxy = false;
        }
	}

    void waitForaBit()
    {
		GOD.goToRandomPointInGalaxy = true;

    }
}

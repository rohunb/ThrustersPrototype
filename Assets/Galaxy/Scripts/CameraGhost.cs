using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour 
{
    bool firstTime = true;
    public GameObject targetPlanet;
    private float rangeFromPlanet = 200;
	// Use this for initialization
	void Start () 
    {
		if (firstTime)
		{
			StartCoroutine(waitForaBit());
		}
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (firstTime)
        {
            int randIndex = UnityEngine.Random.Range(1, GameObject.FindGameObjectsWithTag("Planet").Length - 1);
            targetPlanet = GameObject.FindGameObjectsWithTag("Planet")[randIndex];

            GameObject.Find("BackgroundCamera").transform.position = targetPlanet.transform.position - new Vector3(rangeFromPlanet, 0, 0);
            GameObject.Find("BackgroundCamera").transform.LookAt(targetPlanet.transform.position);
            firstTime = false;
        }
	}

    IEnumerator waitForaBit()
    {
        yield return new WaitForSeconds(2.0f);
        firstTime = true;

    }
}

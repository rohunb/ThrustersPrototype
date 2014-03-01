using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    public int maxCredits = 1000;
    public int currentCredits;

	// Use this for initialization
	void Start () {
        currentCredits = maxCredits;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GetMined(int mineAmount)
    {
        currentCredits -= mineAmount;
        if (currentCredits <= 0)
            Destroy(gameObject);
    }
}

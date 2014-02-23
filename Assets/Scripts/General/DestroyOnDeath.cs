using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour {
    public GameObject explosion;
    private Health parentHealth;
	// Use this for initialization
	void Start () {
        parentHealth = gameObject.GetComponent<Health>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(parentHealth.health<=0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
	}
}

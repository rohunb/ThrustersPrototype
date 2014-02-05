using UnityEngine;
using System.Collections;

public class ClusterMissile : MonoBehaviour {
    public GameObject missile;
    public GameObject explosion;
    public int numberOfMissiles;
    public float missileSpread;
    public float clusterOpenTimer;
    public Transform target;
    
	// Use this for initialization
	void Start () {
        Invoke("ClusterOpen", clusterOpenTimer);
	}
	
    void ClusterOpen()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Vector3 spawnPos;
        for (int i = 0; i < numberOfMissiles; i++)
        {
            spawnPos = transform.position+Random.onUnitSphere*missileSpread;
            GameObject missileClone=Instantiate(missile, transform.position, Quaternion.identity) as GameObject;
            Vector3 vel = (spawnPos - transform.position)*missileSpread*2;
            missileClone.rigidbody.velocity = new Vector3(vel.x,vel.y,vel.z);
            missileClone.transform.LookAt(target);
            missileClone.GetComponent<HomingMissile>().target = this.target;


        }
        Destroy(gameObject);
    }
}

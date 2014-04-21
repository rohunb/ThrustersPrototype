using UnityEngine;
using System.Collections;

public class ClusterMissile : MonoBehaviour {
    public GameObject missile;
    public GameObject explosion;
    public int numberOfMissiles;
    public float missileSpread;
    public float clusterOpenTimer;
    public Transform target;
    public GameObject origin;
    public int missileDamage;
    
	// Use this for initialization
	void Start () {
        Invoke("ClusterOpen", clusterOpenTimer);
	}
	
    void ClusterOpen()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        //play cluster sound
		GOD.audioengine.playSFX("clusterExplosion");
        Vector3 spawnPos;
        for (int i = 0; i < numberOfMissiles; i++)
        {
            spawnPos = transform.position+Random.onUnitSphere*missileSpread;
            GameObject missileClone=Instantiate(missile, transform.position, transform.rotation) as GameObject;
            //Vector3 vel = (spawnPos - transform.position)*missileSpread*20;
            missileClone.rigidbody.AddForce((spawnPos - transform.position)*40f,ForceMode.Impulse);
            //missileClone.rigidbody.velocity = new Vector3(vel.x,vel.y,vel.z);
            missileClone.transform.LookAt(target);
            missileClone.GetComponent<HomingMissile>().target = this.target;
            ProjectileDamager damager = missileClone.GetComponent<ProjectileDamager>();
            damager.origin = this.origin;
            damager.damage = missileDamage;


        }
        Destroy(gameObject);
    }
}

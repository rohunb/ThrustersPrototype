using UnityEngine;
using System.Collections;

public class ProjectileDamager : MonoBehaviour {
    public int damage;
	// Use this for initialization
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyShip")
        {
            //Instantiate(explosion, transform.position, Quaternion.identity);
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

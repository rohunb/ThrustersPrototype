using UnityEngine;
using System.Collections;

public class ProjectileDamager : MonoBehaviour
{
    public int damage;
    public GameObject origin;
    // Use this for initialization
    public void OnTriggerEnter(Collider other)
    {
        if (origin)
        {
            switch (origin.tag)
            {
                case "PlayerShip":

                    if (other.tag == "EnemyShip" || other.tag == "EnemyStructure")
                    {
                        //Instantiate(explosion, transform.position, Quaternion.identity);
                        other.GetComponent<Health>().TakeDamage(damage);
                        Destroy(gameObject);
                    }
                    break;
                case "EnemyShip":
                    if (other.tag == "PlayerShip" || other.tag=="Victim" )
                    {
                        //Instantiate(explosion, transform.position, Quaternion.identity);
                        other.GetComponent<Health>().TakeDamage(damage);
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}

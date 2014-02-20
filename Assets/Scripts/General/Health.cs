using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int health = 1000;
	// Use this for initialization
	public void TakeDamage(int damage)
    {
        health -= damage;
    }

}

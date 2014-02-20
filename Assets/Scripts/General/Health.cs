using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int health = 1000;
    public int maxShield = 1000;
    public int shieldStrength;
    public float shieldRechargeTimer = 4;
    public int shieldRechargeStrength = 1;
    public bool shieldRecharging = false;
    public bool shielded = true;
    public bool alive = true;
    public GameObject shield;
    private float currentTimer=0f;
	// Use this for initialization
    void Start()
    {
        shieldStrength = maxShield;
    }
    void Update()
    {
        if(currentTimer>=shieldRechargeTimer)
        {
            shieldRecharging = true;
        }
        if (shieldStrength > 0)
            shielded = true;
        if (shielded)
            shield.renderer.enabled = true;
        else
            shield.renderer.enabled = false;
        //recharge shield
        if (shieldRecharging && shieldStrength<maxShield)
            shieldStrength += shieldRechargeStrength;
        //prevent shield from going over max
        if (shieldStrength > maxShield)
            shieldStrength = maxShield;
        currentTimer += Time.deltaTime;
    }
	public void TakeDamage(int damage)
    {
        if (shielded)
            shieldStrength -= damage;
        else
            health -= damage;
        if(shieldStrength<=0)
        {
            shieldStrength = 0;
            shielded = false;
        }
        if (health <= 0)
            alive = false;
        shieldRecharging = false;
        currentTimer = 0f;
    }
    
}

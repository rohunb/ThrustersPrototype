using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int health = 1000;
    public int maxShield = 1000;
    public int shieldStrength;
    public float shieldRechargeTimer = 4f;
    public int shieldRechargeStrength = 1;
    public float shieldFlickerTimer = 0.5f;
    public bool shieldRecharging = false;
    public bool shielded = true;
    public bool alive = true;
    public GameObject shield;
    private float shieldRechargeCurrentTimer=0f;
    private bool shieldFlickering = false;
    private float shieldFlickerCurrentTimer = 0f;
    private Color shieldColor;
	// Use this for initialization
    void Start()
    {
        shieldStrength = maxShield;
    }
    void Update()
    {
        if(shieldRechargeCurrentTimer>=shieldRechargeTimer)
            shieldRecharging = true;

        if (shieldFlickerCurrentTimer >= shieldFlickerTimer)
            shieldFlickering = false;

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

        if(shieldFlickering)
        {
            shieldColor = shield.renderer.material.color;
            shieldColor.a = Mathf.Abs(Mathf.Cos(shieldFlickerCurrentTimer*25));
            shield.renderer.material.color = shieldColor;
        }
        shieldRechargeCurrentTimer += Time.deltaTime;
        shieldFlickerCurrentTimer += Time.deltaTime;
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
        shieldRechargeCurrentTimer = 0f;
        shieldFlickering = true;
        shieldFlickerCurrentTimer = 0f;
    }

 
    
}

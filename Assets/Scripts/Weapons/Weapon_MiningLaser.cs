using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class Weapon_MiningLaser : Weapon {
    public int mineAmount;
    public float mineInterval;
    public float lineWidth;
    public float lineNoise;
    public Color beamColor = Color.red;
    public ParticleSystem laserImpactEffect;
    public bool firing;

    LineRenderer line;
    float currentTimer = 0f;
    PlayerInventory playerInventory;
    int length;

	// Use this for initialization
	void Awake () {
        weaponType = WeaponType.Utility;
        line = GetComponent<LineRenderer>();
        line.SetWidth(lineWidth, lineWidth);
        line.SetColors(beamColor, beamColor);
        laserImpactEffect.Stop();
        playerInventory = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<PlayerInventory>();
        length = Mathf.RoundToInt(range);
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        if (firing)
        {
            CheckForCollision();
            CreateBeamEffect();
			GOD.audioengine.playSFX("MiningLaser");
        }
        else
        {
            line.enabled = false;
            laserImpactEffect.Stop();
        }
        currentTimer += Time.deltaTime;
        base.Update();
	}
    public override void Fire()
    {
        firing = true;
    }
    void CreateBeamEffect()
    {
        line.enabled = true;
        line.SetVertexCount(length);
        for (int i = 0; i < length; i++)
        {
            Vector3 newPos = shootPoint.position;
            Vector3 offset = Vector3.zero;
            offset.x = newPos.x + i * shootPoint.forward.x + Random.Range(-lineNoise, lineNoise);
            offset.y = newPos.y + i * shootPoint.forward.y + Random.Range(-lineNoise, lineNoise);
            offset.z = newPos.z + i * shootPoint.forward.z + Random.Range(-lineNoise, lineNoise);
            newPos = offset;
            line.SetPosition(i, newPos);
        }
    }
    void CheckForCollision()
    {
        RaycastHit hit;
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        if (Physics.Raycast(ray, out hit, range))
        {
            length = Mathf.RoundToInt(hit.distance);
            if (currentTimer >= mineInterval && hit.transform.tag == "Asteroid")
            {
                hit.transform.gameObject.GetComponent<Asteroid>().GetMined(mineAmount);
                playerInventory.CreateTransaction(mineAmount);
                currentTimer = 0f;
            }
            laserImpactEffect.transform.position = hit.point;
            laserImpactEffect.Play();
        }
        else
        {
            length = Mathf.RoundToInt(range);
            laserImpactEffect.Stop();
        }
    }
    public void StopFiring()
    {
        firing = false;
    }
}

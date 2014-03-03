using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class Weapon_Railgun : Weapon {
    LineRenderer line;
    public Color lineStartColour = Color.white;
    Color lineColour;
    bool clearEffect;
    public float railEffectClearTimer=0.1f;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        clearEffect = false;
    }
    public override void Fire()
    {
        if(canFire)
        {
            StartCoroutine("FireRail");
            canFire = false;
        }
    }
    IEnumerator FireRail()
    {
        GameObject railshotClone = Instantiate(projectile, shootPoint.position, shootPoint.rotation) as GameObject;
        
        ProjectileMover mover = railshotClone.GetComponent<ProjectileMover>();
        mover.speed = projectileSpeed;
        mover.range = range;

        ProjectileDamager damager = railshotClone.GetComponent<ProjectileDamager>();
        damager.origin = origin;
        damager.damage = damage;
        CreateRailEffect(Mathf.RoundToInt(range));

        yield return new WaitForSeconds(reloadTimer);
        canFire = true;
    }
    void CreateRailEffect(int length)
    {
        line.enabled = true;
        line.SetVertexCount(length);
        line.SetColors(lineStartColour, lineStartColour);
        for (int i = 0; i < length; i++)
        {
            Vector3 newPos = shootPoint.position;

            float offsetX = 1.5f * Mathf.Cos(i + Time.time);
            float offsetY = 1.5f * Mathf.Sin(i + Time.time);
            newPos += shootPoint.right * offsetX;
            newPos += shootPoint.up * offsetY;
            newPos += shootPoint.forward * i;

            line.SetPosition(i, newPos);


        }
        lineColour = lineStartColour;
        clearEffect = true;
    }
    void Update()
    {
        if (clearEffect)
        {
            lineColour = Color.Lerp(lineColour, new Color(lineColour.r, lineColour.g, lineColour.b, 0.0f), Time.deltaTime * railEffectClearTimer);
            if (lineColour.a < 0.1f)
            {
                line.enabled = false;
            }
            line.SetColors(lineColour, lineColour);
        }

    }

}

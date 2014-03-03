using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class Railgun : MonoBehaviour
{

    public GameObject rail;
    public Transform shootPoint;

    public float railEffectRotSpeed;
    public float railEffectClearTimer;
    LineRenderer line;
    public Color lineStartColour = Color.white;
    Color lineColour;
    bool clearEffect;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        clearEffect = false;
    }
    public void Fire(int _damage, float _speed, float _range,GameObject _origin)
    {
        GameObject railshotClone = Instantiate(rail, shootPoint.position, shootPoint.rotation) as GameObject;
        railshotClone.GetComponent<ProjectileMover>().speed = _speed;
        ProjectileDamager damager = railshotClone.GetComponent<ProjectileDamager>();
        damager.origin = _origin;
        damager.damage = _damage;
        CreateRailEffect(Mathf.RoundToInt(_range));
    }
    void CreateRailEffect(int length)
    {
        line.enabled = true;
        line.SetVertexCount(length);
        line.SetColors(lineStartColour, lineStartColour);
        for (int i = 0; i < length; i++)
        {
            //Vector3 newPos = shootPoint.position;
            //newPos.x += 1.5f* Mathf.Cos(i+Time.time);
            //newPos.y +=  1.5f*Mathf.Sin(i+Time.time);
            //newPos += shootPoint.forward * i;
            //line.SetPosition(i, newPos);

            Vector3 newPos = shootPoint.position;
            //Vector3 offset = Vector3.zero;
            //offset.x = newPos.x + i * shootPoint.forward.x /*+ shootPoint.right.x*/ + 2f * Mathf.Cos(i + Time.time);
            //offset.y = newPos.y + i * shootPoint.forward.y + /*shootPoint.right.y +*/  2f*Mathf.Sin(i + Time.time);
            //offset.z = newPos.z+i*shootPoint.forward.z;
            //newPos.x += 2f * Mathf.Cos(i + Time.time);
            //newPos.y += 2f * Mathf.Sin(i + Time.time);
            //newPos += shootPoint.forward * i;
            //newPos = offset;

            float offsetX = 2f * Mathf.Cos(i + Time.time);
            float offsetY = 2f * Mathf.Sin(i + Time.time);
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
        if(clearEffect)
        {
            lineColour=Color.Lerp(lineColour, new Color(lineColour.r, lineColour.g, lineColour.b, 0.0f), Time.deltaTime * railEffectClearTimer);
            if (lineColour.a < 0.1f)
            {
                line.enabled = false;
            }
            line.SetColors(lineColour,lineColour);
        }
        
    }
    void CancelRailEffect()
    {
        line.enabled = false;
    }
}
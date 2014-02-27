using UnityEngine;
using System.Collections;

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
            Vector3 newPos=transform.position;
            //newPos.x+=i * 0.5F;
            newPos.x += 2f*Mathf.Cos(i + Time.time);
            newPos.y+= 2f*Mathf.Sin(i + Time.time);
            //newPos.z += i;
            newPos += transform.forward * i;
            line.SetPosition(i, newPos);
        }
        //Invoke("CancelRailEffect", .2f);
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
                //lineColour.a = 1.0f;
                line.enabled = false;
            }
            //line.renderer.material.color = lineColour;
            line.SetColors(lineColour,lineColour);
        }
        
    }
    void CancelRailEffect()
    {
        line.enabled = false;
    }
}
//public class Example : MonoBehaviour
//{
//    public Color c1 = Color.yellow;
//    public Color c2 = Color.red;
//    public int lengthOfLineRenderer = 20;
//    void Start()
//    {
//        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
//        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
//        lineRenderer.SetColors(c1, c2);
//        lineRenderer.SetWidth(0.2F, 0.2F);
//        lineRenderer.SetVertexCount(lengthOfLineRenderer);
//    }
//    void Update()
//    {
//        LineRenderer lineRenderer = GetComponent<LineRenderer>();
//        int i = 0;
//        while (i < lengthOfLineRenderer)
//        {
//            Vector3 pos = new Vector3(i * 0.5F, Mathf.Sin(i + Time.time), 0);
//            lineRenderer.SetPosition(i, pos);
//            i++;
//        }
//    }
//}
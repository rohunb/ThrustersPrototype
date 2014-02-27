using UnityEngine;
using System.Collections;

public class SpiralMove : MonoBehaviour
{
    public float rotSpeed;
    public float moveSpeed;
    public Transform midpoint;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = transform.localPosition;
        //float x = newPos.x * Mathf.Cos(rotSpeed) + newPos.y * Mathf.Sin(rotSpeed);
        //float y = -newPos.x * Mathf.Sin(rotSpeed) + newPos.y * Mathf.Cos(rotSpeed);
        //float x = midpoint.position.x + Mathf.Cos(rotSpeed * Time.time);
        //float y = midpoint.position.y + Mathf.Sin(rotSpeed * Time.time);
        //newPos.x += x;
        //newPos.y += y;
        ////newPos.z += moveSpeed;
        //Vector3 newPos = new Vector3(Mathf.Cos(rotSpeed * Time.time), Mathf.Sin(rotSpeed * Time.time), transform.position.z + moveSpeed);

        //transform.localPosition = newPos;
        transform.RotateAround(midpoint.position, transform.forward, rotSpeed);
        
    }
}

//using UnityEngine;
//using System.Collections;

//public class SpiralMove : MonoBehaviour
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
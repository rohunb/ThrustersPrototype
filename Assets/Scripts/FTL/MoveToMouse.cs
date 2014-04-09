using UnityEngine;
using System.Collections;

public class MoveToMouse : MonoBehaviour {
    Vector3 target;
    Vector3 velocity = Vector3.zero;
    public float speed = 5f;
    public int galaxyMapLayer = 12;
	// Use this for initialization
	void Start () {
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, 1 << galaxyMapLayer))
            {
                target = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(target);
            }
        }

        MoveToCursor();
	}

    void MoveToCursor()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, speed * Time.deltaTime * 2f);
    }
}

using UnityEngine;
using System.Collections;

public class MoveToMouse : MonoBehaviour {
    Vector3 target;
    Vector3 velocity = Vector3.zero;
    public float speed = 5f;
    public int galaxyMapLayer = 12;
    public bool canMove = true;
	// Use this for initialization
	void Start () {
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        Rect buttonRect = new Rect(Screen.width / 2 - 100, Screen.height - 60, 200, 40);
        canMove = !(buttonRect.Contains(Input.mousePosition));

        if (canMove)
        {
            if (Input.GetMouseButton(1))
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
        else
        {
            Debug.Log("cannot move");
        }
	}

    void MoveToCursor()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, speed * Time.deltaTime * 2f);
    }
}

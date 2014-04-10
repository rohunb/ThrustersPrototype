using UnityEngine;
using System.Collections;

public class FTL_CameraFollow : MonoBehaviour 
{
	private float lerpSpeed = 4f;
	private Transform ftlSpaceship;
	private Camera ftlCamera;
	private float targetCameraSize;
	private float camZoomAmount = 10f;
    
    public int galaxyLayer = 12;
    public Vector4 bounds;
    Rect boundaries;


	// Use this for initialization
	void Start () 
	{
		ftlSpaceship = GameObject.Find ("Spaceship").transform;
		ftlCamera = Camera.main;
		targetCameraSize = ftlCamera.orthographicSize;
        boundaries = new Rect();
        boundaries.xMin = bounds.x;
        boundaries.xMax = bounds.y;
        boundaries.yMin = bounds.z;
        boundaries.yMax = bounds.w;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (CheckIfBeyondEdge())
        {
            Vector3 targetCamPosition = new Vector3(ftlSpaceship.position.x, transform.position.y, ftlSpaceship.position.z);

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetCamPosition.x, Time.deltaTime * lerpSpeed), transform.position.y, Mathf.Lerp(transform.position.z, targetCamPosition.z, Time.deltaTime * lerpSpeed));
        }
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //transform.Translate(new Vector3(h * 30 * Time.deltaTime, v * 30 * Time.deltaTime, 0.0f));

		if (Input.GetAxis ("Mouse ScrollWheel") > 0.05f)
		{
			targetCameraSize -= camZoomAmount;
			if (targetCameraSize < 10)
			{
				targetCameraSize = 10;
			}
		}
		else if (Input.GetAxis ("Mouse ScrollWheel") < -0.05f)
		{
			targetCameraSize += camZoomAmount;
			if (targetCameraSize > 50)
			{
				targetCameraSize = 50;
			}

		}
		ftlCamera.orthographicSize = Mathf.Lerp(ftlCamera.orthographicSize, targetCameraSize, Time.deltaTime * lerpSpeed);

        CheckIfBeyondEdge();
	}
    bool CheckIfBeyondEdge()
    {
        bool canMove = true;
        
        Ray topLeft = Camera.main.ViewportPointToRay(new Vector3(0, 1, 0));
        Ray topRight = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height - 1, 0));
        Ray botLeft = Camera.main.ViewportPointToRay(new Vector3(0, 0, 0));

        float left, right, top, bottom;
        RaycastHit hit;

        if (Physics.Raycast(topLeft, out hit, Mathf.Infinity, 1 << galaxyLayer))
        {
            left = hit.point.x;
            top = hit.point.z;
        }
        else
        {
            transform.Translate(new Vector3(1.0f, 0f, 0f));
            return false;
        }

        if(Physics.Raycast(topRight, out hit, Mathf.Infinity, 1 << galaxyLayer))
        {right = hit.point.x;
        }
        else
        {
            transform.Translate(new Vector3(-1.0f, 0f, 0f));
            return false;
        }

        Physics.Raycast(botLeft, out hit, Mathf.Infinity, 1 << galaxyLayer);
        bottom = hit.point.z;

        //Debug.Log("left right top, botton: " + new Vector4(left, right, top, bottom));

        if (left < boundaries.xMin)
        {
            transform.Translate(new Vector3(boundaries.xMin - left, 0f, 0f));
            canMove = false;
            //Debug.Log("Left over");
        }
        else if (right > boundaries.xMax)
        {
            transform.Translate(new Vector3(boundaries.xMax - right, 0, 0), Space.World);
            canMove = false;
            //Debug.Log("right over");
        }

        if (bottom < boundaries.yMin)
        {
            transform.Translate(new Vector3(0, 0, boundaries.yMin - bottom), Space.World);
            canMove = false;
            //Debug.Log("bottom over");
        }
        else if (top > boundaries.yMax)
        {
            transform.Translate(new Vector3(0, 0, boundaries.yMax - top), Space.World);
            canMove = false;
            // Debug.Log("Top over");
        }
        return canMove;
    }

}

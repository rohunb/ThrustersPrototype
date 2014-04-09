using UnityEngine;
using System.Collections;

public class FTL_CameraFollow : MonoBehaviour 
{
	private float lerpSpeed = 4f;
	private Transform ftlSpaceship;
	private Camera ftlCamera;
	private float targetCameraSize;
	private float camZoomAmount = 10f;
	// Use this for initialization
	void Start () 
	{
		ftlSpaceship = GameObject.Find ("Spaceship").transform;
		ftlCamera = Camera.main;
		targetCameraSize = ftlCamera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetCamPosition = new Vector3(ftlSpaceship.position.x, transform.position.y, ftlSpaceship.position.z);

		transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetCamPosition.x, Time.deltaTime * lerpSpeed),transform.position.y,Mathf.Lerp(transform.position.z, targetCamPosition.z, Time.deltaTime * lerpSpeed)); 


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
	}
}

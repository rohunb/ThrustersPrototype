using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {
    float xMin;
    float yMin;
    public Texture2D crosshairTex;
    public Transform weapon;

	void OnGUI()
    {
        
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairTex.width / 10, crosshairTex.height / 10), crosshairTex);
    }
    void Update()
    {
        //xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshairTex.width / 2 / 10);
        //yMin = (Screen.height - Input.mousePosition.y) - (crosshairTex.height / 2 / 10);
        Vector3 screenHitPos = Vector3.zero;
        Ray ray = new Ray(weapon.position, weapon.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 1000f,Color.red);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity,1<<10))
        {
            screenHitPos = weapon.InverseTransformPoint(hit.point);
            //Debug.Log(screenHitPos);
        }
		if (Camera.main)
		{
			//Debug.Log ("holyshit!!! I HAZ camera!!");
			Ray topLeft = Camera.main.ViewportPointToRay(new Vector3(0, 1, 0));
			Ray topRight = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height - 1, 0));
			Ray botLeft = Camera.main.ViewportPointToRay(new Vector3(0, 0, 0));

			Debug.DrawRay(topLeft.origin,topLeft.direction*10000f,Color.red);
			Debug.DrawRay(topRight.origin,topRight.direction*10000f,Color.blue);
			Debug.DrawRay(botLeft.origin,botLeft.direction*10000f,Color.green);
			float left, right, top, bottom;
			
			Physics.Raycast(topLeft, out hit, Mathf.Infinity, 1 << 10);
            //left = hit.point.x;
            //top = hit.point.y;
            left = weapon.InverseTransformPoint(hit.point).x;
            top = weapon.InverseTransformPoint(hit.point).y;

			
			
			Physics.Raycast(topRight, out hit, Mathf.Infinity, 1 << 10);
			//right = hit.point.x;
            right = weapon.InverseTransformPoint(hit.point).x;
			
			Physics.Raycast(botLeft, out hit, Mathf.Infinity, 1 << 10);
			bottom = hit.point.y;
            bottom = weapon.InverseTransformPoint(hit.point).y;
			
			float screenWidth = right - left;
			float screenHeight = top -bottom;

			//Debug.Log("left right top, bottom: " + new Vector4(left, right, top, bottom));
			
			xMin = (screenWidth/2+screenHitPos.x) / screenWidth * Screen.width;
			yMin = (screenHeight/2-screenHitPos.y) / screenHeight * Screen.height;
			//xMin = Screen.width / 2;
			//yMin = Screen.height / 2;
			Debug.Log("xMin, yMin: "+new Vector2(xMin, yMin));
		}
		
		
	}
}

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    //public float smoothTime = 0.3F;
    

    private Vector2 Velocity;
    void Update()
    {
        //float newPositionX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref Velocity.x, smoothTime);
        //float newPositionY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref Velocity.y, smoothTime);

        transform.position = new Vector3(target.position.x + 6f, transform.position.y, transform.position.z);
    }
}

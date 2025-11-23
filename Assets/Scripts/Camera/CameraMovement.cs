using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public  float CamSpeed;
    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
  Vector3 cam = new Vector3(target.position.x,target.position.y,transform.position.z);
   transform.position = Vector3.SmoothDamp(transform.position,cam, ref velocity,CamSpeed);
    }
    
}

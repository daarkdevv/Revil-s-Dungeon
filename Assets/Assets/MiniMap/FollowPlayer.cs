using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    Vector3 ZcameraFix;

    Vector3 Follow;
    // Start is called before the first frame update
    void Start()
    {
      
       transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x,target.position.y,transform.position.z),ref ZcameraFix,0); 
    }
}

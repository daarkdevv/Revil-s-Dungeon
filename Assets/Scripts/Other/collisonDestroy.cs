using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other) {

       
        if(other.gameObject.CompareTag("Player") && other.gameObject.transform.position.x == gameObject.transform.position.x)
        {
            Destroy(gameObject);
        }
       
       
    }
}

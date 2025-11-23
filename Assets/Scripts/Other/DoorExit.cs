using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    public GameObject DoorClosed;
    // Start is called before the first frame update

    private void OnCollisionExit2D(Collision2D other) {

        Instantiate(DoorClosed,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}

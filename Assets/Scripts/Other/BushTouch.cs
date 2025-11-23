using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushTouch : MonoBehaviour
{
    Animator anim;
    public GameObject Leaf;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Touch");
            Instantiate(Leaf,transform.position,Quaternion.identity);
        }
    }
}

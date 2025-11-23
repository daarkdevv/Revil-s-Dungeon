using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class ShattersScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 randomDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        randomDirection = new Vector2(Random.Range(-2,2),Random.Range(-2,2));
        rb.AddForce(randomDirection * 100);
     
        var bounds = GetComponent<Collider2D>().bounds;

        AstarPath.active.UpdateGraphs(bounds);

    

    Destroy(gameObject,0.4f);

        Destroy(gameObject,0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

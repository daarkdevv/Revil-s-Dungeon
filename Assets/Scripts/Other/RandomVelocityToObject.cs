using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVelocityToObject : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector2 randomDirectionValue;

    [SerializeField]
    float forcePower;

    public bool hasFinished = false;
    // Start is called before the first frame update
    void Start()
    {

       randomDirectionValue = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));

       rb.AddForce(randomDirectionValue * forcePower, ForceMode2D.Impulse);

       StartCoroutine("StopMove");

    }


    IEnumerator StopMove()
    {
      yield return new WaitForSeconds(0.4f);
      hasFinished = true;
      
    }


     private void FixedUpdate() {
      
     if(hasFinished && rb.velocity != Vector2.zero)
     {
       rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
     }
      
    }

    
}

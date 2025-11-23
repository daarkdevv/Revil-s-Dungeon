using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AvoidEnemy : MonoBehaviour
{
    public float avoidDistance = 0.5f;
    GameObject[] otherenemy;
    public float AvoidSpeed;


    void Start()
    {
        
        otherenemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void FixedUpdate()
    {


       foreach(GameObject go in otherenemy)
       {
         if(go != gameObject && go != null )
         {
           float direction1 = Vector2.Distance(go.transform.position , this.transform.position);
           
           if(direction1 <= avoidDistance )
           {

            Vector2 direction = transform.position - go.transform.position;

            
               transform.Translate(direction * AvoidSpeed * Time.deltaTime);

            
           }

           
        
         }

       }
    
    }


}

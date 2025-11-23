using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> slot;

    float ClosestDis = Mathf.Infinity;

    float ClosestChest = Mathf.Infinity;

    Collider2D possibleCollective,PossibleChest;
    
    [SerializeField]
    float sphereRange;

    [SerializeField]

    Collider2D[] eachPickup,eachChest;

    public LayerMask PickAble,chest;

    private void Update() {

      
         if(Input.GetKeyDown(KeyCode.E))
         {
           CollectInRange();
         }
         
         if(Input.GetKeyDown(KeyCode.G))
         {
            ChestInRange();
         }

    }

    void CollectInRange()
    {

     eachPickup = Physics2D.OverlapCircleAll(transform.position,sphereRange,PickAble); 

     if(eachPickup.Length > 0)
     {

      foreach(Collider2D Pickup in eachPickup)
      {

        if(Vector2.Distance(Pickup.transform.position,gameObject.transform.position) < ClosestDis)
        {

          ClosestDis = Vector2.Distance(Pickup.transform.position,gameObject.transform.position);

          possibleCollective = Pickup;

        }


      }

       possibleCollective.gameObject.GetComponent<DistanceView>().Pickup(possibleCollective.GetComponent<DistanceView>().ItemTag);

       possibleCollective = null;

       ClosestDis = Mathf.Infinity;

     } 
      

    }

    void ChestInRange()
    {
      eachChest = Physics2D.OverlapCircleAll(transform.position,sphereRange,chest);

      if(eachChest.Length > 0 && ItemCounter.Instance.KeyCount >= 1)
      {
        
        foreach(Collider2D chest in eachChest)
        {

          if(Vector2.Distance(chest.transform.position,gameObject.transform.position) < ClosestDis)
          {

          ClosestChest = Vector2.Distance(chest.transform.position,gameObject.transform.position);

          PossibleChest = chest;

          }

          
        }



          if(PossibleChest.gameObject.GetComponent<OpenChest>().hasOpened == false)
          {

            gameObject.GetComponent<Animator>().SetTrigger("KeyUse");

            PossibleChest.gameObject.GetComponent<OpenChest>().useKey();

          }

           

           ClosestChest = Mathf.Infinity;
 


      } 
    }


    private void OnDrawGizmosSelected() {

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position ,sphereRange);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
   Vector2 _ref;
   float smoothTime = 0.3f;
   
   bool CanCallFunc = false;
   Transform Player;

   private void Start() {
      Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
   }
   private void OnTriggerEnter2D(Collider2D other) {

     if(other.tag == "Player")
     {
       if(!CanCallFunc)
       {

         CanCallFunc = true;

       }

     }

   }

   private void FixedUpdate() {
       
      if(CanCallFunc)
      {
         
         MoveToPlayer();
         
      }


   }




   void MoveToPlayer()
   {

      transform.parent.LeanMove(Player.position,2f).setEaseOutQuint();

   }

}

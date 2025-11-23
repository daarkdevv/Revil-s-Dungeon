using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public Rigidbody2D rb;
   public float movementspeed;
   Vector2 move;
   public Animator anim;
   public float DashTimer;
   public GameObject cameraShake;
   public int dashForce , dashDamage;
   public bool isDashing;
   Collider2D[] enemysWhileDash;
   public Vector2 boxSize;
   public int dashStaminaCost;
   private PlayerTakeDamage ptake;
   public GameObject DashGhostRenderer;
   public float timerDashGhost , timerbtwDash , resetTimerBtwDash;
   public SliderShaking sliderShaking;

    // Start is called before the first frame update
    void Start()
    {
        movementspeed = ItemCounter.Instance.MaxSpeed;
        move.Normalize();
        ptake = GetComponent<PlayerTakeDamage>();
    }

    void Update()
    {
          //Dash 
          
          
         if(timerDashGhost <= 0 && isDashing && rb.velocity.magnitude > 0)
         {
         
             
             
             DashGhostRenderer.GetComponent<Transform>().localScale = gameObject.transform.localScale;

             DashGhostRenderer.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

           
            timerDashGhost = 0.15f;
       
            Instantiate(DashGhostRenderer, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y,0),Quaternion.identity);


            if(rb.velocity.y > 0 || rb.velocity.y < 0)
            {
               if( rb.velocity.x == 0)
               {
                Instantiate(DashGhostRenderer, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y,0),Quaternion.identity);
               }
               
            }


         }
         else
         {
            timerDashGhost -= Time.deltaTime;
         }

     if(Input.GetKeyDown(KeyCode.F) && !isDashing && timerbtwDash <= 0 && rb.velocity.magnitude != 0 && ptake.InvincTime <= 0 && ItemCounter.Instance.stamina >= dashStaminaCost && gameObject.GetComponent<PlayerAttack>().attacktimer <= 0)
     {
       Decrease(dashStaminaCost);   

       timerbtwDash = resetTimerBtwDash;
      
       isDashing = true;

       if(isDashing)
       {
         StartCoroutine("DashCourotine");

         StartCoroutine(cameraShake.GetComponent<CameraShake>().startDashShaking());

         rb.velocity = move.normalized * ItemCounter.Instance.MaxSpeed * dashForce * Time.fixedDeltaTime;

       }
       
     }

     timerbtwDash -= Time.deltaTime;

      if(isDashing)
      {

        if(rb.velocity.y == 0 || rb.velocity.x != 0 && rb.velocity.y != 0)
        {

         if(gameObject.transform.localScale == Vector3.one )
         {
            transform.eulerAngles = new Vector3(0,0,-5);
         }

         else if(gameObject.transform.localScale == new Vector3(-1,1,1))
         {
            transform.eulerAngles = new Vector3(0,0,5);
         }

        } 
        

         Physics2D.IgnoreLayerCollision(3,7,true); //enemy
         Physics2D.IgnoreLayerCollision(3,11,true);
         Physics2D.IgnoreLayerCollision(3,9,true); //distract

         enemysWhileDash = Physics2D.OverlapBoxAll(transform.position,boxSize,0);

         foreach(Collider2D enemys in enemysWhileDash)
         {
            if(enemys.gameObject != null && enemys.gameObject.tag == "Enemy")
            {
                 
              if(enemys.gameObject.GetComponent<EnemyTakeDamage>().DashDamageCoolDownTimer <= 0 )
              {
                enemys.gameObject.GetComponent<EnemyTakeDamage>().DashDamageCoolDownTimer = 0.5f;
                
                if(enemys.gameObject.layer == 7) //enemys
                {
                    
                     enemys.gameObject.GetComponent<EnemyTakeDamage>().TakeDamage(dashDamage,ItemCounter.Instance.accuracyStat + 100);

                }

              }
         
 
            }

            if( enemys.gameObject != null ) //Distract
            {

             if(enemys.gameObject.tag == "Box" || enemys.gameObject.tag == "Barrel") 
             enemys.gameObject.GetComponent<DamageToDistractableObject>().TakeDamageDistraction(dashDamage * 5);

            }
         }

      }

      else
      {
         transform.eulerAngles = Vector3.zero;
         Physics2D.IgnoreLayerCollision(3,7,false);
         Physics2D.IgnoreLayerCollision(3,9,false);
         Physics2D.IgnoreLayerCollision(3,11,false);
      }


 
    }

    // Update is called once per frame
   private void FixedUpdate() {
   
     if(isDashing)
     return;

     move.x = Input.GetAxisRaw("Horizontal");

     move.y = Input.GetAxisRaw("Vertical");

     rb.velocity = (move.normalized * movementspeed * Time.fixedDeltaTime);

     if(move.magnitude > 0.01)
     {
        anim.SetBool("isRun",true);

         
        
     }
     else{
        anim.SetBool("isRun",false);
     }
     if(move.x < 0 && gameObject.GetComponent<PlayerAttack>().attacktimer <= 0)
     {
        transform.localScale = new Vector3(-1,1,1);
     }
     else if (move.x > 0 && gameObject.GetComponent<PlayerAttack>().attacktimer <= 0){
        transform.localScale = Vector3.one;
     }

 
    }
   IEnumerator DashCourotine()
   {
      yield return new WaitForSeconds(DashTimer);
      isDashing = false;
      ItemCounter.Instance.canGenerate = false;
      yield return new WaitForSeconds(2.3f);
      ItemCounter.Instance.canGenerate = true;
   }


   public void Decrease(int dashCost)
   {
      sliderShaking.TriggerShake();
      ItemCounter.Instance.stamina -= dashStaminaCost;
   }

     private void OnDrawGizmosSelected() {
      
      Gizmos.color = Color.red;
      
      Gizmos.DrawWireCube(transform.position,boxSize);

     }


   }

 

   

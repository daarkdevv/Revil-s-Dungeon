using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackpos;
    public float attackRange;
    public float force;
    public LayerMask enemy;
    public Collider2D[] hit;
    public float attacktimer;
    public float resetattacktimer = 0.4f;
    public float attackindexTimer , attackdownIndexTimer;
    public int attackindex = 0;
     int attackupindex = 0 , attackdownindex = 0;
    public Animator anim;
    public float attackupindexTimer;
    public GameObject cameraC;

    // Update is called once per frame
    void Update()
    {
        attackdownIndexTimer -= Time.deltaTime;

        attackupindexTimer -= Time.deltaTime;

        attacktimer -= Time.deltaTime;

        attackindexTimer -= Time.deltaTime;

 if(gameObject.GetComponent<PlayerMovement>().isDashing == false)
 {


  if(Input.GetKeyDown(KeyCode.Z) && attacktimer <= 0 && gameObject.GetComponent<PlayerTakeDamage>().InvincTime <= 0.8f)
      {
            
              Attack();
             attackindex += 1;
            
      }

       else if(Input.GetKeyDown(KeyCode.X) && attacktimer <= 0 && gameObject.GetComponent<PlayerTakeDamage>().InvincTime <= 0.8f)
        {
          AttackUp();
          attackupindex += 1;
    
        }

         else if(Input.GetKeyDown(KeyCode.C) && attacktimer <= 0 && gameObject.GetComponent<PlayerTakeDamage>().InvincTime <= 0.8f)
        {
         attackDown();
         attackdownindex++;
        }

 }
      
        if(attackindex > 2 )
        {
          attackindex = 0;
        }
        
         if( attackindexTimer <= 0)
        {
          attackindex = 0;
        }
       
        if(attackupindex > 2)
        {
          attackupindex = 0;
        }
      
        if(attackupindexTimer <= 0 )
        {
         attackupindex = 0;
        }
        
        if(attackdownindex > 2)
        {
          attackdownindex = 0;
        }

        if(attackdownIndexTimer <= 0)
        {
          attackdownindex = 0;
        }

      if(attacktimer <= 0)
      {
          gameObject.GetComponent<PlayerMovement>().movementspeed = ItemCounter.Instance.MaxSpeed;
      }
      else
      {
        gameObject.GetComponent<PlayerMovement>().movementspeed = 20;
      }

    }
     void Attack()
  {
      attacktimer = resetattacktimer;

      if(attackindex == 0 )  
      {
         anim.SetTrigger("Attack");
         attackindexTimer = 0.6f;
      }

      else if(attackindex == 1)
      {
       anim.SetTrigger("Attack2");
       attackindexTimer = 0.6f;
      }
      
      else if(attackindex == 2)
      {
        anim.SetTrigger("Attack3");
        attackindexTimer = 0.6f;
      }
      

    attackindexTimer = 1.3f;

    LayerMask Hitable = LayerMask.GetMask ("Enemy","Distarctable");

    hit = Physics2D.OverlapCircleAll(attackpos.position,ItemCounter.Instance.MeeleRange,Hitable);
    
    CircleDamage();

  }

void AttackUp()
{
       attacktimer = resetattacktimer;
       
        if (attackupindex == 0 )  
      {
         anim.SetTrigger("Attackup1");
         attackupindexTimer = 0.6f;
      }
      else if(attackupindex == 1)
      {
       anim.SetTrigger("Attackup2");
       attackupindexTimer = 0.6f;
      }
      else if(attackupindex == 2)
      {
        anim.SetTrigger("Attackup3");
        attackupindexTimer = 0.6f;
      }
      

     attackupindexTimer = 1.3f;

    LayerMask Hitable = LayerMask.GetMask ("Enemy","Distarctable");

    hit = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y + 0.7f),ItemCounter.Instance.MeeleRange,Hitable);

    CircleDamage();
}


void attackDown()
{

      attacktimer = resetattacktimer;
       
      if(attackdownindex == 0 )  
      {
         anim.SetTrigger("Attackdw1");
         attackdownIndexTimer = 0.6f;
      }
      else if(attackdownindex == 1)
      {
       anim.SetTrigger("Attackdw2");
       attackdownIndexTimer = 0.6f;
      }
      else if(attackdownindex == 2)
      {
        anim.SetTrigger("Attackdw3");
        attackdownIndexTimer = 0.6f;
      }
      

  attackdownIndexTimer = 1.3f;

  LayerMask Hitable = LayerMask.GetMask ("Enemy","Distarctable");

  hit = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y - 0.7f),ItemCounter.Instance.MeeleRange,Hitable);
  
  CircleDamage();

  } 

  void CircleDamage()
  {


     foreach(Collider2D enemy in hit)
    {
      if(enemy.gameObject.layer == 7)
      {

        StartCoroutine(cameraC.GetComponent<CameraShake>().StartcameraShaking());

        enemy.GetComponent<EnemyTakeDamage>().TakeDamage(Random.Range(ItemCounter.Instance.weaponDamage,ItemCounter.Instance.MaxDamage + ItemCounter.Instance.weaponDamage + 1) ,ItemCounter.Instance.accuracyStat);

      }

       
       
       if(enemy.gameObject.layer == 9)
       {

        StartCoroutine(cameraC.GetComponent<CameraShake>().StartcameraShaking());


        enemy.gameObject.GetComponent<DamageToDistractableObject>().TakeDamageDistraction(ItemCounter.Instance.MaxDamage);

 
       }

    }

  }

}

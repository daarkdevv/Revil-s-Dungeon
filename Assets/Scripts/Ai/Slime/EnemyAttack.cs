using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public float attackRadius;
    public LayerMask playerLayer;
    private LayerMask hitableLayer;
    public AIPath pathfinder;
    public Enemy enemy;
    public bool isCollided;
    private float attackCooldown;
    private int minimumDamage;
    public int attackDamage;
    Collider2D[] hitPlayers;
    // Start is called before the first frame update
    void Start()
    {
        
        minimumDamage = attackDamage / 2;

        minimumDamage = Mathf.FloorToInt(minimumDamage);

        hitableLayer = LayerMask.GetMask("Distarctable", "Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
        
      if(pathfinder.reachedEndOfPath == true && attackCooldown <= 0)
      {

        PerformAttack();

      }

    }
     void PerformAttack()
     {
        attackCooldown = 0.7f;
        hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRadius, playerLayer);

        foreach(Collider2D hitable in hitPlayers)
        {
          if(hitable.GetComponent<PlayerMovement>().isDashing != true)
          {

          animator.SetTrigger("Attack");

          if(hitable.gameObject.transform.position.x < gameObject.transform.position.x)
          {
             gameObject.transform.localScale = new Vector3(-1, 1, 1);
          }
          
          else
          {
            gameObject.transform.localScale = Vector3.one;
          }
          

          if(hitable.gameObject.layer == 3 && hitable.GetComponent<PlayerTakeDamage>().InvincTime <= 0 && hitable.GetComponent<PlayerMovement>().isDashing != true)
          {
            
           hitable.GetComponent<PlayerTakeDamage>().TakeDamage(Random.Range(minimumDamage, attackDamage));
          }

          }


        }
     
     }

     void OnDrawGizmosSelected()
     {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
     }
     
      private void OnCollisionStay2D(Collision2D other) {
      
       if(other.gameObject.tag == "Box" || other.gameObject.tag == "Barrel" )
       {

         if(attackCooldown <= 0 && pathfinder.reachedEndOfPath || pathfinder.enabled == false)
         {
          
          animator.SetTrigger("Attack");
          
          if(other.gameObject.transform.position.x < gameObject.transform.position.x)
          {
             gameObject.transform.localScale = new Vector3(-1, 1, 1);
          }

          else
          {
            gameObject.transform.localScale = Vector3.one;
          }

          attackCooldown = 0.7f;

          other.gameObject.GetComponent<DamageToDistractableObject>().TakeDamageDistraction(4);
 

         }

        }
     }

      
     
}

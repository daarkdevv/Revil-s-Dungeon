using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public GameObject playerTarget;
    public LayerMask playerLayer;
    public bool isPlayerDetected;
    public float detectionRadius;

    Transform playerTransform;
    public AIPath pathfinder;
    public LayerMask obstacleLayer, enemy1, destructibleLayer;
    public float timeSincePlayerLost;
    public Animator animator;
    public Vector2 moveDirection;
    public int randomDirection;
    public float patrolTimer, avoidanceCooldown;
    public Rigidbody2D rb;
    public PlayerTakeDamage playerDamageHandler;
    public float moveSpeed;
    AIDestinationSetter pathDestinationSetter;
    [SerializeField]
    float moveForce; 
    bool canWalkRandomly;
    public RaycastHit2D lineOfSightHit, upwardRayHit;
    // Start is called before the first frame update
    void Start()
    {
      playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

      AstarPath.active.Scan();
      playerTarget = GameObject.FindGameObjectWithTag("Player");
      gameObject.GetComponent<AIDestinationSetter>().target = playerTarget.GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        UpdateFacingDirection();
        DetectPlayer();
        PatrolRandomly();

    }
     void DetectPlayer()
    {
      
        isPlayerDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if(isPlayerDetected == true)
        {
            PerformLineOfSightCheck(); 
        }
        else{
          
            pathfinder.enabled = false;
        }
    }

     void PerformLineOfSightCheck()
    {
      lineOfSightHit = Physics2D.Linecast(transform.position, playerTarget.transform.position, obstacleLayer);
  

      if( lineOfSightHit == true )
      {
        timeSincePlayerLost -= Time.deltaTime;
        if(timeSincePlayerLost <= 0)
        {
         pathfinder.enabled = false;
        }
      }

      else if(lineOfSightHit == false)
      {
        timeSincePlayerLost = 10f;
        pathfinder.enabled = true;
      }


    if(isPlayerDetected == true && timeSincePlayerLost > 0 )
    {

     RaycastHit2D[] itemsInSight = Physics2D.LinecastAll(transform.position, playerTransform.transform.position, destructibleLayer);

      foreach(RaycastHit2D hitObject in itemsInSight)
      {
         if(hitObject.collider.gameObject.layer == 9 )
         {
           if(pathfinder.reachedEndOfPath == true)
           {

            hitObject.collider.gameObject.GetComponent<DamageToDistractableObject>().ScanWhenMovedDoor();

           }
         }
        
      }

    }

    }
    void UpdateFacingDirection()
    {
        if(pathfinder.desiredVelocity.x >= 0.01)
        {
         transform.localScale = Vector3.one;
        }
        else if(pathfinder.desiredVelocity.x <= -0.01)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if(pathfinder.desiredVelocity.sqrMagnitude > 0.01)
        {
         animator.SetBool("IsRunning", true);
        }
        else
        {
           animator.SetBool("IsRunning", false); 
        }
    }
     void PatrolRandomly()
     {
       if(isPlayerDetected == false || isPlayerDetected == true && timeSincePlayerLost <= 0)
       {
         if(patrolTimer <= 0)
         {
            patrolTimer = Random.Range(1.9f, 4.2f);
            randomDirection = Random.Range(0, 3);
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
           

         }
         else
         {
            if(randomDirection >= 2)
            {
              rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
            if(rb.velocity.magnitude >= 0.01)
            {
                animator.SetBool("IsRunning", true);
                if(rb.velocity.x <= -0.01)
                {
                    transform.localScale = new Vector3(-1,1,1);
                }
                else if(rb.velocity.x >= 0.01)
                {
                    transform.localScale = Vector3.one;
                }
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
            }
            patrolTimer -= Time.deltaTime;
         }
       }
     }

     private void OnCollisionEnter2D(Collision2D other) 
     {

       if(other.gameObject.layer == 6) // Enemy layer collision
       {

         if(moveDirection.x >= 0.0001f)
         {
            moveDirection.x = -Mathf.Abs(moveDirection.x);
         }

         else if(moveDirection.x <= -0.0001f)
         {
           moveDirection.x = Mathf.Abs(moveDirection.x);
         }

        if(moveDirection.y >= 0.0001f)
        {
          moveDirection.y = -Mathf.Abs(moveDirection.y);
        }
        
       else if(moveDirection.y <= -0.0001f)
        {
          moveDirection.y = Mathf.Abs(moveDirection.y);
        }

       }
     }
    
    private void OnDrawGizmosSelected() {
       Gizmos.color = Color.red;

       Gizmos.DrawWireSphere(transform.position, detectionRadius);

       Gizmos.DrawLine(transform.position, playerTarget.transform.position);
    }
}

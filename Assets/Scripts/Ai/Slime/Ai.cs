using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Ai : MonoBehaviour
{
    public GameObject Target;
    public LayerMask Player;
    public bool isDetected;
    public float RaduisDetect;

    Transform PlayerCast;
    public AIPath aiP;
    public LayerMask Colliders , enemy1,BoxnBarrl;
    public float losingtimeLocation;
    public Animator anim;
    public Vector2 direction;
    public int rand;
    public float timer,timeravoid;
    public Rigidbody2D rb;
    public PlayerTakeDamage ptakeDamage;
    public float speed;
    AIDestinationSetter setter;
    [SerializeField]
    float directionForce; 
    bool canwalkrand;
    public RaycastHit2D hitcollide ,Uphit2D_E;
    // Start is called before the first frame update
    void Start()
    {
      PlayerCast = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

      AstarPath.active.Scan();
      Target = GameObject.FindGameObjectWithTag("Player");
      gameObject.GetComponent<AIDestinationSetter>().target = Target.GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        FlipEnemyAndRun();
        DetectPlayer();
        WalkRandomly();

    }
     void DetectPlayer()
    {
      
        isDetected = Physics2D.OverlapCircle(transform.position,RaduisDetect,Player);

        if(isDetected == true)
        {
            raycast(); 
        }
        else{
          
            aiP.enabled = false;
        }
    }

     void raycast()
    {
      hitcollide = Physics2D.Linecast(transform.position,Target.transform.position,Colliders);
  

      if( hitcollide == true )
      {
        losingtimeLocation -= Time.deltaTime;
        if(losingtimeLocation <= 0)
        {
         aiP.enabled = false;
        }
      }

      else if(hitcollide == false)
      {
        losingtimeLocation = 10f;
        aiP.enabled = true;
      }


    if(isDetected == true && losingtimeLocation > 0 )
    {

     RaycastHit2D[] itemsInSight = Physics2D.LinecastAll(transform.position,PlayerCast.transform.position,BoxnBarrl);

      foreach(RaycastHit2D Obj in itemsInSight)
      {
         if(Obj.collider.gameObject.layer == 9 )
         {
           if(aiP.reachedEndOfPath == true)
           {

            Obj.collider.gameObject.GetComponent<DamageToDistractableObject>().ScanWhenMovedDoor();

           }
         }
        
      }

    }

    }
    void FlipEnemyAndRun()
    {
        if(aiP.desiredVelocity.x >= 0.01)
        {
         transform.localScale = Vector3.one;
        }
        else if(aiP.desiredVelocity.x <= -0.01)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if(aiP.desiredVelocity.sqrMagnitude > 0.01)
        {
         anim.SetBool("IsRunning", true);
        }
        else
        {
           anim.SetBool("IsRunning",false); 
        }
    }
     void WalkRandomly()
     {
       if(isDetected == false || isDetected == true && losingtimeLocation <= 0)
       {
         if(timer <= 0)
         {
            timer = Random.Range(1.9f,4.2f);
            rand = Random.Range(0,3);
            direction = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
           

         }
         else
         {
            if(rand >= 2)
            {
              rb.velocity = direction * speed * Time.deltaTime;
            if(rb.velocity.magnitude >= 0.01)
            {
                anim.SetBool("IsRunning",true);
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
                anim.SetBool("IsRunning",false);
            }
            }
            timer -= Time.deltaTime;
         }
       }
     }

     private void OnCollisionEnter2D(Collision2D other) 
     {

       if(other.gameObject.layer == 6)
       {

         if(direction.x >= 0.0001f)
         {
            direction.x = -Mathf.Abs(direction.x);
         }

         else if(direction.x <= -0.0001f)
         {
           direction.x = Mathf.Abs(direction.x);
         }

        if(direction.y >= 0.0001f)
        {
          direction.y = -Mathf.Abs(direction.y);
        }
        
       else if(direction.y <= -0.0001f)
        {
          direction.y = Mathf.Abs(direction.y);
        }

       }
     }
    
    private void OnDrawGizmosSelected() {
       Gizmos.color = Color.red;

       Gizmos.DrawWireSphere(transform.position,RaduisDetect);

       Gizmos.DrawLine(transform.position,Target.transform.position);
    }
}

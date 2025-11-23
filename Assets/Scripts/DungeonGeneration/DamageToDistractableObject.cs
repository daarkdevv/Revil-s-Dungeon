using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class DamageToDistractableObject : MonoBehaviour
{
    public ScriptableDistract ScriptableDistract;
    public int Health;
    public int CurrentHealth;
    public Animator anim;
    public bool IsDestroyed;
    public GameObject particle;
    public GameObject[] Shatters;
    
    public bool isInDoorRaduis;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Health = ScriptableDistract.Health;

        CurrentHealth = ScriptableDistract.Health;

        anim = gameObject.GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
      if(rb.velocity.magnitude > 0 )
      {
        ScanWhenMove();
      }
       
      if(Input.GetKeyDown(KeyCode.M))
      {
        ScanWhenMovedDoor();
      } 

    }
   
  public  void TakeDamageDistraction(int damage)
    {

      Instantiate(particle,transform.position,Quaternion.identity);

      CurrentHealth -= damage;

      anim.SetTrigger("Damage");



      if(CurrentHealth <= 0)
      {
        IsDestroyed = true;
        
        Destroyed();
      }

    }

  public  void Destroyed()
  {
   anim.SetBool("IsDestroyed",true);

   this.gameObject.GetComponent<Collider2D>().enabled = false;

    for (int i = 0; i < Shatters.Length; i++)
    {
       Instantiate(Shatters[i],new Vector3(transform.position.x + Random.Range(-0.3f,0.5f),transform.position.y + Random.Range(-0.3f,0.5f),transform.position.z),Quaternion.identity);
    }
   
    gameObject.layer = 0;
     
    var bounds = GetComponent<Collider2D>().bounds;

    AstarPath.active.UpdateGraphs(bounds);
 
    Destroy(gameObject,0.4f);
    
  }


 public void ScanWhenMove()
  {
    

    var bounds = GetComponent<Collider2D>().bounds;

    var ScanObj = new GraphUpdateObject(bounds);

    ScanObj.updatePhysics = true;

    AstarPath.active.UpdateGraphs(bounds);

  }

 public void ScanWhenMovedDoor()
  {
    gameObject.layer = 0;

    var bounds = GetComponent<Collider2D>().bounds;
  
    var ScanObj = new GraphUpdateObject(bounds);

    ScanObj.updatePhysics = true;
  
    AstarPath.active.UpdateGraphs(ScanObj);

    StartCoroutine(nameof(ChangeObject));
  }

 public IEnumerator ChangeObject()
  {
    yield return new WaitForSeconds(0.2f);
    gameObject.layer = 9;
    yield return new WaitForSeconds(2.5f);
  }

}

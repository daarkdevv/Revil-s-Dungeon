using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public float PlayerHealth;
    public  float InvincTime;  [SerializeField]
    public float currenthealth;
    public Animator anim;
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;
    public PlayerAttack pattack;
    public GameObject BloodParticle;
    public healthslider healthslider;
    public SliderShaking ShakeSlide;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = ItemCounter.Instance.MaxHealth;
        
        currenthealth = ItemCounter.Instance.MaxHealth;

        healthslider.setHealthMax(PlayerHealth);

        Mathf.Clamp(ItemCounter.Instance.MaxDamage,0,ItemCounter.Instance.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if(InvincTime > 0)
        {
           InvincTime -= Time.deltaTime;

           StartCoroutine("InvicAnim");
            
        }

    }
   public void TakeDamage(int Pdamage)
    {
        ItemCounter.Instance.CurrentHealth -= Pdamage;

        currenthealth = ItemCounter.Instance.CurrentHealth;

        
        InvincTime = ItemCounter.Instance.InvincTime;


        Instantiate(BloodParticle,transform.position,Quaternion.identity);


        anim.SetTrigger("Hurt");
      
        if(ItemCounter.Instance.CurrentHealth <= 0)

        {

          Death();

        }

        ShakeSlide.TriggerShake();
        

    }
    void Death()
    {
       
        anim.SetBool("IsDead",true);

        rb.velocity = Vector2.zero;

        this.GetComponent<Collider2D>().enabled = false;

        playerMovement.enabled = false;

        pattack.enabled = false;  

    }

    IEnumerator InvicAnim()
    {
        if(InvincTime > 0 && ItemCounter.Instance.CurrentHealth > 0)
     {   
      
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.4f);

        yield return new WaitForSeconds(0.4f);

         gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        
    }
        
         
       
    }

}

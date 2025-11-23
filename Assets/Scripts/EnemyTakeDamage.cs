using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TMPro;
public class EnemyTakeDamage : MonoBehaviour
{
    public  float health;
    public  float currenthealth;
    public GameObject target,ParticleBlood,DamageText;
    public float force;
    public Animator anim;
    public Ai ai;
    public AIPath path;
    public Rigidbody2D rb;
    public Color dodgeColor;
    public int evasionChance;
    public float knockbacktime;
    public float DashDamageCoolDownTimer;
    public Color damagecolor,expcolor,OrangeDamage,YellowDamage;
    bool isdead;
    public GameObject canvasenable;
    public EnemyHealthBar enemyHealthBar;
    int ItemChanceOrder;
    public int XpAfterDeath;
    public List<GameObject> itemToDrop;
    float quantity = 1;
    float damageTaken;
    bool IsCritcalHit;

    // Start is called before the first frame update

    void Start()
    {

       target = GameObject.FindGameObjectWithTag("Player");

       currenthealth = health;

       InvokeRepeating("Update",0,0.2f);

       enemyHealthBar.setHealthMax(health);

    }

    // Update is called once per frame
    void Update()
    {
        KnockBack();
    }

    public void TakeDamage(float damage,int accuracy)
    {
        
      accuracy -= evasionChance;

      int randEva = Random.Range(0,101); //generates a number btw 0 to 100 if the rand eva was bigger than accuracy then it will evade.

  
      if(randEva > accuracy)
      {

        Dodging();

        return;

      }

      //Critcal Chance Section.

      int randCrit = Random.Range(0,101);


      if(randCrit <= ItemCounter.Instance.CriticalChance)
      {
      
         DamageText.GetComponentInChildren<TextMeshPro>().text = (int.Parse(damage.ToString()) * 4).ToString() + " Crit !";  
         

         currenthealth -= damage * 4;
         
         IsCritcalHit = true;
      }
      
      else
      {

       IsCritcalHit = false; 

       currenthealth -= damage;

       DamageText.GetComponentInChildren<TextMeshPro>().text = damage.ToString();

      }
       
      damageTaken = damage;

      SetDamageTextColor();

      Instantiate(DamageText, new Vector2( transform.position.x ,transform.position.y + Random.Range(0.2f,0.6f)),Quaternion.identity);

      KnockBack2(); 
    }

    void Dodging()
    {
        StartCoroutine("EvadeSprite");

        DamageText.GetComponentInChildren<TextMeshPro>().color = dodgeColor;

        DamageText.GetComponentInChildren<TextMeshPro>().text = "DODGED !";
     
        Instantiate(DamageText, new Vector2( transform.position.x ,transform.position.y + Random.Range(0.2f,0.6f)),Quaternion.identity);
    }

    void KnockBack()
    {

        if(DashDamageCoolDownTimer > 0)
        DashDamageCoolDownTimer -= Time.deltaTime;

        if(knockbacktime <= 0 && isdead == false)
        {

          path.enabled = true;

        }

        else
        {
          
          path.enabled = false;

          knockbacktime -= Time.deltaTime;

        }


        if(isdead)
        {
          rb.velocity = Vector3.zero;
        }

    }


    void KnockBack2()
    {

      canvasenable.SetActive(true);
       
      Instantiate(ParticleBlood,transform.position,Quaternion.identity);     
     
      anim.SetTrigger("Damage");

      Vector2 DiffrenceLength;
       
      if(target.GetComponent<PlayerMovement>().isDashing == false)
      {

        DiffrenceLength = gameObject.transform.position - target.transform.position;

        rb.AddForce(DiffrenceLength * force,ForceMode2D.Impulse);

      }
      
      path.enabled = false;

      knockbacktime = 0.4f;

      if(currenthealth <= 0 && gameObject != null)
      {
        anim.SetBool("IsDead",true);

        Death();

      }
 

    }



    void Death()
    {

      isdead = true;

      path.enabled = false;

      ai.enabled = false;

      GetComponent<Collider2D>().enabled = false;

      GenerateLoot();

      ItemCounter.Instance.increaseXp(XpAfterDeath);

      DamageText.GetComponentInChildren<TextMeshPro>().color = expcolor;

      DamageText.GetComponentInChildren<TextMeshPro>().text = "EXP+ " + XpAfterDeath.ToString();

      Instantiate(DamageText,new Vector2(transform.position.x ,transform.position.y + Random.Range(1f,1.5f)),Quaternion.identity);
      
      gameObject.GetComponent<Collider2D>().enabled = false;

      canvasenable.SetActive(false);

      Destroy(this.gameObject,1.1f);

    }


    void SetDamageTextColor()
    {

      if(IsCritcalHit)
      {
        damageTaken = damageTaken * 4;
      }
      
      if(damageTaken >= health / 2)
      {

        DamageText.GetComponentInChildren<TextMeshPro>().color = damagecolor;

      }

      else if(damageTaken >= health / 3)
      {

        DamageText.GetComponentInChildren<TextMeshPro>().color = OrangeDamage;

      }

      else 
      {

        DamageText.GetComponentInChildren<TextMeshPro>().color = YellowDamage;

      }



    }


  void GenerateLoot()
  { 
      
     List<int> itemTable = new List<int> {50,50,25,15,3}; //143 all of them.
       
     quantity = quantity + ItemCounter.Instance.LuckStat / 4;
     
     for (int i = 0; i < quantity; i++)
     {

        ItemChanceOrder = -1;        

        int RandLootDropChance = Random.Range(1,143);

        foreach(int chance in itemTable)
        {

           if(chance <= RandLootDropChance)
           {
             
             RandLootDropChance -= chance;
             
             ItemChanceOrder++;
 
           }

           else
           {
          
             if(ItemChanceOrder > -1)
             {

              Instantiate(itemToDrop[ItemChanceOrder],transform.position,Quaternion.identity);

             }
            
             break;
 
           }

         }
     
       }
    }


    IEnumerator EvadeSprite()
    {

      gameObject.GetComponent<SpriteRenderer>().color = dodgeColor;

      yield return new WaitForSeconds(0.3f);
      
      gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,255);

      
    }
}

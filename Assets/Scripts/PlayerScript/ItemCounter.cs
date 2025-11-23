using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemCounter : MonoBehaviour
{
     public TMP_Text CoinCount;

    public int CoinNumber = 0;
    public int KeyCount = 0;

   [Header("Health")]

     public float MaxHealth;

     public float CurrentHealth;

     public TMP_Text HealthCount;

    [Header("Speed")]

     public float MaxSpeed;
     

     public float CurrentSpeed;

  [Header("Damage && Melee Range")]
     
    public int weaponDamage;
     public int MaxDamage;

     public float MeeleRange;

     public float CurrentDamage;

     public int strength;

     public float CriticalChance;

     public Transform attackpos;

     public TMP_Text[] StatsTxt;

[Header("Different Stats")]
     public int LuckStat;

     public float IntellgenceStat;
     public float CharismaStat;

     public float resistance;

     public int AgilityStat;

     public int accuracyStat;

     

     public int evasionstat;

     public float stamina;
     public float MaxStamina;
     public bool canGenerate; 
     public float staminaRegen;
     
[Header("Defence")]
     public int DefenceStat;

     public float InvincTime;

 [Header("Other")]

     public TMP_Text XAxis;
     public TMP_Text YAxis;
     
[Header("XP")]
 
 public int currentXp = 0;
 public int[] XPLevels = {13,47,98,167,364,611,1124};
 public int currentLevel = 0;

 public int attributePoints;
 public TMP_Text CurrentLevelXPTxt;

  public static ItemCounter Instance;
    // Start is called before the first frame update
   void Awake() {
    
    {
        CurrentLevelXPTxt.text = "Lv." + currentLevel.ToString();

        CurrentHealth = MaxHealth;
        stamina = MaxStamina;

        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

      Mathf.Clamp(CurrentHealth,0,MaxHealth);

    }
}
    // Update is called once per frame
    void Update()
    {
        if(stamina < MaxStamina && canGenerate)
        {
            if(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
            {
              stamina += staminaRegen * Time.fixedDeltaTime;
            }
            else
            {
              stamina += staminaRegen * 2 * Time.fixedDeltaTime;
            }
            
        }

        CoinCount.text = ":" + CoinNumber.ToString();

        XAxis.text = "X:" + (int) transform.position.x;
        YAxis.text = "Y:" + (int) transform.position.y;

       HealthCount.text = "HP : " + (int) CurrentHealth + "/" + (int) MaxHealth;

      //  StatsTxt[0].text = "STR :" + " " + strength;
       // StatsTxt[1].text = "INT :" + " " + IntellgenceStat;
       // StatsTxt[2].text = "AGI :" + " " + AgilityStat;
       // StatsTxt[3].text = "DEF :" + " " + DefenceStat;
       // StatsTxt[4].text = "ACC :" + " " + accuracyStat + "%";
       // StatsTxt[5].text = "CRIT :" + " " + CriticalChance + "%";
       // StatsTxt[6].text = "SPD :" + " " +  (MaxSpeed / 100).ToString("0.00");
       // StatsTxt[7].text = "LUCK :" + " " + LuckStat;
       // StatsTxt[8].text = "EVA :" + " " + evasionstat + "%";
       // StatsTxt[9].text = "CHA :" + " " + CharismaStat;
        
       // StatsTxt[10].text = "HP : " + (int) CurrentHealth + "/" + (int) MaxHealth;
       // StatsTxt[11].text =  "ST : " + (int)stamina + "/" + MaxStamina;
       // StatsTxt[12].text =  "XP : " + currentXp + "/" + XPLevels[currentLevel];
       // StatsTxt[13].text = "LV." + currentLevel;
      //  StatsTxt[14].text = "RES :" + " " +  resistance.ToString("0.0");
       // StatsTxt[15].text = "GOLD : " + CoinNumber;
        StatsTxt[16].text = ":" + KeyCount.ToString();
      //  StatsTxt[17].text = "ATR PTS : " + attributePoints;
     //ExpSystem


     if(currentXp >= XPLevels[currentLevel])
     {
       currentXp -= XPLevels[currentLevel];

       currentLevel++;

       attributePoints++;    

      CurrentLevelXPTxt.text = "Lv." + currentLevel.ToString();

       XPSlider.instance.slider.maxValue = XPLevels[currentLevel];
     }

        
    }


    void OnDrawGizmosSelected()
  {

   Gizmos.color = Color.green;
   Gizmos.DrawWireSphere(attackpos.position,MeeleRange);

  }

 public void increaseXp(int Increase)
  {
    currentXp += Increase;
  }

}

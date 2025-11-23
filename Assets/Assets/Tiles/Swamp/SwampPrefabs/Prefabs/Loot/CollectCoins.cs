using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectCoins : MonoBehaviour
{

      Vector2 ref_;
      public Animator animator;

      bool IsCollected;

      public bool canMoveTo;

      float timer;

      public int GoldCount;

      [SerializeField]

      public GameObject CollectTxt;

      bool enterPlayer;

      public Vector3 offset;

      public float radius;

      public Color collectColor;

      public LayerMask Player;
      private void Awake() {

        animator = GetComponent<Animator>();
        timer = 0.7f;
        StartCoroutine(TimerAfterSpawn());
      }


       private void FixedUpdate() {
        
         enterPlayer = Physics2D.OverlapCircle(transform.position + offset,radius,Player);
   

        if(enterPlayer && canMoveTo)
         enterPlayer_(0);

      }


        void enterPlayer_(int test) {
         
        
        if( !IsCollected)
        {

          ColorUtility.TryParseHtmlString("#FFB723" , out collectColor);

          CollectTxt.GetComponentInChildren<TextMeshPro>().color = collectColor;
            
          CollectTxt.GetComponentInChildren<TextMeshPro>().text = "+" + GoldCount + "GOLD";

          Instantiate(CollectTxt,transform.position,Quaternion.identity);  

          animator.SetBool("IsCollected",true);

          ItemCounter.Instance.CoinNumber += GoldCount;

        }

       
        IsCollected = true; 
      
    }

    IEnumerator TimerAfterSpawn()
    {
      yield return new WaitForSeconds(timer);
      canMoveTo = true;
   
    }

 

  void destroyWhenFinish111()
   {
     Destroy(gameObject);
   }

  private void OnDrawGizmosSelected() {
    Gizmos.color = Color.grey;

    Gizmos.DrawWireSphere(transform.position + offset,radius);
  }
    

}

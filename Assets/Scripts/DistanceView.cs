using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DistanceView : MonoBehaviour
{
    private GameObject player;
    public Color alpha;

    public GameObject buttonOfPotion;

    private Inventory inv;
    private SlotSys slotS;

    private Animator animator;

    public string ItemTag;

    public bool isStackable;

    public int maxStackItem;

    public bool canBeDestroyed;

    public bool hasDetectedSlot = false;

    public GameObject TxtWhenCollect;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame

    public void Pickup(string tag)
    {
        
        animator.SetTrigger("Pickup");

        TxtWhenCollect.GetComponentInChildren<TextMeshPro>().text = "+1 " + "Health Potion";

        TxtWhenCollect.GetComponentInChildren<TextMeshPro>().color = Color.red;

        Instantiate( TxtWhenCollect,new Vector2(transform.position.x,transform.position.y + 0.2f),Quaternion.identity);

        foreach(var slot in inv.slot)
        {

         slot.GetComponent<SlotSys>().SeeIfItemLessMaxStack(maxStackItem); 

         if(isStackable && slot.GetComponent<SlotSys>().currentStack > 0)
         {

            if(slot.transform.childCount >= 2  && slot.GetComponent<SlotSys>().currentStack < maxStackItem)
           {

             if(slot.transform.GetChild(1).gameObject.tag == tag)
             {
   
              hasDetectedSlot = true;
              slot.GetComponent<SlotSys>().currentStack++;
              break;

             }


           }

         }
         
            
            
        }


        if(hasDetectedSlot)
        {

           animator.SetTrigger("Pickup");
          

          hasDetectedSlot = false;
          return;
        }
        


      for (int i = 0; i < inv.slot.Count; i++)
      {
          slotS = inv.slot[i].GetComponent<SlotSys>();

          slotS.SeeIfItemLessMaxStack(maxStackItem);
        
        
          if(slotS.isFull == false ) // it can't spawn beacuse isStackable system not here yet.
          {

            if(slotS.currentStack == 0)
            {
                slotS.AlreadySpawned = false;
            }
            else
            {
              slotS.AlreadySpawned = true;
            }
           

            if(!isStackable && slotS.transform.childCount == 1)
            {
              if(slotS.currentStack < 1)
              {
                Instantiate(buttonOfPotion,inv.slot[i].transform);
                inv.slot[i].GetComponent<SlotSys>().currentStack++;

                  animator.SetTrigger("Pickup");
               
                 
                break;
              }
              
            }

            else if(isStackable)
            {

              if(slotS.AlreadySpawned == false)
              {
                Instantiate(buttonOfPotion,inv.slot[i].transform);
              }

              slotS.SeeIfItemLessMaxStack(maxStackItem);
              
              if(inv.slot[i].transform.childCount >= 2 && inv.slot[i].transform.GetChild(1).tag == ItemTag )
              {

                inv.slot[i].GetComponent<SlotSys>().currentStack++;
                
           
                  animator.SetTrigger("Pickup");
                

                break;
              }
              
             
            }
          

          }       

      }

    }

    void DestroyAfterAnimation()
    {
      Destroy(gameObject);
      
    }

    
}

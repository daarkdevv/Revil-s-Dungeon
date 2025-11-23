using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class OpenChest : MonoBehaviour
{
  private Animator anim;

  public List<GameObject> itemsToDropChest;

  public bool hasOpened = false;

  public int KeyNeed;
  int PossibleLootDrops;

  private void Awake() {

    var bounds = GetComponent<Collider2D>().bounds;

    AstarPath.active.UpdateGraphs(bounds);

    anim = GetComponent<Animator>();

  }

 private void Update() {

 }


 public void useKey()
 {
    if(KeyNeed >= 1 && !hasOpened && ItemCounter.Instance.KeyCount >= 1)
    {
      KeyNeed--;

      ItemCounter.Instance.KeyCount--;

      if(KeyNeed == 0)
      {
        OpenChest_();
        hasOpened = true;
        Destroy(gameObject.GetComponent<OpenChest>(),0.8f);
      }

    }
 }


  public void OpenChest_()
  {

    Vector2 SpawnPos = new Vector2(transform.position.x,transform.position.y - 0.5f);

    anim.SetTrigger("Open");



    if(!hasOpened)
    {

      PossibleLootDrops = Random.Range(1,3);

       for (int i = 0; i < PossibleLootDrops + ItemCounter.Instance.LuckStat / 2; i++)
       {

        int randDropChance = Random.Range(1, 51);

        if(randDropChance >= 25 )
        {

          Instantiate(itemsToDropChest[0],SpawnPos,Quaternion.identity);
          

        }

        else if(randDropChance < 25  + ItemCounter.Instance.LuckStat &&  randDropChance > 11)
        
        {

            Instantiate(itemsToDropChest[1],SpawnPos,Quaternion.identity);

        }

        else if(randDropChance < 10 + + ItemCounter.Instance.LuckStat &&  randDropChance > 1)
        {

          Instantiate(itemsToDropChest[2],SpawnPos,Quaternion.identity);

        }

         else if(randDropChance == 1)
        {

          Instantiate(itemsToDropChest[3],SpawnPos,Quaternion.identity);

        }

        
      
       }

    }
     
     gameObject.layer = 0;
     
    var bounds = GetComponent<Collider2D>().bounds;

    AstarPath.active.UpdateGraphs(bounds);

     Destroy(gameObject,0.8f);
  }


}

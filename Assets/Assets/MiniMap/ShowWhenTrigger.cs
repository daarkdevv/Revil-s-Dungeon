using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowWhenTrigger : MonoBehaviour
{
   public List<Collider2D> gameObjectInMap;
   bool HasEntered;

    // Update is called once per frame
    void Update()
    {
        
      if(gameObject.GetComponent<Tilemap>().color.a >= 1)
      {
        Destroy(gameObject.GetComponent<BoxCollider2D>(),10);
        Destroy(gameObject.GetComponent<ShowWhenTrigger>(),10);
      }

       if(HasEntered && gameObject.GetComponent<Tilemap>().color.a != 255)
     {
         gameObject.GetComponent<Tilemap>().color += new Color(1,1,1,2 * Time.fixedDeltaTime);
     }


    }
       [System.Obsolete]
    private void OnTriggerStay2D(Collider2D other ) {

        foreach(Collider2D go in gameObjectInMap)
        {
          if(go != null)
          {
           if(go.tag != "BlueTile") 
           go.transform.FindChild("MiniIcon").GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Tilemap>().color;
           
           if(go.gameObject.tag == "BlueTile" && gameObject.tag != "Corridor")
           {
             go.transform.parent = gameObject.transform;
             go.gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Tilemap>().color;
           }

          }
  
        
        }

     

    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other) {
       
      if(other.tag == "Player")
      {
        HasEntered = true;
      } 


      if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Barrel") || other.gameObject.tag == "BlueTile")
     {
              
        gameObjectInMap.Add(other);
        
        if(other.tag != "BlueTile")
        other.gameObject.transform.FindChild("MiniIcon").GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Tilemap>().color;
            
     }

    }
   private void OnTriggerExit2D(Collider2D other) {
    
      if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Barrel") || other.gameObject.CompareTag("BlueTile"))
      gameObjectInMap.Remove(other);

  }

  
}

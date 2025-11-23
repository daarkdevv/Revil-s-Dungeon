using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Sprite DoorSprite;
    public Vector2 boxSize;
    public Vector3 Offset;
    public bool IsThereObjects;

    public LayerMask AllowedToOpen;
    // Start is called before the first frame update

   private void FixedUpdate() {
      
     IsThereObjects = Physics2D.OverlapBox(transform.position + Offset,boxSize,0,AllowedToOpen);

     if(IsThereObjects && gameObject.GetComponent<SpriteRenderer>().sprite != null)
     {
       gameObject.GetComponent<SpriteRenderer>().sprite = null;
       gameObject.GetComponent<Collider2D>().enabled = false;
     }
     
     else if(IsThereObjects == false)
     {
       gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprite;
       gameObject.GetComponent<Collider2D>().enabled = true;
     }

   }

   void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireCube(transform.position + Offset,boxSize);
   }

}

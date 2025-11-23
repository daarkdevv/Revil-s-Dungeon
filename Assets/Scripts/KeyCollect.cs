using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyCollect : MonoBehaviour
{

  private Animator anim;

  bool hasTaken;

  public Color silvercolor;

  public GameObject textTake;  

 private void Awake() {
    anim = GetComponent<Animator>();
 }

  private void OnTriggerEnter2D(Collider2D other) {

    
    if(other.gameObject.tag == "Player" && hasTaken == false)
    {
        textTake.GetComponentInChildren<TextMeshPro>().text = "+1 " + "key";

        textTake.GetComponentInChildren<TextMeshPro>().color = silvercolor;

        Instantiate(textTake,new Vector2(transform.position.x,transform.position.y + 0.2f),Quaternion.identity);

        ItemCounter.Instance.KeyCount++;

        hasTaken = true;

        anim.SetTrigger("KeyTaken");

    }


  }

  void DestroyOnFinish()
  {

    Destroy(gameObject);

  }
}

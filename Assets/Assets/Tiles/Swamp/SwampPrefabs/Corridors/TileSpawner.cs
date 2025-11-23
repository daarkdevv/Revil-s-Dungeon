using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject BlueTileHori,BlueTileVerti;
    // Start is called before the first frame update
    void Start()
    {
        
      StartCoroutine("WaitTillSpawn"); 
        
    }

    IEnumerator WaitTillSpawn()
    {
        yield return new WaitForSeconds(2);

      if(gameObject.tag == "TopDoor" || gameObject.tag == "BottomDoor")
      Instantiate(BlueTileVerti,transform.position,Quaternion.identity);
      else
      Instantiate(BlueTileHori,transform.position,Quaternion.identity);

      StartCoroutine(WaitTillSpawn());

      if(transform.parent == null)
      {
        Destroy(gameObject);
      }


        Destroy(gameObject);
    }
}

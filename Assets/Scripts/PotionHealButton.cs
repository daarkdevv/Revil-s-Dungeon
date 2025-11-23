using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealButton : MonoBehaviour
{
    private GameObject parent;
    private SlotSys SlotS;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;

        SlotS = parent.GetComponent<SlotSys>();

        
    }
    
    


    public void UsePotion()
    {
        SlotS.currentStack--;

        if(SlotS.currentStack < 1)
        {
           Destroy(gameObject);
        }

    }
}

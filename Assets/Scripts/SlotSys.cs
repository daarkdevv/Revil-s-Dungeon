using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlotSys : MonoBehaviour
{
     public int currentStack = 0;
     
     public bool AlreadySpawned = false;

     public bool isFull;

    public TMP_Text textStack;

    [System.Obsolete]
    private void FixedUpdate() {

       if( currentStack > 0  )
        {
             textStack.text = (currentStack).ToString();
        }
        else
        {
            if(textStack.text != null)
            {
                textStack.text = null;

            }
           
        } 
       
     }

    public void SeeIfItemLessMaxStack(int maxStack)
    {

       
        if(currentStack == maxStack)
        {
            isFull = true;
        }
        else
        {
            isFull = false;
        }
         

    }
}

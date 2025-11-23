using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingInventory : MonoBehaviour
{
    public GameObject Screen;
        
   public void DeActivateScreen()
    {
        Screen.SetActive(false);
    }

    public void activateScreen()
    {
       if(Screen.activeInHierarchy == false) 
       Screen.SetActive(true);
       else
       Screen.SetActive(false);  
    }    
}

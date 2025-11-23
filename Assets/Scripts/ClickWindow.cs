using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickWindow : MonoBehaviour
{
   Vector3 extraSize, smallSize;
   public bool isClicked;
   
   float windowScaleSize = 0.35f;

   private Vector3 startingPosition;

   public bool isFirstWindow;
   public GameObject[] otherUIWindows;

   public Color unhighlightedColor, highlightedColor;
   
   
    // Start is called before the first frame update
    void Start()
    {
  

       if(isFirstWindow)
       {
         isClicked = true;
         gameObject.GetComponent<Image>().color = highlightedColor;
       }

       DisableOtherWindows(); 
         
      if(!isClicked)
      {
         gameObject.GetComponent<Image>().color = unhighlightedColor;
      }


    }


    public void OnClickWindow()
    {
        if(!isClicked)
        {
             
            DisableOtherWindows();
            
            isClicked = true;

               for (int i = 0; i < gameObject.transform.childCount; i++)
               {
                 gameObject.transform.GetChild(i).gameObject.SetActive(true);
               }
             
            gameObject.GetComponent<Button>().interactable = false;

            gameObject.GetComponent<Image>().color = highlightedColor;
        }

       
        
        
    }


    void DisableOtherWindows()
    {
      
           foreach(GameObject window in otherUIWindows)
           {
             if(window.GetComponent<ClickWindow>().isClicked == true && window != this)
             {

               window.GetComponent<ClickWindow>().isClicked = false;
               window.GetComponent<Image>().color = unhighlightedColor;
               window.GetComponent<Button>().interactable = true;

               for (int i = 0; i < window.transform.childCount; i++)
               {
                 window.transform.GetChild(i).gameObject.SetActive(false);
               }
             }
           }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAnimatorHandler : MonoBehaviour
{
   public GameObject InventoryScreen;
   public CanvasGroup BlackScreen;
   public Vector3 LeanScaleUP,LeanScaleDown;
   public Transform DownArea,RightArea,LeftArea,AreaBarDown,UpperTransform;
   GameObject currentWindow;
   public bool IsLeftButton;
   public float[] WindowCenterLocation;
   public GameObject[] otherWindow;
   public int CurrentPage = 0;
   public int PageNumber;
   public UIAnimatorHandler OtherButton;
   public bool IsDoneAnimation;
   private float GlobalSmoothness;
   public GameObject[] QuickSlotBar;
   public GameObject[] ButtonSwitchers;

   [SerializeField]
   private TMP_Text PageNum;

   [SerializeField]
   private UIAnimatorHandler[] CbuttonInvetory;

   private int PageNumText;

   private void Start() 
   {

     GlobalSmoothness = 0.5f;

   }

   public void OpenInventory()
   {

     otherWindow[CurrentPage].SetActive(true);

     if(CurrentPage == 2)
     {

       otherWindow[CurrentPage].transform.LeanMoveY(UpperTransform.position.y,0.6f).setEaseInOutQuad();

     }

     else
     {

      otherWindow[CurrentPage].transform.LeanMoveLocalY(0,0.6f).setEaseInOutQuad();

     }

     
    
     BlackScreen.alpha = 0;
     
     BlackScreen.LeanAlpha(0.7f,0.5f);

   }

   public void ClosingInventory()
   {
    
     otherWindow[CurrentPage].transform.LeanMoveLocalY(-750,0.6f).setEaseInOutQuad().setOnComplete(SetFalse);

     BlackScreen.LeanAlpha(0f,0.5f);
     
   }

   public void SetFalse()
   {

     otherWindow[CurrentPage].SetActive(false);

   }

   public void ScaleUP()
   {

     transform.LeanScale(LeanScaleUP,0.6f).setEaseOutCubic();

   }

   public void ScaleDown()
   {
     transform.LeanScale(LeanScaleDown,0.6f).setEaseOutExpo();
   }

   public void MoveBarDown()
   {

     foreach(GameObject slotBar in QuickSlotBar)
     {

       slotBar.transform.LeanMoveY(-69,GlobalSmoothness).setEaseInOutQuad();

     }

     foreach(GameObject switcher in ButtonSwitchers)
     {

      switcher.transform.LeanMoveY(79,GlobalSmoothness).setEaseInOutQuad();

     }
      
   }

   void SetChild()
   {

      PageNumText = CurrentPage + 1;

      PageNum.text = PageNumText.ToString();

   }

   public void moveBarUp()
   {

     foreach(GameObject slotBar in QuickSlotBar)
     {

      slotBar.transform.LeanMoveY(AreaBarDown.position.y,GlobalSmoothness).setEaseInOutQuad();

     }

      foreach(GameObject switcher in ButtonSwitchers)
     {

      switcher.transform.LeanMoveY(-90,GlobalSmoothness - 0.2f).setEaseInOutQuad();

     }


   }

   public void Switch()
   {

     if(IsDoneAnimation)
     {

      if(IsLeftButton)
      {
         if(CurrentPage == 0)
         {
          
           CurrentPage = 2;

         }

         else
         {

           CurrentPage--;

         }

        SetOtherButtonStatus(); 
 
        SwitchRight();
  
      }

      else
      {
        

        if(CurrentPage == 2)
        {
        
          CurrentPage = 0;
          
        }

        else
        {

          CurrentPage++;

        }
    
        SetOtherButtonStatus();

        SwitchAnimation(); 

      }

    }




     
   }

   void SetOtherButtonStatus()
   {

      OtherButton.GetComponent<UIAnimatorHandler>().CurrentPage = CurrentPage;

      IsDoneAnimation = false;

      OtherButton.GetComponent<UIAnimatorHandler>().IsDoneAnimation = IsDoneAnimation;

      foreach(UIAnimatorHandler uIAnimator in CbuttonInvetory)
      {

         uIAnimator.CurrentPage = CurrentPage;

      }

      SetChild();

   }

   void SwitchAnimation()
   {
     
     for (int i = 0; i < otherWindow.Length; i++)
     {

       if(otherWindow[CurrentPage])
       {
  
         foreach(GameObject window in otherWindow)
         {

           if(window != otherWindow[CurrentPage])
           {
   
            window.transform.LeanScale(LeanScaleDown,GlobalSmoothness).setEaseOutCubic();

            window.transform.LeanMoveLocalX(-1250,0.8f).setEaseInOutQuad().setOnComplete(AfterComplete);

           }

         }
     
         otherWindow[CurrentPage].transform.position = RightArea.position;

         otherWindow[CurrentPage].SetActive(true);

         setMiniUp();

         otherWindow[CurrentPage].transform.LeanScale(LeanScaleUP,GlobalSmoothness).setEaseOutExpo();

         otherWindow[CurrentPage].transform.LeanMoveLocalX(WindowCenterLocation[CurrentPage],GlobalSmoothness);

       }
      
     }
 

   }

   void AfterComplete()
   {

     foreach(GameObject window in otherWindow)
     {

        if(window != otherWindow[CurrentPage])
       {

        window.transform.position = DownArea.position;

        window.SetActive(false);

       }

     IsDoneAnimation = true;

     OtherButton.GetComponent<UIAnimatorHandler>().IsDoneAnimation = IsDoneAnimation;

    }
     

   }

   void SwitchRight()
   {

       for (int i = 0; i < otherWindow.Length; i++)
     {

       if(otherWindow[CurrentPage])
       {
  
         foreach(GameObject window in otherWindow)
         {

           if(window != otherWindow[CurrentPage])
           {

            window.transform.LeanScale(LeanScaleDown,GlobalSmoothness).setEaseOutExpo();
            
            window.transform.LeanMoveLocalX(1250,0.8f).setEaseInOutQuad().setOnComplete(AfterComplete);

           }

         }
     
         otherWindow[CurrentPage].transform.position = LeftArea.position;

         otherWindow[CurrentPage].SetActive(true);

         setMiniUp();

         otherWindow[CurrentPage].transform.LeanScale(LeanScaleUP,GlobalSmoothness).setEaseOutExpo();

         otherWindow[CurrentPage].transform.LeanMoveLocalX(WindowCenterLocation[CurrentPage],GlobalSmoothness);

       }
      
     }

   }

   void setMiniUp()
   {

    if(CurrentPage == 2)
    {

       otherWindow[CurrentPage].transform.position = new Vector2(otherWindow[CurrentPage].transform.position.x,UpperTransform.position.y); 
           
    }

   }

}

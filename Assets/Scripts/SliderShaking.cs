using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderShaking : MonoBehaviour
{
    float Timer;

    [SerializeField]  
    float ResetTimer;

    Vector3 Movement;

    [SerializeField]
    float Smoothness;
    
    [SerializeField]
    float ShakeMagnitude;
    Animator animator;

    public enum WhatIsSlider
    {
      HealthBar,
      StaminaBar,
      None,
    }

    public WhatIsSlider whatIsSlider;

    public Slider mainSlide,WhiteSlide;

    float TimerNeeded = 1.5f;

    public bool canRunFunction;

    private void Start() 
    {

        animator = GetComponent<Animator>();

        canRunFunction = true;

        SwitchWhiteState();
   
    }

    void SwitchWhiteState()
    {

      switch(whatIsSlider)
      {
         case WhatIsSlider.HealthBar :
   
         WhiteSlide.maxValue = ItemCounter.Instance.MaxHealth;
   
         WhiteSlide.value = ItemCounter.Instance.MaxHealth;
    
         break;


         case WhatIsSlider.StaminaBar :

         WhiteSlide.maxValue = ItemCounter.Instance.MaxStamina;
   
         WhiteSlide.value = ItemCounter.Instance.MaxStamina;

         break;

         case WhatIsSlider.None :

         DisableFunction();

         break;


         
      }

    }
    
    private void Update() {
    
      if(canRunFunction)
      {
         if(WhiteSlide.value != mainSlide.value)
         {

           WhiteSlide.value = Mathf.Lerp(WhiteSlide.value, mainSlide.value, 0.1f);

         } 
    
      }

    }


    public void DisableFunction()
   {
      canRunFunction = false;
   }


    public void TriggerShake()
    {

      animator.SetTrigger("ShakeSlider");


    }


}

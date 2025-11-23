using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthslider : MonoBehaviour
{
    public Slider slider;
    public Image fill;


    public void setHealthMax(float MaxHealth1)
    {
         slider.maxValue = MaxHealth1;
         slider.value = MaxHealth1;
    }


    // Update is called once per frame
    void Update()
    {
          float currentBarFill = Mathf.Lerp(slider.value,ItemCounter.Instance.CurrentHealth,0.2f);

          slider.value = currentBarFill;
    }

    
}

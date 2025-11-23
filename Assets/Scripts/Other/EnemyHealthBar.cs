using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public GameObject parent;

    public void setHealthMax(float MaxHealth1)
    {
         slider.maxValue = MaxHealth1;
         slider.value = MaxHealth1;
 
    }


    // Update is called once per frame
    void Update()
    {
          float currentBarFill = Mathf.Lerp(slider.value,gameObject.GetComponentInParent<EnemyTakeDamage>().currenthealth,4.3f * Time.deltaTime);

          slider.value = currentBarFill;

          if(parent.transform.localScale != Vector3.one)
          {
            slider.direction = Slider.Direction.RightToLeft;
          }
          else
          {
            slider.direction = Slider.Direction.LeftToRight;
          }


    }
}

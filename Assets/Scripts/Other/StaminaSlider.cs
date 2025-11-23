using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StaminaSlider : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public TMP_Text stmText;

    public void Start()
    {
         slider.maxValue = ItemCounter.Instance.MaxStamina;
         slider.value = ItemCounter.Instance.MaxStamina;
    }


    // Update is called once per frame
    void Update()
    {
          float currentBarFill = Mathf.Lerp(slider.value,ItemCounter.Instance.stamina,0.2f );

          slider.value = currentBarFill;

          stmText.text = "ST : " + (int)ItemCounter.Instance.stamina + "/" + ItemCounter.Instance.MaxStamina;

    }
}

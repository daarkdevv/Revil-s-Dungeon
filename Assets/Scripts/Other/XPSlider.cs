using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class XPSlider : MonoBehaviour
{
    public  Slider slider;
    public Image fill;

    public  TMP_Text XpText;

    public static XPSlider instance;
    public void Start()
    {
         slider.maxValue = ItemCounter.Instance.XPLevels[ItemCounter.Instance.currentLevel];
         slider.value = ItemCounter.Instance.currentXp;

         instance = this;
    }


    // Update is called once per frame
    void Update()
    {
          float currentBarFill = Mathf.Lerp(slider.value,ItemCounter.Instance.currentXp,2f * Time.deltaTime);

          slider.value = currentBarFill;

          XpText.text = "XP : " + ItemCounter.Instance.currentXp + "/" + ItemCounter.Instance.XPLevels[ItemCounter.Instance.currentLevel];
         
    }
}

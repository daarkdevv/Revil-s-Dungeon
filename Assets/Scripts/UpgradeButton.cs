using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeButton : MonoBehaviour
{
    public Image OriginalImage;
    public Sprite unHightlightedButton;
    // Start is called before the first frame update
    void Start()
    {
        OriginalImage = GetComponent<Image>();
        
        if(ItemCounter.Instance.attributePoints < 1)
        {
            gameObject.GetComponent<Image>().sprite = unHightlightedButton; // unhighlitedbutton is a sprite variable
        } 
    }

    // Update is called once per frame
    private void OnEnable() {

        if(ItemCounter.Instance.attributePoints >= 1)
        {
          gameObject.GetComponent<Image>().sprite = OriginalImage.sprite; 
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = unHightlightedButton;
            
        }

    }


    void AttackAtrIncrease()
    {

    }

    void IntellgenceAtrIncrease()
    {
        
    }

    void AgilityAtrIncrease()
    {

    }
}

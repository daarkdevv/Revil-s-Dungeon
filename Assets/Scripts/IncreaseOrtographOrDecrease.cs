using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IncreaseOrtographOrDecrease : MonoBehaviour
{ 
    [SerializeField]
    private CinemachineVirtualCamera CmMap;
    public int OrtoGraphState;
    public IncreaseOrtographOrDecrease OtherInc; 
    public bool CanIncrease;
    // Start is called before the first frame update
    void Start()
    {
        OrtoGraphState = 3;
    }

    // Update is called once per frame

   public void SwitchOrtoGraph()
    {
        switch (CanIncrease)
        {
            
          case false :
          
          if(OrtoGraphState > 1)
          {
            OrtoGraphState--;
          } 

          break;

          case true :
          
          if(OrtoGraphState < 4)
          {
            OrtoGraphState++;
          }

          break;
          

        }

        OtherInc.OrtoGraphState = OrtoGraphState; 

       
       switch (OrtoGraphState)
       {

        case 1 :
         
        CmMap.m_Lens.OrthographicSize = 7;

        break;

        case 2 :

        CmMap.m_Lens.OrthographicSize = 10;

        break;

        case 3 : 

        CmMap.m_Lens.OrthographicSize = 16;

        break;

        case 4 :

        CmMap.m_Lens.OrthographicSize = 22; 

        break;

  
   

       }

    } 
}

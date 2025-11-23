using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMiniMapState : MonoBehaviour
{
  public DragCamera dragCamera;
    
  private void OnDisable() 
  {
    
    dragCamera.shouldResetCamera = false;

    dragCamera.SetMode();
    
  }

  private void OnEnable() 
  {
    
    dragCamera.shouldResetCamera = true;

    dragCamera.SetMode();

  }

}

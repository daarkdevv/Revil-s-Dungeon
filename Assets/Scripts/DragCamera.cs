using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DragCamera : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private Vector3 mouseStartPosition;

    [SerializeField]
    private Transform mapBounds;

    private CinemachineVirtualCamera virtualCamera;

    [HideInInspector]
    public bool shouldResetCamera;
    
    // Update is called once per frame

    private void Start() {

        virtualCamera = GetComponent<CinemachineVirtualCamera>();

    }
    void Update()
    {

        HandleCameraPan();

        
    }
    

    void HandleCameraPan()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);

        if(Input.GetMouseButtonDown(0))
        {

            mouseStartPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

 
        }

        if(Input.GetMouseButton(0))
        {

          if(Input.mousePosition.magnitude > 20f)
          {

            Vector3 panOffset = mouseStartPosition - mainCamera.ScreenToWorldPoint(Input.mousePosition);

            mainCamera.transform.position += new Vector3((int)panOffset.x, (int)panOffset.y);


          }
          



        }
    }

   public void SetMode()
    {  
        if(virtualCamera == null)
        {

            return;
            
        } 



        if(shouldResetCamera)
        {

            virtualCamera.m_Follow = mainCamera.transform;
            
        }


        else
        {
            if(mapBounds != null)
            {

                virtualCamera.transform.position = mapBounds.transform.position;

                virtualCamera.m_Follow = mapBounds.transform;

            }     
            
            
        }
    }

    public void SetPositionToIcon()
    {

         if(mapBounds != null)
         {

            mainCamera.transform.position = mapBounds.transform.position;

         }

    }
}

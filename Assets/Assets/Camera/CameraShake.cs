using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public  CinemachineVirtualCamera Vcamera;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Vcamera = GetComponent<CinemachineVirtualCamera>();
        Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

 public IEnumerator StartcameraShaking()
    {
      Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Random.Range(0.5f,1.6f);
      yield return new WaitForSeconds(player.GetComponent<PlayerAttack>().attacktimer);
      Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
       
    }

   public IEnumerator startDashShaking()
   {

      Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Random.Range(0.3f,1.1f);
      yield return new WaitForSeconds(player.GetComponent<PlayerMovement>().DashTimer);
      Vcamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

   } 
}

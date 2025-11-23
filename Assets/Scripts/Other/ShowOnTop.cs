using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnTop : MonoBehaviour
{
    private MeshRenderer mrender;
    // Start is called before the first frame update
    void Start()
    {
        mrender = GetComponent<MeshRenderer>();
        mrender.sortingLayerName = "FontLayer";
        mrender.sortingOrder = 55;
    }

    void destroyafterfinish1()
    {

         gameObject.transform.parent.GetComponent<destroyafterfinish>().DestroyMe();
         
    

    }
}

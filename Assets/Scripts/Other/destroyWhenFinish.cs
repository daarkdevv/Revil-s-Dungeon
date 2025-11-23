using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenFinish : MonoBehaviour
{

    void isFinish()
    {
        Destroy(gameObject);
    }
}

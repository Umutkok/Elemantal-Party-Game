using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSingleton : MonoBehaviour
{
    public static CanvasSingleton instance;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

}

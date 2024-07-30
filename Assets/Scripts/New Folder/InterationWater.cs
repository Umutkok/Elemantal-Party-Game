using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationWater : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fire") || other.CompareTag("Air"))
        {
            Debug.Log("umut");
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }
    }
}

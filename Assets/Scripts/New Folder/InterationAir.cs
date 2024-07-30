using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationAir : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Water") || other.CompareTag("Fire"))
        {
            Debug.Log("umut");
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }
    }
}

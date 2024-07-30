using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Interation : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void OnTriggerEnter(Collider other)
    {

        if ( other.CompareTag("Water") || other.CompareTag("Air"))
        {
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }
    }

    
}

using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();      
        audioSource.Play();
    }
}
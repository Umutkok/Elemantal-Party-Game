using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Animator AnimButton;
    public List<string> ButtonTags = new List<string> { "Fire", "Player", "Water", "Air","Poison" };
    public void OnTriggerEnter(Collider other)
    {
        if (ButtonTags.Contains(other.tag))
        {
            Debug.Log("yess");
            AnimButton.SetBool("Clouds", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ElementChangeAir : NetworkBehaviour
{
    public MeshRenderer Renderer;
    [SerializeField] Color32 PosionColor = new Color32(1, 1, 1, 1);
    public string newTag = "Poisoned";

    public GameObject gameOverCanvas;

    private void Start()
    {
        Renderer = GetComponent<MeshRenderer>();
    }

    // RPC metodu
    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc()
    {
        ChangeColorClientRpc();
    }

    // Client RPC metodu
    [ClientRpc]
    private void ChangeColorClientRpc()
    {
        GetComponent<Renderer>().material.color = PosionColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1111");
        if (other.CompareTag("Fire") || other.CompareTag("Water"))
        {
            Time.timeScale = 0f;
            gameOverCanvas.SetActive(true);
        }

        if (other.CompareTag("Poison"))
        {
            Debug.Log("2222");
            if (IsServer)
            {
                ChangeColorClientRpc();
                gameObject.tag = newTag;
            }
            else
            {
                ChangeColorServerRpc();
                gameObject.tag = newTag;
            }
        }

        if (gameObject.tag == newTag)
        {
            if (other.CompareTag("Water") || other.CompareTag("Air") || other.CompareTag("Fire"))
            {
                Time.timeScale = 0f;
                gameOverCanvas.SetActive(true);
            }
        }
    }
}

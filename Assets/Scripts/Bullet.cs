using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != BallController.Singleton)
            return;

        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BallController : NetworkBehaviour
{
    public float speed = 5f;
    public float JumpForce = 500f;
    public float gravity = 2f;
    private Rigidbody Rb;
    [HideInInspector] public Transform camTransform;
    public static BallController Singleton;

    public LayerMask groundLayer; 
    private bool isGrounded = true;

    public GameOverManager gameOverManager;
    private AudioSource audioSource;
    
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
        audioSource = GetComponent<AudioSource>();
    }


    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer) Singleton = this;

        base.OnNetworkSpawn();
    }

    private void Update()
    {
        if (IsOwner)
        {
            BasicPlayerMovement();
        }

    }
    
    private void BasicPlayerMovement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Hesaplama
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0f; //  forward direction stays horizontal
        right.y = 0f; //  right direction stays horizontal .design pattern
        forward.Normalize();
        right.Normalize();
        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;

        if (moveDirection.magnitude > 0)
        {
            Rb.AddForce(moveDirection.normalized * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
            //audioSource.Play();
        }

        if (!IsGrounded())
        {
            Rb.AddForce(Vector3.down * gravity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true; 
        }


    }
    [ServerRpc(RequireOwnership = true)]
    public void DeathCanvasServerRPC()
    {
        DeathCanvasClientRPC();

    }

    [ClientRpc]
    void DeathCanvasClientRPC()
    {

        if (!IsHost) Time.timeScale = 0f;
        CanvasSingleton.instance.gameObject.SetActive(true);

    }

    //void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Zehirr"))
    //    {
    //        Time.timeScale = 0f;
    //        gameOverCanvas.SetActive(true);
    //    }
    //}

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
    

    public void Die()
    {
        if (IsServer)
        {
            gameOverManager.PlayerDied();
        }
    }

}


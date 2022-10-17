using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private Vector2 m_inputVector = Vector2.zero, m_movementVector = Vector2.zero;
    private Rigidbody2D m_Rigidbody2D;
    private Transform m_transform;
    [SerializeField] private float m_speed = 100f;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    private void Start()
    {
        m_playerInput.actions["Movement"].performed += Movement_Performed;
        m_playerInput.actions["LeftMouse"].performed += LeftMouse_Performed;
    }

    private void Update()
    {
        if(isServer && m_transform.position.y > 5)
        {
            TooHigh();
        }

        if (!isLocalPlayer)
        {
            return;
        }

        m_movementVector = m_inputVector * m_speed;
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.velocity = m_movementVector * Time.deltaTime;
    }

    private void LeftMouse_Performed(InputAction.CallbackContext obj)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Debug.Log("Sending Hola to Server!");
        Hola();
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_inputVector = context.ReadValue<Vector2>();
    }

    // Can be called from a client or server, and will only be executed on the server.
    // The server is the only one who will Log "Recieved Hola from client!".
    [Command]
    public void Hola()
    {
        Debug.Log("Recieved Hola from Client!");
    }

    // Called on the server, but run on the client.
    [ClientRpc]
    public void TooHigh()
    {
        Debug.Log("Too high!");
    }
}

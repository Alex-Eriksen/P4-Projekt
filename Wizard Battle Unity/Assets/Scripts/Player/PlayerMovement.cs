using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private Vector2 m_inputVector = Vector2.zero, m_movementVector = Vector2.zero;
    private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private float m_speed = 100f;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["Movement"].performed += Movement_Performed;
    }

    private void Update()
    {
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

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        m_inputVector = context.ReadValue<Vector2>();
    }

    // [Command]
    // Can be called from a client or server, and will only be executed on the server.
    // The server is the only one who will Log "Recieved Hola from client!".

    // [TargetRpc]
    // Called on the server, but run on the TARGET client, I.E. a specific client.
    // This can be implicit, meaning that if a client calls a command and the server then
    // calls a TargetRpc the target client will be the caller of the command, instead of specifying
    // the client when calling the TargetRpc.

    // [ClientRpc]
    // Called on the server, but run on all clients.
    // Do not use in the update loop, its bad practice.

    // [SyncVar(hook = "OnHolaCountChanged")]
    // SyncVar ONLY from server to client.
    // Never from client to server, the server is the source of truth for SyncVar's.
    // A hook can be attached to a SyncVar that will be called whenever the SyncVar changes.
    // This will however only be called if the change came from the server, as SyncVars ONLY updates FROM the server.
}

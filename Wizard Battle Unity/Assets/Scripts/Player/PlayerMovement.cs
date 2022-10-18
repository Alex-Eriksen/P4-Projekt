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
    // private Transform m_transform;
    [SerializeField] private float m_speed = 100f;

    // SyncVar ONLY from server to client.
    // Never from client to server, the server is the source of truth for SyncVar's.
    // A hook can be attached to a SyncVar that will be called whenever the SyncVar changes.
    // This will however only be called if the change came from the server, as SyncVars ONLY updates FROM the server.
    [SyncVar(hook = "OnHolaCountChanged")]
    private int m_holaCount = 0;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        // m_transform = transform;
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["Movement"].performed += Movement_Performed;
        m_playerInput.actions["LeftMouse"].performed += LeftMouse_Performed;
    }

    private void Update()
    {
        // Not good to run in the update loop.
        //if(isServer && m_transform.position.y > 5)
        //{
        //    TooHigh();
        //}

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
        Debug.Log("Sending Hola to Server!");
        Hola();
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        m_inputVector = context.ReadValue<Vector2>();
    }

    // Can be called from a client or server, and will only be executed on the server.
    // The server is the only one who will Log "Recieved Hola from client!".
    [Command]
    public void Hola()
    {
        Debug.Log("Recieved Hola from Client!");
        m_holaCount += 1;
        ReplyHola();
    }

    // Called on the server, but run on the TARGET client, I.E. a specific client.
    // This can be implicit, meaning that if a client calls a command and the server then
    // calls a TargetRpc the target client will be the caller of the command, instead of specifying
    // the client when calling the TargetRpc.
    [TargetRpc]
    public void ReplyHola()
    {
        Debug.Log("Recieved Hola from server!");
    }

    // Called on the server, but run on all clients.
    // Do not use in the update loop, its bad practice.
    [ClientRpc]
    public void TooHigh()
    {
        Debug.Log("Too high!");
    }

    public void OnHolaCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"We had {oldCount} holas, but now we have {newCount} holas!");
    }
}

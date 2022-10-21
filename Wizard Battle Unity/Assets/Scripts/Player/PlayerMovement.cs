using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Linq;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerConnection m_playerConnection;
    private PlayerInput m_playerInput;
    private Vector2 m_inputVector = Vector2.zero, m_movementVector = Vector2.zero;
    private Rigidbody2D m_rigidbody2D;
    private Animator m_animator;
    [SerializeField] private float m_speed = 100f;

    public override void OnStartAuthority()
    {
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        m_playerInput = m_playerConnection.PlayerInput;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();

        SetInput();
    }

    private void SetInput()
    {
        m_playerInput.actions["Movement"].performed += Movement_Performed;
    }

    private void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        m_movementVector = m_inputVector * m_speed;
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }

        m_rigidbody2D.velocity = m_movementVector * Time.deltaTime;
    }

    /// <summary>
    /// Method listening on the Movement performed event.
    /// Responsible for reading the movement input from the player.
    /// </summary>
    /// <param name="context"></param>
    private void Movement_Performed(InputAction.CallbackContext context)
    {
        m_inputVector = context.ReadValue<Vector2>();
        m_animator.SetBool("Moving", !(m_inputVector == Vector2.zero));
        #region May or may not use...
        //Vector2 newMovementVector = m_inputVector * m_speed;
        //CmdValidatePlayerMovement(m_movementVector, newMovementVector);
        //m_movementVector = newMovementVector;
        #endregion
    }

    #region May or may not use...
    //[Command]
    //private void CmdValidatePlayerMovement(Vector2 oldVelocity, Vector2 newVelocity)
    //{

    //}

    //[ClientRpc]
    //private void RpcPlayerPositionReset(Vector2 newVelocity)
    //{

    //}
    #endregion

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

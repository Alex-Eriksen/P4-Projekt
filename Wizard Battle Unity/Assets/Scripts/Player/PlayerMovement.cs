using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Linq;

public class PlayerMovement : NetworkBehaviour
{
    [SyncVar] public float speedMultiplier = 1f;
    private PlayerConnection m_playerConnection;
    private PlayerInput m_playerInput;
    private Vector2 m_inputVector = Vector2.zero, m_movementVector = Vector2.zero;
    private Vector2 m_validSavedPosition, m_validSavedVelocity;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;
    private Animator m_animator;
    [SerializeField] private float m_speed = 100f;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();
        m_transform = transform;
    }

    public override void OnStartAuthority()
    {
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        m_playerInput = m_playerConnection.PlayerInput;

        SetInput();
    }

    public override void OnStartServer()
    {
        m_validSavedPosition = m_transform.position;
        m_validSavedVelocity = m_rigidbody2D.velocity;
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

        m_movementVector = m_speed * speedMultiplier * m_inputVector;
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }

        ClientMovement();
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
    }

    /// <summary>
    /// <para>Called every physics update on the client.</para>
    /// </summary>
    private void ClientMovement()
    {
        m_rigidbody2D.velocity = m_movementVector * Time.deltaTime;
        Cmd_ValidateVelocity(m_rigidbody2D.velocity);
        Cmd_ValidatePosition(m_transform.position);
    }

    /// <summary>
    /// <para>Validates the new position sent by the client.</para>
    /// <br>If the new position was valid it stores the position.</br>
    /// <br>If the new position was invalid it will set the clients position to the last saved valid position.</br>
    /// </summary>
    /// <param name="newPosition"></param>
    [Command]
    private void Cmd_ValidatePosition(Vector2 newPosition)
    {
        if(m_validSavedPosition == null)
        {
            Debug.LogError($"PlayerMovement::Cmd_ValidatePosition -> Failed: m_validSavedPosition is NULL");
            return;
        }
        
        if(Vector2.Distance(newPosition, m_validSavedPosition) > WizardNetworkManager.PositionThreshold)
        {
            Rpc_OverrideClientVelocity(m_validSavedVelocity);
            Rpc_OverrideClientPosition(m_validSavedPosition);
            return;
        }

        m_validSavedPosition = newPosition;
    }

    /// <summary>
    /// <para>Validates the new velocity sent by the client.</para>
    /// <br>If the new velocity was valid the new velocity is saved.</br>
    /// <br>If the new velocity was invalid it will set the client velocity to the last valid saved velocity.</br>
    /// </summary>
    /// <param name="newVelocity"></param>
    [Command]
    private void Cmd_ValidateVelocity(Vector2 newVelocity)
    {
        if(m_validSavedVelocity == null)
        {
            Debug.LogError($"PlayerMovement::Cmd_ValidateVelocity -> Failed: m_validSavedPosition is NULL");
            return;
        }

        if(newVelocity.magnitude > WizardNetworkManager.VelocityThreshold)
        {
            Rpc_OverrideClientVelocity(m_validSavedVelocity);
            Rpc_OverrideClientPosition(m_validSavedPosition);
            return;
        }

        m_validSavedVelocity = newVelocity;
    }

    /// <summary>
    /// <para>Overrides the velocity and position on the client.</para>
    /// <br>This is called from the server on the client when the new position was invalid.</br>
    /// </summary>
    /// <param name="validSavedPosition"></param>
    [TargetRpc]
    private void Rpc_OverrideClientPosition(Vector2 validSavedPosition)
    {
        m_transform.position = validSavedPosition;
    }

    /// <summary>
    /// <para>Overrides the velocity and position on the client.</para>
    /// <br>This is called from the server on the client when the new velocity was invalid.</br>
    /// </summary>
    /// <param name="validSavedVelocity"></param>
    [TargetRpc]
    private void Rpc_OverrideClientVelocity(Vector2 validSavedVelocity)
    {
        m_rigidbody2D.velocity = validSavedVelocity;
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

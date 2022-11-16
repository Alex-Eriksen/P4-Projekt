using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Linq;
using System;

public class PlayerMovement : NetworkBehaviour
{
    public event Action InterruptMovementCallback;

    [SyncVar] public float speedMultiplier = 1f;
    private float m_maxSpeed = 0f;
    private PlayerConnection m_playerConnection;
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;
    private Vector2 m_inputVector = Vector2.zero, m_movementVector = Vector2.zero;
    private Vector2 m_validSavedPosition, m_validSavedVelocity;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;
    private Animator m_animator;
    [SerializeField] private float m_speed = 100f;
    private bool m_interrupted = false;

    #region Setup
    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponentInChildren<Animator>();
        m_playerEntity = GetComponent<PlayerEntity>();
        m_transform = transform;
    }

    public override void OnStartAuthority()
    {
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        m_playerInput = m_playerConnection.PlayerInput;

        Cmd_ServerSetup(m_playerConnection.netId);

        SetInput();
    }

    [Command]
    private void Cmd_ServerSetup(uint netId)
    {
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.netId == netId).Single();
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
    #endregion

    #region Update Loops
    private void Update()
    {
        m_maxSpeed = (WizardNetworkManager.VelocityThreshold - WizardNetworkManager.VelocityError) * speedMultiplier;

        if (!hasAuthority)
        {
            return;
        }

        m_movementVector = m_speed * m_inputVector;
    }

    private void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }

        ClientMovement();
    }

    private void LateUpdate()
    {
        if (!isServer)
        {
            return;
        }

        SC_ValidatePosition(m_transform.position);
        SC_ValidateVelocity(m_rigidbody2D.velocity);
    }
    #endregion

    #region Client Input
    /// <summary>
    /// Method listening on the Movement performed event.
    /// Responsible for reading the movement input from the player.
    /// </summary>
    /// <param name="context"></param>
    private void Movement_Performed(InputAction.CallbackContext context)
    {
        m_inputVector = context.ReadValue<Vector2>();
        if (m_playerEntity.ContainsStatusEffect(StatusEffectType.Stun))
        {
            m_inputVector = Vector2.zero;
        }
        m_animator.SetBool("Moving", !(m_inputVector == Vector2.zero));
    }

    /// <summary>
    /// <para>Called every physics update on the client.</para>
    /// </summary>
    private void ClientMovement()
    {
        if (m_interrupted)
        {
            return;
        }
        m_rigidbody2D.velocity = m_movementVector * Time.fixedDeltaTime;
    }
    #endregion

    #region Movement Validation
    /// <summary>
    /// <para>Validates the new position sent by the client.</para>
    /// <br>If the new position was valid it stores the position.</br>
    /// <br>If the new position was invalid it will set the clients position to the last saved valid position.</br>
    /// </summary>
    /// <param name="newPosition"></param>
    [ServerCallback]
    private void SC_ValidatePosition(Vector2 newPosition)
    {
        if(m_validSavedPosition == null)
        {
            Debug.LogError($"[{System.DateTime.Now}] PlayerMovement::SC_ValidatePosition -> Failed: m_validSavedPosition is NULL");
            return;
        }
        
        if(Vector2.Distance(newPosition, m_validSavedPosition) > WizardNetworkManager.PositionThreshold)
        {
            Debug.LogWarning($"\n[{System.DateTime.Now}] PlayerMovement::SC_ValidatePosition() - Saved Position: {m_validSavedPosition}");
            Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_ValidatePosition() - New Position: {newPosition}");
            Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_ValidatePosition() - Invalid Distance: {Vector2.Distance(newPosition, m_validSavedPosition)}\n");
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
    [ServerCallback]
    private void SC_ValidateVelocity(Vector2 newVelocity)
    {
        if(m_validSavedVelocity == null)
        {
            Debug.LogError($"[{System.DateTime.Now}] PlayerMovement::SC_ValidateVelocity -> Failed: m_validSavedVelocity is NULL");
            return;
        }

        if(newVelocity.magnitude > m_validSavedVelocity.magnitude + WizardNetworkManager.VelocityThreshold)
        {
            Debug.LogWarning($"\n[{System.DateTime.Now}] PlayerMovement::SC_ValidateVelocity() - Threshold: {WizardNetworkManager.VelocityThreshold}");
            Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_ValidateVelocity() - Valid Velocity Magnitude: {m_validSavedVelocity.magnitude}");
            Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_ValidateVelocity() - Invalid Velocity Magnitude: {newVelocity.magnitude}");
            Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_ValidateVelocity() - Discarded Velocity Magnitude: {newVelocity.magnitude}>{m_validSavedVelocity.magnitude + WizardNetworkManager.VelocityThreshold}\n");
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
    [ClientRpc]
    public void Rpc_OverrideClientPosition(Vector2 validSavedPosition)
    {
        m_transform.position = validSavedPosition;
    }

    /// <summary>
    /// <para>Overrides the velocity and position on the client.</para>
    /// <br>This is called from the server on the client when the new velocity was invalid.</br>
    /// </summary>
    /// <param name="validSavedVelocity"></param>
    [ClientRpc]
    public void Rpc_OverrideClientVelocity(Vector2 validSavedVelocity)
    {
        m_rigidbody2D.velocity = validSavedVelocity;
    }

    [ClientRpc]
    public void Rpc_OverridePosition(Vector2 validSavedPosition, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_transform.position = validSavedPosition;
    }

    /// <summary>
    /// Overrides the saved position the server has on the player.
    /// This is used to override the position for spells like teleportation that moves the player more than the allowed distance.
    /// </summary>
    /// <param name="newPosition"></param>
    /// <param name="reason"></param>
    [ServerCallback]
    public void SC_OverrideCurrentSavedPosition(Vector2 newPosition, string reason, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_validSavedPosition = newPosition;
        m_transform.position = newPosition;
        Rpc_OverridePosition(newPosition, duration);
        Debug.LogWarning($"[{System.DateTime.Now}] PlayerMovement::SC_OverrideCurrentSavedPosition -> Current saved position was overriden for {m_playerConnection.PlayerName}: {reason}");
    }

    [ClientRpc]
    public void Rpc_OverrideVelocity(Vector2 newVelocity, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_rigidbody2D.velocity = newVelocity;
    }

    [ServerCallback]
    public void SC_OverrideCurrentSavedVelocity(Vector2 newVelocity, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_validSavedVelocity = newVelocity;
        m_rigidbody2D.velocity = newVelocity;
        Rpc_OverrideVelocity(newVelocity, duration);
    }
    #endregion

    #region Apply Force
    [ClientRpc]
    public void Rpc_AddForceAtPosition(Vector2 force, Vector2 position, ForceMode2D forceMode, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_rigidbody2D.AddForceAtPosition(force, position, forceMode);
    }

    [ServerCallback]
    public void SC_AddForceAtPosition(Vector2 force, Vector2 position, ForceMode2D forceMode, float duration)
    {
        StartCoroutine(InterruptMovement(duration));
        m_validSavedVelocity += force * 2;
        m_rigidbody2D.AddForceAtPosition(force, position, forceMode);
        Rpc_AddForceAtPosition(force, position, forceMode, duration);
    }
    #endregion

    public IEnumerator InterruptMovement(float duration)
    {
        m_interrupted = true;
        yield return new WaitForSeconds(duration);
        m_interrupted = false;
        InterruptMovementCallback?.Invoke();
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

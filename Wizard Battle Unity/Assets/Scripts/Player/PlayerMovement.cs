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

    public void Movement_Performed(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_inputVector = context.ReadValue<Vector2>();
    }
}

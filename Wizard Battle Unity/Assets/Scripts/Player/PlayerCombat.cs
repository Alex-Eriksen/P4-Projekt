using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerCombat : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerEntity = GetComponent<PlayerEntity>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["LeftMouse"].started += LeftMouse_Started;
        m_playerInput.actions["RightMouse"].started += RightMouse_Started;
    }

    private void RightMouse_Started(InputAction.CallbackContext obj)
    {

    }

    private void LeftMouse_Started(InputAction.CallbackContext obj)
    {

    }
}

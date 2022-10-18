using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class SpellCaster : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private Vector2 m_mousePosition = Vector2.zero, m_aimDirection;
    private Transform m_transform;

    public Spellbook Spellbook { get { return m_spellbook; } }
    private Spellbook m_spellbook;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_transform = transform;
        m_spellbook = FindObjectOfType<Spellbook>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["MousePosition"].performed += MousePosition_Performed;
        m_playerInput.actions["Spellbook"].started += Spellbook_Started;
        m_playerInput.actions["Spellbook"].canceled += Spellbook_Canceled;
    }

    private void Spellbook_Started(InputAction.CallbackContext obj)
    {
        m_spellbook.OpenSpellbook();
    }   
    
    private void Spellbook_Canceled(InputAction.CallbackContext obj)
    {
        m_spellbook.CloseSpellbook();
    }

    private void MousePosition_Performed(InputAction.CallbackContext obj)
    {
        m_mousePosition = obj.ReadValue<Vector2>();
        m_aimDirection = Camera.main.ScreenToWorldPoint(m_mousePosition) - m_transform.position;
    }
}

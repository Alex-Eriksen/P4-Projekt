using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Testing
{
    public class Test_PlayerMovement : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private Rigidbody2D m_rigidbody;
        private Transform m_transform;
        private Vector2 m_moveInput, m_velocityVector, m_mousePosition;
        [SerializeField] private float m_dashSpeed, m_dashDuration, m_movementSpeed;
        [SerializeField] private float m_currentSpeed = 0f;

        private bool m_interrupted = false;

        private void Awake()
        {
            m_playerInput = GetComponent<PlayerInput>();
            m_rigidbody = GetComponent<Rigidbody2D>();
            m_transform = transform;
        }

        private void Start()
        {
            m_playerInput.actions["Movement"].performed += Movement_performed;
            m_playerInput.actions["Utility"].started += Utility_started;
            m_playerInput.actions["MousePosition"].performed += MousePosition_performed;
        }

        private void Update()
        {
            RotateToMouse();

            m_velocityVector = m_movementSpeed * m_moveInput;
            m_currentSpeed = m_rigidbody.velocity.magnitude;
        }

        private void FixedUpdate()
        {
            if (m_interrupted)
            {
                return;
            }

            m_rigidbody.velocity = m_velocityVector * Time.fixedDeltaTime;

            if(m_rigidbody.velocity.magnitude <= 0.1)
            {
                m_rigidbody.velocity = Vector2.zero;
            }
        }

        private void MousePosition_performed(InputAction.CallbackContext obj)
        {
            m_mousePosition = obj.ReadValue<Vector2>();
        }

        private void Utility_started(InputAction.CallbackContext obj)
        {
            if (m_interrupted)
            {
                return;
            }
            StartCoroutine(Dash());
        }

        private void Movement_performed(InputAction.CallbackContext obj)
        {
            m_moveInput = obj.ReadValue<Vector2>();
        }

        private void RotateToMouse()
        {
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(m_mousePosition);

            lookPos -= m_transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
            m_transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private IEnumerator Dash()
        {
            m_interrupted = true;
            m_rigidbody.velocity = m_transform.up * m_dashSpeed;
            yield return new WaitForSeconds(m_dashDuration);
            m_interrupted = false;
        }
    }
}

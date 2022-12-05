using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public PlayerConnection PlayerConnection { get; private set; }
    private Transform m_playerTransform;
    private Transform m_transform;
    [SerializeField, Range(1f, 10f)] private float m_smoothSpeed = 5f;

    private void Awake()
    {
        m_transform = transform;
    }

    private void Start()
    {
        StartCoroutine(GetPlayerConnectionObjectBuffer());
    }

    private IEnumerator GetPlayerConnectionObjectBuffer()
    {
        try
        {
            PlayerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        }
        catch
        {
            PlayerConnection = null;
        }
        yield return new WaitForEndOfFrame();
        if (PlayerConnection == null)
        {
            StartCoroutine(GetPlayerConnectionObjectBuffer());
        }
        else
        {
            m_playerTransform = PlayerConnection.wizardIdentity.transform;
        }
    }

    private void FixedUpdate()
    {
        if(m_playerTransform == null)
        {
            return;
        }
        Vector3 desiredPosition = new Vector3(m_playerTransform.position.x, m_playerTransform.position.y, m_transform.position.z);
        m_transform.position = Vector3.Lerp(m_transform.position, desiredPosition, m_smoothSpeed * Time.fixedDeltaTime);
    }
}

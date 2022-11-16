using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class SlowStatus : Status
{
    [SerializeField, Range(0, 100)] private int m_slowPercent = 30;
    private PlayerMovement m_targetMovement;

    protected override void OnSetup()
    {
        m_targetMovement = target.GetComponent<PlayerMovement>();
    }

    protected override void OnServerSetup()
    {
        m_targetMovement.speedMultiplier -= m_slowPercent / 100f;
    }

    private void OnDestroy()
    {
        if (isServer)
        {
            m_targetMovement.speedMultiplier += m_slowPercent / 100f;
        }
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEffectsManager : NetworkBehaviour
{
    public static GameEffectsManager Instance { get { return m_instance; } }
    private static GameEffectsManager m_instance;

    [Header("Effect Prefabs")]
    [SerializeField] private GameObject m_numberEffectPrefab;

    private void Awake()
    {
        if(m_instance != null && m_instance != this)
        {
            Debug.LogError($"There was 2 instances of {GetType()} - Deleting the new one.");
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdCreateNumberEffect(NumberEffectData data)
    {
        RpcCreateNumberEffect(data);
    }

    [ClientRpc]
    private void RpcCreateNumberEffect(NumberEffectData data)
    {
        Instantiate(m_numberEffectPrefab).GetComponent<NumberEffect>().data = data;
    }
}
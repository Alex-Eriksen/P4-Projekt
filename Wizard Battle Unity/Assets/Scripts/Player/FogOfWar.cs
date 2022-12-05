using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private LayerMask m_fogOfWarMask;
    [SerializeField] private int m_rayCount = 16;

    private List<FogOfWar> fogOfWars = new List<FogOfWar>();

    private Transform m_transform;

    private void Awake()
    {
        m_transform = transform;
    }

    private void Start()
    {
        fogOfWars.Clear();
        var fows = FindObjectsOfType<FogOfWar>();
        foreach (FogOfWar fow in fows)
        {
            fogOfWars.Add(fow);
        }
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < m_rayCount; i++)
        {
            //Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, m_fogOfWarMask);
            Vector3 direction = Quaternion.Euler(0, 0, i) * transform.up * 2;
            Gizmos.DrawRay(transform.position, direction);
        }
    }

    public void RunFogOfWar()
    {
        foreach (FogOfWar fow in fogOfWars)
        {
            RaycastHit2D[] hits = Physics2D.LinecastAll(m_transform.position, fow.transform.position);
            if(hits.Length > 1)
            {
                
            }
        }
    }
}

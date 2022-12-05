using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] private LayerMask m_fogOfWarMask;
    [SerializeField] private int m_rayCount = 16;

    private Transform m_transform;

    private void Awake()
    {
        m_transform = transform;
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
}

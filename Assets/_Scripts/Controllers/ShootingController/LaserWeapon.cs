using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] private float m_Lenght = 5000.0f;
    [SerializeField] private bool m_IsVisible = true;
    
    private LineRenderer m_LineRenderer;

    void Start () 
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.startWidth = 0.07f;
        m_LineRenderer.endWidth = 0.07f;
    }
    
    void Update () 
    {
        if (!m_IsVisible)
            return;
        
        RaycastHit hit;
        m_LineRenderer.SetPosition(0, transform.position);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
                m_LineRenderer.SetPosition(1, hit.point);
        }
        
        else m_LineRenderer.SetPosition(1, transform.forward * m_Lenght);
    }
}

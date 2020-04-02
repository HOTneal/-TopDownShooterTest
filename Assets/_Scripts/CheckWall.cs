using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CheckWall : MonoBehaviour
{
    [SerializeField] private Transform m_Point1;
    [SerializeField] private Transform m_Point2;
    [SerializeField] private Transform m_Point3;
    [SerializeField] private Transform m_Point4;

    public Vector3 minPos;
    public Transform maxPos;
    
    private RaycastHit m_Hit;
    private Transform m_Transform;

    private void Start()
    {
        m_Transform = transform;
        minPos = m_Transform.position * 3.0f;
    }

    private void Update()
    {
        Debug.DrawRay(m_Transform.position, m_Transform.forward, Color.green);

        if (Physics.Raycast(m_Transform.position, m_Transform.forward, out m_Hit, 5.0f))
        {
            print(m_Hit.point);
            minPos = m_Hit.point;
            StopNearWall();
        }

    }

    private void StopNearWall()
    {
        var x = Mathf.Clamp(m_Transform.position.x, minPos.x + 2.0f, maxPos.position.x);
        m_Transform.position = new Vector3(x, m_Transform.position.y, m_Transform.position.z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Unit m_PlayerUnit;
    
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    private void Update()
    {
        MovePlayer();
        PlayerShoot();
        NextWeapon();
    }

    public Vector2 MovePlayer()
    {
        Vector2 axis;
        var horizontalKeyboard = Input.GetAxis("Horizontal");
        var verticalKeyboard = Input.GetAxis("Vertical");
        
        var horizontalJoystick = m_LinkManager.m_MobilePlayerController.Horizontal();
        var verticalJoystick = m_LinkManager.m_MobilePlayerController.Vertical();
        
        axis.x = horizontalKeyboard != 0 ? horizontalKeyboard : horizontalJoystick;
        axis.y = verticalKeyboard != 0 ? verticalKeyboard : verticalJoystick;
        return axis;
    }

    private void PlayerShoot()
    {
        if (Input.GetMouseButton(0))
            m_PlayerUnit.Shoot(m_PlayerUnit);
        
        if (Input.GetMouseButtonUp(0))
            m_LinkManager.m_EventsManager.EndPlayerShoot(m_PlayerUnit);
    }

    private void NextWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            m_LinkManager.m_EventsManager.NextWeapon();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private LinkManager m_LinkManager;

    private void Start()
    {
        m_LinkManager = LinkManager.Instance;
    }

    private void Update()
    {
        if (m_LinkManager.m_Player == null)
            return;
        
        PlayerShoot();
        NextWeapon();
    }

    public Vector2 MovePlayer()
    {
        Vector2 axis;
        var horizontalKeyboard = Input.GetAxis("Horizontal");
        var verticalKeyboard = Input.GetAxis("Vertical");
        
        var horizontalJoystick = m_LinkManager.MobilePlayerController.Horizontal();
        var verticalJoystick = m_LinkManager.MobilePlayerController.Vertical();
        
        axis.x = horizontalKeyboard != 0 ? horizontalKeyboard : horizontalJoystick;
        axis.y = verticalKeyboard != 0 ? verticalKeyboard : verticalJoystick;
        return axis;
    }

    private void PlayerShoot()
    {
        if (Input.GetMouseButton(0))
            m_LinkManager.m_Player.Shoot(m_LinkManager.m_Player);
    }

    private void NextWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0) 
            m_LinkManager.m_Player.NextWeapon();
    }
}

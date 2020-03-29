using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthbarUnit : MonoBehaviour
{
    public Image m_Helthbar;
    public Text m_Nickname;

    private Unit m_Unit;
    private LinkManager m_LinkManager;
    
    private void Start()
    {
        m_Helthbar = transform.GetChild(3).GetChild(0).GetComponent<Image>();
        m_Nickname = transform.GetChild(3).GetChild(1).GetComponent<Text>();
        m_Unit = GetComponent<Unit>();
        m_LinkManager = LinkManager.Instance;
        
        SetColorBar();
        SetNickname();
    }

    private void SetColorBar()
    {
        m_LinkManager.m_HelthController.SetColorBar(m_Unit);
    }

    private void SetNickname()
    {
        m_Nickname.text = m_Unit.m_Nickname;
    }
}
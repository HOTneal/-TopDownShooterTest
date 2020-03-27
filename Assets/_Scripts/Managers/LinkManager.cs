using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : MonoBehaviour
{
    #region Singltone
    private static LinkManager _instance;
    public static LinkManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public MovementController m_MovementController;
    public EventsManager m_EventsManager;
    public ShootingController m_ShootingController;
    public UIManager m_UIManager;
    public InputController m_InputController;
    public MobileInputController m_MobilePlayerController;
    public BulletController m_BulletController;
}

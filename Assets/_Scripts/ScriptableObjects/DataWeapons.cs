using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CreateNewWeapons")]
public class DataWeapons : ScriptableObject {
    public enum ChoiceTypeShooting
    {
        Automatic,
        Shotgun
    }
    [SerializeField] private int m_IdWeapon;
    [SerializeField] private float m_SpeedShoot;
    [SerializeField] private float m_ReloadTime;
    [SerializeField] private float m_Damage;
    [SerializeField] private int m_QuantityBulletsPerShot;
    [SerializeField] private int m_QuantityBulletsInClip;
    [SerializeField] private int m_QuantityAllBulletsWeapon;
    [SerializeField] private ChoiceTypeShooting m_TypeShooting;
    [SerializeField] private Sprite m_Icon;
    [SerializeField] private AudioClip m_SoundShot;
    [SerializeField] private string m_NameAnim;
    [SerializeField] private bool m_InfinityBullets;

    public int IdWeapon { get { return m_IdWeapon; } }
    public float SpeedShoot { get { return m_SpeedShoot; } }
    public float ReloadTime { get { return m_ReloadTime; } }
    public float Damage { get { return m_Damage; } }
    public int QuantityBulletsPerShot { get { return m_QuantityBulletsPerShot; } }
    public int QuantityBulletsInClip { get { return m_QuantityBulletsInClip; } }
    public int QuantityAllBulletsWeapon { get { return m_QuantityAllBulletsWeapon; } }
    public ChoiceTypeShooting TypeShooting { get { return m_TypeShooting; } }
    public Sprite Icon { get { return m_Icon; } }
    public AudioClip SoundShot { get { return m_SoundShot; } }
    public string NameAnim { get { return m_NameAnim; } }
    public bool InfinityBullets { get { return m_InfinityBullets; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLanguage : MonoBehaviour
{
    [SerializeField] private string m_Language;

    public void Set()
    {
        LocalizationManager.Instance.SetLanguage(m_Language);
    }
}

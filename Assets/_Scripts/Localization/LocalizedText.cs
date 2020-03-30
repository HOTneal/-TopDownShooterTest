using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public Text m_Text;
    public string m_KeyText;

    private LocalizationManager link;
    
    void Start()
    {
        link = LocalizationManager.Instance;
        m_Text = GetComponent<Text>();
        m_KeyText = m_Text.text;
        m_Text.text = link.GetLocalizedValue(m_KeyText);
    }
}
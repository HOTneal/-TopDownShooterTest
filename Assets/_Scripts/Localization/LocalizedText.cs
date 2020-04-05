using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public Text text;
        public string keyText;

        private LocalizationManager m_Link;
    
        void Start()
        {
            m_Link = LocalizationManager.Instance;
            text = GetComponent<Text>();
            keyText = text.text;
            text.text = m_Link.GetLocalizedValue(keyText);
        }
    }
}
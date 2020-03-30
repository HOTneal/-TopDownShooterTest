using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizedText : MonoBehaviour
    {
        public Text Text;
        public string KeyText;

        private LocalizationManager link;
    
        void Start()
        {
            link = LocalizationManager.Instance;
            Text = GetComponent<Text>();
            KeyText = Text.text;
            Text.text = link.GetLocalizedValue(KeyText);
        }
    }
}
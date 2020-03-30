using Localization;
using UnityEngine;

namespace Menu
{
    public class SetLanguage : MonoBehaviour
    {
        [SerializeField] private string m_Language;

        public void Set()
        {
            LocalizationManager.Instance.SetLanguage(m_Language);
        }
    }
}

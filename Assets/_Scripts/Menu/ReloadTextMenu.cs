using Localization;
using UnityEngine;

namespace Menu
{
    public class ReloadTextMenu : MonoBehaviour
    {
        public void ReloadText()
        {
            LocalizationManager.Instance.Reload();
        }
    }
}

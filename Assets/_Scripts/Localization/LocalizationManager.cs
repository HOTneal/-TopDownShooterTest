using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] private LocalizedText[] m_AllText;
    
        public static LocalizationManager Instance;
        [HideInInspector] public List<LocalizedText> listLocalizedText = new List<LocalizedText>();

        private Dictionary<string, string> m_LocalizedText = new Dictionary<string, string>();
        private bool m_IsReady = false;
        private string m_MissingTextString = "Localized text not found";
        private string m_FilePath;

        private void Awake()
        {
            Instance = this;
            SetLanguage("Russian");
            DontDestroyOnLoad(gameObject);
        }

        public void SetLanguage(string language)
        {
            PlayerPrefs.DeleteAll();
            TextAsset languageData = Resources.Load<TextAsset>($"Language/{language}") as TextAsset;

            LoadLocalizedText(language, languageData.ToString());
        }

        public void LoadLocalizedText(string language, string loadKeys)
        {
            PlayerPrefs.SetString("language", $"{language}.json");
            PlayerPrefs.Save();
   
            m_LocalizedText = new Dictionary<string, string>();
            
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(loadKeys);
                
            for (int i = 0; i < loadedData.items.Length; i++)
                m_LocalizedText.Add(loadedData.items[i].key, loadedData.items[i].value);

            m_IsReady = true;
        }

        public string GetLocalizedValue(string key)
        {
            var result = m_MissingTextString;
            if (m_LocalizedText.ContainsKey(key))
                result = m_LocalizedText[key];

            return result;
        }
    
        public void Reload()
        {
            Text currentText;
            for (int i = 0; i < m_AllText.Length; i++)
            {
                currentText = m_AllText[i].GetComponent<Text>();
                currentText.text = GetLocalizedValue(m_AllText[i].keyText);
            }
        }
    }
}
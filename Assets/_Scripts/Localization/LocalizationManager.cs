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
            SetLanguage("English");
        }

        public void SetLanguage(string language)
        {
            PlayerPrefs.DeleteAll();
            string languageData = PlayerPrefs.GetString(language, $"{language}.json");
        
            LoadLocalizedText(languageData);
            DontDestroyOnLoad(gameObject);
        }

        public void LoadLocalizedText(string fileName)
        {
            PlayerPrefs.SetString("language", fileName);
            PlayerPrefs.Save();
        
            m_LocalizedText = new Dictionary<string, string>();

#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        if (reader.error != null) { Debug.Log("error:" + reader.error); }
        filePath = Application.persistentDataPath + fileName;
        File.WriteAllBytes(filePath, reader.bytes);

#else
            m_FilePath = Path.Combine(Application.streamingAssetsPath, fileName);
#endif
            if (File.Exists(m_FilePath))
            {
                string dataAsJson = File.ReadAllText(m_FilePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                    m_LocalizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            else
                Debug.LogError("Cannot find file!");
            
            m_IsReady = true;
        }

        public string GetLocalizedValue(string key)
        {
            string result = m_MissingTextString;
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
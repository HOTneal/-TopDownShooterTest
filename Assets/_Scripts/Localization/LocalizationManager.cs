using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private LocalizedText[] m_AllText;
    
    public static LocalizationManager Instance;
    [HideInInspector] public List<LocalizedText> listLocalizedText = new List<LocalizedText>();

    private Dictionary<string, string> localizedText = new Dictionary<string, string>();
    private bool isReady = false;
    private string missingTextString = "Localized text not found";
    private string filePath;

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
        
        localizedText = new Dictionary<string, string>();

#if UNITY_ANDROID && !UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        if (reader.error != null) { Debug.Log("error:" + reader.error); }
        filePath = Application.persistentDataPath + fileName;
        File.WriteAllBytes(filePath, reader.bytes);

#else
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
#endif

        if (File.Exists(filePath))
        {

            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                
            }
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }
    
    public void Reload()
    {
        Text currentText;
        for (int i = 0; i < m_AllText.Length; i++)
        {
            currentText = m_AllText[i].GetComponent<Text>();
            currentText.text = GetLocalizedValue(m_AllText[i].m_KeyText);
        }
    }
}
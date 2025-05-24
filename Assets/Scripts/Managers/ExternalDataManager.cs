using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExternalDataManager : MonoBehaviour
{
    public StoredData m_StoredData = new StoredData();
    public static ExternalDataManager Instance;
    public TextMeshProUGUI m_TomesText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadFromJson();
    }
    private void Start()
    {
        UpdateTomesText();
    }

    private void UpdateTomesText()
    {
        m_TomesText.text = m_StoredData.m_AmountTomes.ToString();
    }

    public void AddTomes(int numberTomes)
    {
        m_StoredData.m_AmountTomes += numberTomes;
        m_TomesText.text = m_StoredData.m_AmountTomes.ToString();
        UpdateTomesText();
    }

    public void SaveToJson()
    {
        string DataToStore = JsonUtility.ToJson(m_StoredData);
        string filePath = Application.persistentDataPath + "/StoredData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, DataToStore);
    }
    
    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/StoredData.json";
        string storedData = System.IO.File.ReadAllText(filePath);

        m_StoredData = JsonUtility.FromJson<StoredData>(storedData);
    }
#if !UNITY_EDITOR
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
#endif
}

[System.Serializable]
public class StoredData
{
    public int m_AmountTomes = 0;
    public List<ItemBaseScript> unlockedScripts = new List<ItemBaseScript>();
}

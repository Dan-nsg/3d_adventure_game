using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImportantScripts.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake() 
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Daniel";
    }

    private void Start() 
    {
        Invoke(nameof(Load), .1f);
    }

#region SAVE 
    [NaughtyAttributes.Button]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }
#endregion

    private void SaveFile(string json)
    { 
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if(File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        } 

        FileLoaded.Invoke(_saveSetup);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float health;
    public string playerName;
}

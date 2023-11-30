using System.Collections.Generic;
using Newtonsoft.Json;
using Server.Data;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}


public class DataManager
{
    public enum CharacterClass   //class * 100 + (1~5) = skill
    {
        HolyKnight = 1,
        wizard = 2,
        BlackWizard = 3    
    }


    //public static Dictionary<int, Data.ItemData> ItemDict { get; private set; } = new Dictionary<int, Data.ItemData>();
    public static Dictionary<int, MonsterData> MonsterDict { get; private set; } = new();
    public PlayerData PlayerData { get; private set; } = new();

    public void Init()
    {
        LoadData();
    }


    public static void LoadData()
    {
        MonsterDict = LoadJson<MonsterLoader, int, MonsterData>("MonsterData").MakeDict();
    }

    private static Loader LoadJson<Loader, Key, Value>(string name) where Loader : ILoader<Key, Value>
    {
        var t = Resources.Load($"Data/{name}").ToString();
        //string text = File.ReadAllText($"{Resources.Load($"Data/{name}")}");
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        return JsonConvert.DeserializeObject<Loader>(t, settings);
    }
}
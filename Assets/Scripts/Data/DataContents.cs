using System;
using System.Collections.Generic;
using Google.Protobuf.Protocol;

namespace Server.Data
{
    

    #region Item
    [Serializable]
    public class ItemData
    {
    	public int id;
    	public string name;
    	public ItemType itemType;

    }
    
    public class ConsumableData : ItemData
    {
    	public ConsumableType consumableType;
    	public int MaxCount;
    }

    [Serializable]
    public class ItemLoader : ILoader<int, ItemData>
    {
    	public List<WeaponData> weapons = new List<WeaponData>();
    	public List<ArmorData> armors = new List<ArmorData>();
    	public List<ConsumableData> consumables = new List<ConsumableData>();

    	public Dictionary<int, ItemData> MakeDict()
    	{


    		Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();

    		
    		foreach (ItemData item in consumables)
    		{
    			item.itemType = ItemType.Consumable;
    			dict.Add(item.id, item);
    		}

    		return dict;
    	}
    }

    #endregion

    #region Mosnter

    [Serializable]
    public class RewardData
    {
        public int probability; // 100분율
        public int itemId;
        public int count;
    }


    [Serializable]
    public class MonsterData
    {
        public int id;
        public string name;
        public List<RewardData> rewards;

        public StatInfo stat;
        //public string prefabPath;
    }

    [Serializable]
    public class MonsterLoader : ILoader<int, MonsterData>
    {
        public List<MonsterData> monsters = new();

        public Dictionary<int, MonsterData> MakeDict()
        {
            var dict = new Dictionary<int, MonsterData>();
            foreach (var monster in monsters) dict.Add(monster.id, monster);
            return dict;
        }
    }

    #endregion
}
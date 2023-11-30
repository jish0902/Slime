using System;
using System.Collections.Generic;

namespace Server.Data
{
	#region Item
    [Serializable]
    public class ItemData
    {
    	public int id;
    	public string name;
    }
    
    public class StoneData : ItemData
    {
    	public int MaxCount;
    }

    [Serializable]
    public class ItemLoader : ILoader<int, ItemData>
    {
    
    	public List<StoneData> consumables = new List<StoneData>();

    	public Dictionary<int, ItemData> MakeDict()
    	{


    		Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();

    		
    		foreach (ItemData item in consumables)
    		{
    			dict.Add(item.id, item);
    		}

    		return dict;
    	}
    }

    #endregion

    #region Mosnter

    public enum AttributeType
    {
        none,
        fire,
        water,
        soil,
        all,
    }
    
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
        public int maxHp;
        public AttributeType type;
        public float attackRange;
        public float damage;
        public float attackSpeed;
        public AttributeType weakness;
        public List<RewardData> rewards;
        public string prefabPath;
    }
    
    [Serializable]
    public class PlayerData
    {
        public string name;
        public float damage;
        public int hp;
        public List<RewardData> rewards;
        public float coolTime = 0.1f;

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
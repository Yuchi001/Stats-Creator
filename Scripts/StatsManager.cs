// main script, add this on any object that will have stats from EStatType

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private List<Stat> stats;

    [SerializeField] private Dictionary<EEffectType, bool> effects = new Dictionary<EEffectType, bool>();

    #region stats
    public void CreateStatList(List<Stat> stats) // se stats list, used by stat list creator
    {
        this.stats = stats;
    }

    // Function used to checking value of the given stat, return -1 if stat wasnt found in stats(l: 9) list
    public int GetStatValue(EStatsType statType)
    {
        foreach(var stat in stats)
        {
            if(stat.type == statType)
                return stat.value;
        }
        return -1;
    }

    // Function used to setting given stat by given value
    public void ChangeStatValue(Stat setStat)
    {
        foreach(var stat in stats)
        {
            if(stat.type == setStat.type)
            {
                stat.value = setStat.value;
                return;
            }
        }
    }
}

// Stats class with two constructors, one for statCreator and one for the user
[System.Serializable]
public class Stat
{
    public Stat(string name, int value, EStatsType type) // constructor meant for creating stat for statCreator
    {
        this.name = name;
        this.value = value;
        this.type = type;
    }

    public Stat(int value, EStatsType type) // constructor meant for user. name is only needed when displaying stat name in the inspector
    {
        this.value = value;
        this.type = type;
    }

    [HideInInspector] public string name;
    public int value;
    public EStatsType type;
}
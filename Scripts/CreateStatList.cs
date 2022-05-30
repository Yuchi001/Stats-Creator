using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(StatsManager))]
public class CreateStatList : MonoBehaviour
{
    public StatsManager GetStatsManager()
    {
        return GetComponent<StatsManager>();
    }
    public void UpdateStats(StatsManager statsManager) // updating stat list on current object
    {
        var list = new List<Stat>();
        foreach (var typeName in System.Enum.GetNames(typeof(EStatsType)))
        {
            EStatsType type = (EStatsType)System.Enum.Parse(typeof(EStatsType), typeName);
            int value = statsManager.GetStatValue(type) == -1 ? 0 : statsManager.GetStatValue(type);
            list.Add(new Stat(typeName, value, type));
        }
        statsManager.CreateStatList(list);
    }

    public void UpdateAllMBStats() // updating stat list on objects in scene and resources folder
    {
        foreach(var go in FindObjectsOfTypeAll(typeof(StatsManager)))
        {
            UpdateStats(go as StatsManager);
        }
        foreach (var go in Resources.FindObjectsOfTypeAll(typeof(StatsManager)))
        {
            UpdateStats(go as StatsManager);
        }
    }
}
[CustomEditor(typeof(CreateStatList))]
public class ECreateStatList : Editor
{
    public override void OnInspectorGUI()
    {
        var createStatList = target as CreateStatList;

        if(GUILayout.Button("RefreshStats")) // Custom button. Refresh local object stat list
        {
            var statsManager = createStatList.GetStatsManager();
            createStatList.UpdateStats(statsManager);
        }

        if (GUILayout.Button("RefreshAll")) // Custom button. Refresh all objects stat list
        {
            createStatList.UpdateAllMBStats();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    public static List<UnitBase> Units = new List<UnitBase>();
    public static Dictionary<string, UnitBase> UnitsDictionary = new Dictionary<string, UnitBase>();
    public static Dictionary<GameObject, UnitBase> UnitGameObjectDictionary = new Dictionary<GameObject, UnitBase>();

    public static void RegistUnit(UnitBase newUnit)
    {
        if (Units.Contains(newUnit))
        {
            return;
        }
        if (string.IsNullOrEmpty(newUnit.UnitName) && !UnitsDictionary.ContainsKey(newUnit.UnitName))
        {
            UnitsDictionary.Add(newUnit.UnitName, newUnit);
        }
        if (!UnitGameObjectDictionary.ContainsKey(newUnit.gameObject))
        {
            UnitGameObjectDictionary.Add(newUnit.gameObject, newUnit);
        }
        Units.Add(newUnit);
    }

    public static void UnRegistUnit(UnitBase oldUnit)
    {
        if (!Units.Contains(oldUnit))
        {
            return;
        }

        Units.Remove(oldUnit);
        if(UnitsDictionary.ContainsKey(oldUnit.UnitName))
        {
            UnitsDictionary.Remove(oldUnit.UnitName);
        }
        if (UnitGameObjectDictionary.ContainsKey(oldUnit.gameObject))
        {
            UnitGameObjectDictionary.Remove(oldUnit.gameObject);
        }
    }

    public static UnitBase GetUnitByUnitName(string unitName)
    {
        if (UnitsDictionary.ContainsKey(unitName))
        {
            return UnitsDictionary[unitName];
        }

        return null;
    }

    public static UnitBase GetUnitByGameObject(GameObject gameObject)
    {
        if (UnitGameObjectDictionary.ContainsKey(gameObject))
        {
            return UnitGameObjectDictionary[gameObject];
        }

        return null;
    }
}

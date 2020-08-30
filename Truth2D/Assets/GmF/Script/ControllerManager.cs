using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    public static Dictionary<string, Controller> UnitControllerDictionary = new Dictionary<string, Controller>();
    public static List<Controller> Controllers = new List<Controller>();

    public static void RegistController(Controller newController)
    {
        if (Controllers.Contains(newController))
        {
            return;
        }

        Controllers.Add(newController);

        UnitBase unit = newController.GetComponent<UnitBase>();
        if (unit != null && !UnitControllerDictionary.ContainsKey(unit.UnitName))
        {
            UnitControllerDictionary.Add(unit.UnitName, newController);
        }
    }

    public static void UnRegistController(Controller oldController)
    {
        if (!Controllers.Contains(oldController))
        {
            return;
        }

        Controllers.Remove(oldController);

        UnitBase unit = oldController.GetComponent<UnitBase>();
        if (unit != null && UnitControllerDictionary.ContainsKey(unit.UnitName))
        {
            UnitControllerDictionary.Remove(unit.UnitName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public string UnitName = "";
    public List<string> UnitTags = new List<string>(); 

    private void Awake()
    {
        UnitManager.RegistUnit(this);
    }

    private void OnDestroy()
    {
        UnitManager.UnRegistUnit(this);
    }
}

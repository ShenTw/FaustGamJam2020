using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public GameObject talkerGO;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void CreateTalker(string _string, GameObject _target, float destoryTimer = 3)
    {
        GameObject go =  Instantiate(talkerGO , transform) as GameObject;
        go.GetComponent<Talker>().Init(_string , _target , destoryTimer);
    }
}

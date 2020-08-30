using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isAuto = false;
    public List<KeyCode> KeyCodes = new List<KeyCode>();
    public bool useMouseRightClick = true;
    public List<string> DoUnitTags = new List<string>();
    public int DoMaxCount = 1;
    public int doCount = 0;

    public virtual void Awake()
    {
        ControllerManager.RegistController(this);
    }

    public void OnDestroy()
    {
        ControllerManager.UnRegistController(this);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerEnterControllerCheck();
        }

        CheckDoUnitTag(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerEnterControllerCheck();
        }

        CheckDoUnitTag(other.gameObject);
    }

    bool CheckDoUnitTag(GameObject gameObject)
    {
        UnitBase unit = UnitManager.GetUnitByGameObject(gameObject);
        if (unit != null)
        {
            foreach (string unitTag in DoUnitTags)
            {
                if (unit.UnitTags.Contains(unitTag))
                {
                    CheckDo();
                    return true;
                }
            }
        }

        return false;
    }

    void PlayerEnterControllerCheck()
    {
        if(isAuto || CheckKeys() || (useMouseRightClick && Input.GetMouseButtonDown(0)) )
        {
            CheckDo();
        }
    }

    bool CheckKeys()
    {
        foreach(KeyCode key in KeyCodes)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void CheckDo()
    {
        if(DoMaxCount != -1 && DoMaxCount > doCount)
        {
            Do();
            doCount++;
        }
    }

    public virtual void Do()
    {

    }
}

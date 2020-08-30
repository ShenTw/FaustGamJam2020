using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    static public HUDManager instance;

    public Image bossHP;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ChangeBossHP(float _percent)
    {
        bossHP.fillAmount = _percent;
    }

}


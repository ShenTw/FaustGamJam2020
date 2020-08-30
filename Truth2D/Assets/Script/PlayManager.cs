using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    static public PlayManager instance;

    public PlayerController pc;
    public BossMove boss;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}

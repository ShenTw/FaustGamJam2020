using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowController : Controller
{
    int ShowCount = 0;
    public int ShowMaxCount = 1;
    public float ShowTime = 60;
    float showOverTime = 0;
    bool onShow = false;
    public GameObject ShowGameObject = null;

    public override void Awake()
    {
        base.Awake();

        if(ShowGameObject != null)
        {
            ShowGameObject.SetActive(false);
        }
    }

    public override void Do()
    {
        base.Do();

        Show();
    }

    public virtual void Show()
    {
        if (onShow)
        {
            return;
        }
        if (ShowCount >= ShowMaxCount)
        {
            return;
        }
            
        ShowCount++;
        ShowGameObject.SetActive(true);
        showOverTime = 0;
        onShow = true;
    }

    public virtual void DisableShow()
    {
        if(ShowTime == -1)
        {
            return;
        }

        ShowGameObject.SetActive(false);
        onShow = false;
    }

    private void Update()
    {
        if (!onShow)
        {
            return;
        }
        if(showOverTime >= ShowTime)
        {
            DisableShow();
        }
        showOverTime += Time.deltaTime;
    }
}

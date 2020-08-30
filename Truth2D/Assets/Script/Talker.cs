using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{
    public Text m_Text;
    public RectTransform canvasRectT;
    public Camera m_Camera;

    bool isInit = false;
    GameObject target;
    private Vector2 uiOffset;

    public void Init(string _string , GameObject _target , float destoryTimer = 3)
    {

        gameObject.SetActive(true);
        isInit = true;

        m_Text.text = _string;
        target = _target;
        
        Destroy(gameObject , destoryTimer);
    }

    private void Update()
    {
        if (!isInit) return;
        if (target == null) return;

        transform.position = m_Camera.WorldToScreenPoint(target.transform.position);

    }
}

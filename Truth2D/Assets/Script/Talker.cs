using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{
    public Text m_Text;
    public Image m_Image;
    public RectTransform canvasRectT;
    public Camera m_Camera;

    bool isInit = false;
    GameObject target;
    private Vector2 uiOffset;

    public void Init(string _string, GameObject _target, float destoryTimer = 3)
    {
        isInit = true;
        gameObject.SetActive(true);
        m_Text.text = _string;
        Transform tempTalker = _target.transform.Find("Talker");

        if (tempTalker != null)
        {
            target = tempTalker.gameObject;
        }
        else
        {
            target = _target;
        }

        m_Text.enabled = false;
        m_Image.enabled = false;


        Destroy(gameObject , destoryTimer);
    }

    bool isShow = false;
    void Show()
    {
        isShow = true;
        m_Text.enabled = true;
        m_Image.enabled = true;
        
    }

    private void Update()
    {
        if (!isInit) return;
        if (target == null) return;

        transform.position = m_Camera.WorldToScreenPoint(target.transform.position) ;

        if(!isShow)
        {
            Show();
        }

    }
}

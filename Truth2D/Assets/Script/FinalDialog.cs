using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDialog : MonoBehaviour
{
    public Text dialog;

    public Action OnTimeintval;

    public string[] currentDialog;

    public TriggerAction[] triggers;

    public float dialogDuration = 1f;
    public float dialogInstanc = 1f;

    private float timer = 0f;

    public enum DialogStatus { IDLE, START, ENDING }
    public DialogStatus runState;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var tr in triggers)
        {
            tr.OnColliderTrigger += _OnTrigger;
        }

        OnTimeintval += _OnDialogChange;

        dialog.text = "";

        currentDialog = InitDialog(1);
    }

    private string[] InitDialog(int dialogID)
    {
        if (dialogID == 1)
        {
            return new string[]
            {
                // 1
                "爸爸！你看，這裡有好多漂亮的花！",
                "孩子，記得回家，別玩太晚。",
                "夜晚很危險，到處都有．．．",
                "爸，知道了！你都說過好多次啦。",
            };
        }
        else if (dialogID == 2)
        {
            return new string[] {
                // 2
                "爸爸，你都不會害怕嗎？",
                "很多時候，我們別無選擇。",
                "我只有在必須勇敢的時候勇敢。",
                "但為了生存，我們不得不學會．．．",
                "那些必要的殘忍。",
            }; ;
        }

        else if (dialogID == 3)
        {
            return new string[] {
                // 3
                "爸爸，我辦不到！",
                "孩子，你比你以為得更加強大。",
                "靜下心來，感受風的呢喃。",
                "但是爸爸，我．．不想傷害牠們。",
                "今天，我們將牠們作為食糧，",
                "有一天，我們也會成為其他生命的一部份。",
                "這是大自然的法則。",
                "萬物藉此生生不息．．．",
            }; ;
        }

        else if (dialogID == 4)
        {
            return new string[] {
                // 4
                "小孩：但如今．．．誰又能繼續教導我呢？",
                "父親，我需要您阿．．．",
                "（停頓一陣子之後，天上的繁星組成父親的面容緩緩浮現在空中）",
                "父親：生命終有回歸的時候",
                "父親：只要不曾忘記，我就永遠會活在你心中",
                "父親：當你感到孤單、無助",
                "父親：就抬頭仰望星空，我會一直在這陪著你。",
            }; ;
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (runState)
        {
            case DialogStatus.IDLE:

                break;

            case DialogStatus.START:

                timer += Time.deltaTime;

                if (timer > dialogDuration)
                {
                    timer = 0;
                    OnTimeintval?.Invoke();
                }

                break;

            case DialogStatus.ENDING:
                break;
        }
    }

    private void _OnTrigger(int triggerID)
    {
        runState = DialogStatus.START;

        currentDialog = InitDialog(triggerID);

        timer = 0;
    }

    int dialogIndex = 0;
    private void _OnDialogChange()
    {
        if (currentDialog == null)
            return;

        if (dialogIndex >= currentDialog.Length)
        {
            dialogIndex = 0;
            runState = DialogStatus.ENDING;
            dialog.text = "";
        }
        else
        {
            dialog.text = currentDialog[dialogIndex];
        }

        dialogIndex++;
    }
}

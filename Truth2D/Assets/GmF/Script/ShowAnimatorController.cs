using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAnimatorController : ShowController
{
    public Animator animator = null;
    public List<string> AnimationNames = new List<string>();
    int showAnimationNameIndex = 0;
    public bool DisableHideAnimator = false;
    public string InitAnimatorName = "";
    public bool WattingAnimPlayEnd = true;

    public override void Awake()
    {
        base.Awake();

        if (animator != null)
        {
            if (DisableHideAnimator)
            {
                animator.gameObject.SetActive(false);
            }
            if (!string.IsNullOrEmpty(InitAnimatorName))
            {
                animator.Play(InitAnimatorName);
            }
        }
    }

    public override void Show()
    {
        base.Show();

        if(AnimationNames.Count == 0)
        {
            return;
        }
        if(animator == null)
        {
            return;
        }

        if(WattingAnimPlayEnd && animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationNames[showAnimationNameIndex]))
        {
            ///等待目前動畫結束
            return;
        }

        animator.gameObject.SetActive(true);

        string showAnimationName = AnimationNames[showAnimationNameIndex];
        if (!string.IsNullOrEmpty(showAnimationName))
        {
            animator.Play(AnimationNames[showAnimationNameIndex]);
        }
        showAnimationNameIndex = Mathf.Min(showAnimationNameIndex, Mathf.Max(0, AnimationNames.Count - 1));
    }
}

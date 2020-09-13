using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Animator m_Animator;

    public GameObject bullet;

    public float HP = 0;

    bool isDie = false;

    //動畫Call
    public void OnAttack()
    {
        Vector2 target = new Vector2(PlayManager.instance.pc.transform.position.x , PlayManager.instance.pc.transform.position.y);

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-2, 2);
            float randomY = Random.Range(-2, 2);
            Vector2 myPos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

            GameObject projectile = (GameObject)Instantiate(bullet, myPos, Quaternion.identity);

            Destroy(projectile, 2);
        }

    }

    float timer = 0;
    int dialog = 0;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(!isDie)
        {
            if (timer <= 0)
            {
                timer = 5;
                m_Animator.SetTrigger("Attack");

                //UIManager.instance.CreateTalker("鄵襙鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操操" , gameObject , 2);
            }
        }
        else
        {
            if(dialog == 0)
            {
                dialog = 1;
                StartCoroutine(Talk());
            }
        }
       
    }

    IEnumerator Talk()
    {
        yield return new WaitForSeconds(1);
        UIManager.instance.CreateTalker("今天玩的真開心！", transform.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("但是天色很晚了，你不回家嗎？", transform.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("家？這裡就是我的家呀！", PlayManager.instance.pc.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("這麼漂亮，還有好多好多朋友陪著我。", PlayManager.instance.pc.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("家裡沒有重要的人在等妳嗎？他們會感到很著急的。", transform.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("重要．．．", PlayManager.instance.pc.gameObject, 3);

        yield return new WaitForSeconds(3.2f);
        UIManager.instance.CreateTalker("重要．．的人？", PlayManager.instance.pc.gameObject, 3);
        UIManager.instance.FadeOut();

        yield return new WaitForSeconds(5f);
        PlayManager.instance.ClearBoss();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            Debug.Log("Boss被打");
            Destroy(other.gameObject);
            HP += 10;

            HUDManager.instance.ChangeBossHP(HP / 100);

            if(HP >= 100)
            {
                isDie = true;
            }
        }
    }


}

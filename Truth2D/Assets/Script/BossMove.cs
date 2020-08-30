using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Animator m_Animator;

    public GameObject bullet;

    public float HP = 0;

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
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            timer = 5;
            m_Animator.SetTrigger("Attack");

            //UIManager.instance.CreateTalker("鄵襙鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操鄵襙操操" , gameObject , 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            Debug.Log("Boss被打");
            Destroy(other.gameObject);
            HP += 10;

            HUDManager.instance.ChangeBossHP(HP / 100);
        }
    }
}

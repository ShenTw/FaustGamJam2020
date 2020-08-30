using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Animator m_Animator;

    public GameObject bullet;

    //動畫Call
    public void OnAttack()
    {
        Vector2 target = new Vector2(PlayManager.instance.pc.transform.position.x , PlayManager.instance.pc.transform.position.y);

        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(-2, 2);
            float randomY = Random.Range(-2, 2);
            Vector2 myPos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            Vector2 direction = myPos - target;
            direction.Normalize();
            GameObject projectile = (GameObject)Instantiate(bullet, myPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = -direction * 10;
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
        }
    }
}

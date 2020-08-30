using System.Collections;
using UnityEngine;

public class Deer : MonoBehaviour
{
    GameObject player;
    private Rigidbody rb;

    public bool isLion;
    public bool isSpecialRabbit;
    public bool isRabbit;

    private void Start()
    {
        player = PlayManager.instance.pc.gameObject;
        rb = GetComponent<Rigidbody>();

    }

    bool isToLion = false;
    float lionTimer = 5;
    private void Update()
    {
        if(!isLion && !isSpecialRabbit)
        {
            if(!isToLion)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 3)
                {
                    if (transform.position.x < player.transform.position.x)
                    {
                        if (transform.localScale.x > 0)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                        transform.position += new Vector3(Time.deltaTime * 5, 0, 0);
                    }
                    if (transform.position.x > player.transform.position.x)
                    {
                        if (transform.localScale.x < 0)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                        transform.position -= new Vector3(Time.deltaTime * 5, 0, 0);
                    }
                }
            }
            

            if(isRabbit)
            {
                if (Vector3.Distance(PlayManager.instance.m_Lion.transform.position, transform.position) < 8)
                {
                    isToLion = true;
                }

                if(isToLion)
                {
                    if (Vector3.Distance(PlayManager.instance.m_Lion.transform.position, transform.position) > 3)
                    {
                        if (transform.position.x < PlayManager.instance.m_Lion.transform.position.x)
                        {
                            if (transform.localScale.x > 0)
                            {
                                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                            }
                            transform.position += new Vector3(Time.deltaTime * 5, 0, 0);
                        }
                        if (transform.position.x > PlayManager.instance.m_Lion.transform.position.x)
                        {
                            if (transform.localScale.x < 0)
                            {
                                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                            }
                            transform.position -= new Vector3(Time.deltaTime * 5, 0, 0);
                        }
                    }
                }
            }
        }

        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3)
            {
                isSpecialRabbit = false;
            }
        }

        if(isLion)
        {
            lionTimer -= Time.deltaTime;

            if(lionTimer <=0)
            {
                lionTimer = 5;
                UIManager.instance.CreateTalker("陪我玩陪我玩陪我玩陪我玩" , gameObject , 4);
            }
        }
        
    }

    public void OnJump()
    {
        StartCoroutine(OnJumpWait());
    }

    IEnumerator OnJumpWait()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = new Vector3(rb.velocity.x, 8, rb.velocity.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("hitPlayer");
        }

        if (other.tag == "Lion")
        {
            Debug.Log("hitLion");
            EffectController.PlayFlowerTimberEffect(transform , 2);

        }
    }

}

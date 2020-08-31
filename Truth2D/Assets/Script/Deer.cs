using System.Collections;
using UnityEngine;

public class Deer : MonoBehaviour
{
    GameObject player;
    private Rigidbody rb;

    public bool isLion;
    public bool isSpecialRabbit;
    public bool isRabbit;
    public bool isDeer;

    public LayerMask groundLayer;

    public Transform isGroundedChecker;

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
                Vector3 playerPos = player.transform.position;
                if (isDeer)
                {
                    playerPos = new Vector3(player.transform.position.x , transform.position.y , transform.position.z);
                }

                if (Vector3.Distance(playerPos, transform.position) > 3)
                {
                    
                    if (transform.position.x < playerPos.x)
                    {
                        if (transform.localScale.x > 0)
                        {
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                        }
                        transform.position += new Vector3(Time.deltaTime * 5, 0, 0);
                    }
                    if (transform.position.x > playerPos.x)
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
                if (Vector3.Distance(PlayManager.instance.m_Lion.transform.position, transform.position) < 10)
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
            if (Vector3.Distance(player.transform.position, transform.position) < 1)
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
                UIManager.instance.CreateTalker("陪我玩陪我玩陪我玩陪我玩" , gameObject , 5);
            }
        }
        
    }

    public void OnJump()
    {
        if (PlayManager.instance.stage != 1) return;
        StartCoroutine(OnJumpWait());
    }

    IEnumerator OnJumpWait()
    {
        yield return new WaitForSeconds(0.5f);

        if(CheckIfGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 9, rb.velocity.z);
            EffectController.PlayJumpEffect(transform, 2);
        }
            
    }

    bool CheckIfGrounded()
    {

        Collider[] collider = Physics.OverlapSphere(isGroundedChecker.position, 0.5f, groundLayer);
        if (collider.Length != 0) return true;

        return false;
    }

    bool isTouchLion;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("hitPlayer");
        }

        if (other.tag == "Lion")
        {
            if(isTouchLion)
            {
                return;
            }
            isTouchLion = true;
            PlayManager.instance.LionGetRabbit();
            Debug.Log("hitLion");
            EffectController.PlayLionEatEffect(transform , 2);

        }
    }

}

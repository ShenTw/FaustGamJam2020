﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 1. Player Move: 
// 2. Player Jump:
// 3. Player 
public class PlayerController : MonoBehaviour {

    //Public Variables\\
    public int GameNow = 0;
    public float jumpForce = 0.0f;
    public Transform playerTransform = null;
    public float speed = 1;
    public Animator playerMovement = null;

    public Seeding seeding = null;

    public Camera m_Camera;
    public float cameraheight = 0f;
    public float camerFollowSpeed = 1f;
    public GameObject diePos;
    public GameObject bullet;
    
    [HeaderAttribute("Ground checker")]
    #region ground checker
    public Transform isGroundedChecker; 
    public float checkGroundRadius = 0.5f; 
    public LayerMask groundLayer;
    [SerializeField]
    bool isGrounded = false;

    public bool isNotShot;


    #endregion

    //Private Variables\\

    private bool faceRight = true;
    private Rigidbody rb;
    private float distToGround;

    //Initiate at first frame of game\\


    void Start ()
    {
        //Calling Components\\
 
        rb = GetComponent<Rigidbody> ();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        AudioListener.volume = 0.5f;
    }
 
    //Initiate at a set time\\
 
    void Update() 
    { 
        Move(); 
        CheckIfGrounded();
        Jump();
        MoveCamera();
        CheckDie();
        RayTest();
        Shoot();
    } 

    void RayTest()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider != null && hit.transform.tag == "Seed")
                {
                    seeding.MouseClick();
                }
            }

            
        }
        
    }

    void MoveCamera()
    {
        if (isDie) return;
        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position ,
                                    new Vector3(transform.position.x, transform.position.y + cameraheight, m_Camera.transform.position.z) ,
                                    camerFollowSpeed * Time.deltaTime);

        if(Input.mouseScrollDelta.y > 0 && m_Camera.orthographicSize > 5)
        {
            m_Camera.orthographicSize -= 0.1f;
        }
        else if (Input.mouseScrollDelta.y < 0 && m_Camera.orthographicSize < 8)
        {
            m_Camera.orthographicSize += 0.1f;
        }
    }
 

    void Move() 
    { 
        // float x = Input.GetAxisRaw("Horizontal"); 
        // float moveBy = x * speed; 
        // rb.velocity = new Vector2(moveBy, rb.velocity.y); 
        
        transform.position += Vector3.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
        Animation();
        // Change direction
        ChangeDirection();

    }
    
    void Animation()
    {
        playerMovement.SetBool("isWalking", Input.GetAxis ("Horizontal") != 0);
    }
    void ChangeDirection()
    {
        bool moveToRight = Input.GetAxis ("Horizontal") > 0;
        bool moveToLeft = Input.GetAxis ("Horizontal") < 0;

        if (moveToRight && faceRight != true)
        {
            faceRight = true;
            playerTransform.localScale = new Vector3(- playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        } 
        else if (moveToLeft && faceRight == true)
        {
            faceRight = false;
            playerTransform.localScale = new Vector3(- playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        }
    }

    int hp = 0;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
        }

        if(other.tag == "EnemyBullet")
        {
            Debug.Log("玩家被打到");
            Destroy(other.gameObject);
            hp += 1;
            if(hp >= 3)
            {
                PlayManager.instance.ReGame();
            }

        }

        if (other.tag == "Stage1")
        {
            SceneManager.LoadScene("Yueh1");
        }
    }
    void CheckIfGrounded() 
    { 
        
        Collider[] collider = Physics.OverlapSphere(isGroundedChecker.position, checkGroundRadius, groundLayer); 
        if (collider.Length != 0) 
            isGrounded = true; 
        else 
            isGrounded = false; 
    }

    void Jump() 
    { 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            EffectController.PlayJumpEffect(transform, 2);

            if (PlayManager.instance != null)
                PlayManager.instance.m_Deer.OnJump();
        }
            

        rb.velocity -= new Vector3(0 , Time.deltaTime * 10 ,0);
    }

    IEnumerator ReBorn()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(diePos.transform.position.x, diePos.transform.position.y, transform.position.z);
        isDie = false;
    }

    bool isDie = false;
    void CheckDie()
    {
        if (GameNow == 0)
        {
            if (transform.position.y < -9)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if(GameNow == 1)
        {
            if (transform.position.y < -30)
            {
                PlayManager.instance.ReGame();
            }
        }

        else if (GameNow == 2)
        {
            if (transform.position.y < -9)
            {
                if (isDie) return;
                isDie = true;
                StartCoroutine(ReBorn());
            }
        }
    }

    void Shoot()
    {
        if (isNotShot) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPos = GetWorldPositionOnPlane(Input.mousePosition , 0);
            GameObject projectile = (GameObject)Instantiate(bullet, transform.position , Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = (targetPos - transform.position).normalized * 20;
            Destroy(projectile, 2);
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

}

using System.Collections.Generic;
using UnityEngine;
using System.Collections;

// 1. Player Move: 
// 2. Player Jump:
// 3. Player 
public class PlayerController : MonoBehaviour {
 
    //Public Variables\\
    public float jumpForce = 0.0f;
    public Transform playerTransform = null;
    public float speed = 1;
    public Animator playerMovement = null;

    public Seeding seeding = null;

    public Camera m_Camera;
    public GameObject diePos;
    public GameObject bullet;

    public int stage;
    
    [HeaderAttribute("Ground checker")]
    #region ground checker
    public Transform isGroundedChecker; 
    public float checkGroundRadius = 0.5f; 
    public LayerMask groundLayer;
    [SerializeField]
    bool isGrounded = false; 

    #endregion
 
    //Private Variables\\
 
    private bool faceRight = true;
    private Rigidbody rb;
 
    //Initiate at first frame of game\\
 
    void Start ()
    {
        //Calling Components\\
 
        rb = GetComponent<Rigidbody> ();

        stage = 1; 
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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null && hit.transform.tag == "Seed")
            {
                seeding.MouseClick();
            }
        }
        
    }

    void MoveCamera()
    {
        if (isDie) return;
        m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position , new Vector3(transform.position.x, transform.position.y, m_Camera.transform.position.z) , 1f);

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
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            other.gameObject.SetActive (false);
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
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); 
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
        if(stage == 1)
        {
            if(transform.position.y < -9)
            {
                if (isDie) return;
                isDie = true;
                StartCoroutine(ReBorn());
            }
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y + 1);
            Vector2 direction = myPos - target;
            direction.Normalize();
            GameObject projectile = (GameObject)Instantiate(bullet, myPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = - direction * 20;
            Destroy(projectile, 2);
        }
    }
}

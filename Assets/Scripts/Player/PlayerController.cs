using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerController otherPlayer;

    [SerializeField]
    public bool isAttacking = false;

    public HealthController hpController;

    public bool isPlayerRight; // Differentiere mellem spillere, h?jre og venstre (spiller 1 og 2)

    public string navn;

    public Collider punchCol, kickCol;
    private Vector3 punchPos, kickPos;

    public KeyCode left, right, jumpKey, kick, punch;

    public Sprite st?, spark, sl?, hop;

    public Vector3 jump;
    public float jumpForce;
    public float speed;
    Rigidbody rb;

    private float attackCd = 1f;
    private float attackTimer = 0f;

    public float knockbackKick;
    public float knockbackPunch;

    public Vector3 moveDirection;

    [SerializeField]
    private LayerMask groundLayer;
    public float distanceToGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        punchPos = punchCol.gameObject.transform.localPosition;
        kickPos = kickCol.gameObject.transform.localPosition;
    }
    void Update()
    {
        Move();
        Kick();
        Slag();
    }
    
    void Move()
    {
        Hop();
        SidewaysMove();
    }
    void Hop()
    {
        if (Input.GetKeyDown(jumpKey) && groundCheck())
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision) // Tjekker om spiller er p? jorden eller ej og viser en bestemt sprite efter det
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = st?;
        }
    }
    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = hop;
        }
    }


    void SidewaysMove()
    {
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(left))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            knockbackKick = -knockbackKick;
            knockbackPunch = -knockbackPunch;
            punchCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(-punchPos.x, punchPos.y, punchPos.z);
            kickCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(-kickPos.x, kickPos.y, kickPos.z);
        }
        else if (Input.GetKey(right))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            knockbackKick = Mathf.Abs(knockbackKick);
            knockbackPunch = Mathf.Abs(knockbackPunch);
            punchCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(punchPos.x, punchPos.y, punchPos.z);
            kickCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(kickPos.x, kickPos.y, kickPos.z);
        }
    }
    void Kick()
    {
        if (Input.GetKeyDown(kick) && attackTimer <= 0)
        {
            attackTimer = attackCd;
            StartCoroutine(AttackDelay(kickCol));
            StartCoroutine(ChangeImageBack(spark,st?));
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
    void Slag()
    {
        if (Input.GetKeyDown(punch) && attackTimer <= 0)
        {
            attackTimer = attackCd;
            StartCoroutine(AttackDelay(punchCol));
            StartCoroutine(ChangeImageBack(sl?,st?));
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }
   
    IEnumerator AttackDelay(Collider col)
    {
        col.enabled = true;
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
    }
    IEnumerator ChangeImageBack(Sprite sp1, Sprite sp2)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sp1;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sp2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            isAttacking = true;
            hpController.damage = 10;
            hpController.isHit = false;
            rb.AddForce(new Vector3(otherPlayer.knockbackPunch, 0f, 0f), ForceMode.Impulse);
        }

        else if (other.gameObject.CompareTag("Kick"))
        {
            isAttacking = true;
            hpController.damage = 20;
            hpController.isHit = false;
            rb.AddForce(new Vector3(otherPlayer.knockbackKick, 0f, 0f), ForceMode.Impulse);
        }
        else
        {
            isAttacking = false;
        } 
    }  
    
    public bool groundCheck()
    {
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, Vector3.down, out raycastHit,distanceToGround,groundLayer);
        return raycastHit.collider != null;
    }
}

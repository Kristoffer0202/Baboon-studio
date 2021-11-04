using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool isAttacking = false;

    public HealthController hpController;

    public bool isPlayerRight;

    public string navn;

    public Collider punchCol, kickCol;
    private Vector3 punchPos, kickPos;

    public KeyCode left, right, jumpKey, kick, punch;

    public Sprite stå, spark, slå, hop;

    // Start is called before the first frame update
    public Vector3 jump;
    public float jumpForce;
    public float speed;
    public bool isGrounded;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        punchPos = punchCol.gameObject.transform.localPosition;
        kickPos = kickCol.gameObject.transform.localPosition;
    }
    void Update()
    {
        Move();
        Kick();
        Slag();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = stå; 
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = hop;
        }
    }
    void Move()
    {
        Hop();
        SidewaysMove();
    }
    void Hop()
    {

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse); 
        }
    }
    void SidewaysMove()
    {
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(left))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            punchCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(-punchPos.x, punchPos.y, punchPos.z);
            kickCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(-kickPos.x, kickPos.y, kickPos.z);
        }
        else if (Input.GetKey(right))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            punchCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(punchPos.x, punchPos.y, punchPos.z);
            kickCol.gameObject.GetComponent<Transform>().transform.localPosition = new Vector3(kickPos.x, kickPos.y, kickPos.z);
        }
    }
    void Kick()
    {
        if (Input.GetKeyDown(kick))
        {
            print("test");
            StartCoroutine(AttackDelay(kickCol));
            StartCoroutine(ChangeImageBack(spark,stå));
        }
    }

    void Slag()
    {
        if (Input.GetKeyDown(punch))
        {         
            StartCoroutine(AttackDelay(punchCol));
            StartCoroutine(ChangeImageBack(slå,stå));
        }
    }

    

    IEnumerator AttackDelay(Collider col)
    {
        col.enabled = true;
        yield return new WaitForSeconds(1);
        col.enabled = false;
    }
    IEnumerator ChangeImageBack(Sprite sp1, Sprite sp2)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sp1;
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sp2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            isAttacking = true;
            hpController.damage = 10;
            hpController.isHit = false;
        }

        else if (other.gameObject.CompareTag("Kick"))
        {
            isAttacking = true;
            hpController.damage = 20;
            hpController.isHit = false;
        }
        else
        {
            isAttacking = false;
        }
    }
}

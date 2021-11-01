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

    public KeyCode left, right, jumpKey, kick, punch;

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
    }
    void Update()
    {
        Move();
        Kick();
        Slag();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
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
        }
        else if (Input.GetKey(right))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
        }


    }
    void Kick()
    {
        if (Input.GetKeyDown(kick))
        { 
            StartCoroutine(AttackDelay(kickCol));
        }

    }

    void Slag()
    {
        if (Input.GetKeyDown(punch))
        {         
            StartCoroutine(AttackDelay(punchCol));
        }
    }

    IEnumerator AttackDelay(Collider col)
    {
        col.enabled = true;
        yield return new WaitForSeconds(1);
        col.enabled = false;
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

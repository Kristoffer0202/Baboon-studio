using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce;
    public float speed;
    public bool isGrounded;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    void Update()
    {
        Move();
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
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void SidewaysMove()
    {
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
        }
    }
}

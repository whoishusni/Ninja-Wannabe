using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isFacingLeft = false;
    public Transform b1;
    public Transform b2;
    Rigidbody2D rigid;
    Animator animator;
    public int HP = 1;
    bool isDie = false;
    public static int enemyKilled = 0;
    float kecepatan = 2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded && !isDie)
        {
            if(isFacingLeft)
            {
                toLeft();
            }
            else
            {
                toRight();
            }

            if(transform.position.x <= b1.position.x && isFacingLeft)
            {
                flip();
            }
            else if (transform.position.x >= b2.position.x && !isFacingLeft)
            {
                flip();
            }
        }
        
    }
    void TakeDamage(int damage)
    {
        HP -= damage;
        if(HP <=0)
        {
            isDie = true;
            rigid.velocity = Vector2.zero;
            animator.SetBool("monDead", true);
            Destroy(this.gameObject, 2);
            DataCoin.data += 25;
            
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingLeft = !isFacingLeft;
    }

    private void toRight()
    {
        Vector3 position = transform.position;
        position.x += kecepatan * Time.deltaTime;
        transform.position = position;
        if(isFacingLeft)
        {
            flip();
        }
    }

    private void toLeft()
    {
        Vector3 position = transform.position;
        position.x -= kecepatan * Time.deltaTime;
        transform.position = position;
        if(!isFacingLeft)
        {
            flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

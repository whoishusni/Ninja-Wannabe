using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isDead = false;
    bool isJump = true;
    int idMove = 0;
    bool isCanShoot = true;
    Animator animator;
    public GameObject projectile;
    public Vector2 projectileVelocity;
    public Vector2 projectileOffset;
    public float coolDown = 1f;
    public String nameScene;
    AudioSource audioSource;
    public AudioClip audioClipCoin;
    public AudioClip audioThrow;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isCanShoot = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            toLeft();
        }
        
        if(Input.GetKeyDown(KeyCode.D))
        {
            toRight();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            toJump();
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            toIdle();
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            toIdle();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            toFire();
        }
        toMove();
        toDead();

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Kunai"))
        {
            isCanShoot = true;
        }

        if (collision.transform.tag.Equals("Monster"))
        {
            animator.SetTrigger("isDead");
            SceneManager.LoadScene("GameOver");
            isDead = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(isJump)
        {
            animator.ResetTrigger("jump");
            if(idMove == 0)
            {
                animator.SetTrigger("idle");
            }
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetTrigger("jump");
        animator.ResetTrigger("run");
        animator.ResetTrigger("idle");
        isJump = true;
    }

    private void toDead()
    {
       
    }

    private void toMove()
    {
        if(idMove == 1 && !isDead)
        {
            if(!isJump)
            {
                animator.SetTrigger("run");
            }
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        if (idMove == 2 && !isDead)
        {
            if (!isJump)
            {
                animator.SetTrigger("run");
            }
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }
    }

    private void toIdle()
    {
        if(!isJump)
        {
            animator.ResetTrigger("jump");
            animator.ResetTrigger("run");
            animator.SetTrigger("idle");
        }
        idMove = 0;
    }

    private void toFire()
    {
       if(isCanShoot)
        {
            audioSource.PlayOneShot(audioThrow);
            animator.SetTrigger("throw");
            //Membuat projectile baru
            GameObject bullet = (GameObject)Instantiate(projectile,
                (Vector2)transform.position + projectileOffset * transform.localScale.x, Quaternion.identity);
            // mengatur kecepatan dari projectile
            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x,
                projectileVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity;
            //Menyesuaikan scale dari projectile dengan scale karakter
            Vector3 scale = transform.localScale;
            bullet.transform.localScale = scale * 1;
            StartCoroutine(CanThrow());
        }
    }

    IEnumerator CanThrow()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(coolDown);
        isCanShoot = true;
    }

    private void toRight()
    {
        idMove = 1;
    }

    private void toLeft()
    {
        idMove = 2;
    }

    private void toJump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag.Equals("Koin"))
        {
            audioSource.PlayOneShot(audioClipCoin);
            DataCoin.data += 10;
            Destroy(collision.gameObject);
        }

        if(collision.transform.tag.Equals("Petir"))
        {
            SceneManager.LoadScene(nameScene);
        }

        if(collision.transform.tag.Equals("GOBatas"))
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }
}

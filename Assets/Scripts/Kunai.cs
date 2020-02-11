using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
       
        Destroy(this.gameObject,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
        }
        Destroy(this.gameObject);
    }
}

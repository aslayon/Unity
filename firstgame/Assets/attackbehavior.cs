using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackbehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("monster"))
        {
            
            Destroy(collision.gameObject);
            
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("player"))//�÷��̾���� �浹�� ����
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
}

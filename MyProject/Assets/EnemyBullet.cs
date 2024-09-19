using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;          
    public float lifetime = 5f;        
    private Rigidbody rb;
    public string tagToIgnore = "Enemy";

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        rb.velocity = transform.forward * speed;  

        
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToIgnore))
        {
            Debug.Log("Colisión ignorada con: " + collision.gameObject.name);
            return;
        }
        else { 
            Destroy(gameObject);
         }
    }
}

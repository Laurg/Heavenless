using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;

    void Start()
    {
        // Obtiene la referencia al componente Rigidbody del proyectil
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mover el proyectil usando el Rigidbody en la dirección hacia adelante
        MoveBullet();
    }

    private void MoveBullet()
    {
        // Mueve el proyectil hacia adelante en función de la rotación del objeto
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destruir el proyectil al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}

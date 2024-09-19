using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    public GameObject enemyBullet;  // Prefab de la bala
    private float timer = 0f;
    public float speed = 1f;
    public float fireRate = 0.5f;  // Intervalo entre disparos

    [SerializeField] private SpawnerType spawnerType;

    void Update()
    {
        // Si el tipo de spawner es "Spin", rota el spawner
        if (spawnerType == SpawnerType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 1f, 0f);
        }

        // Control del tiempo entre disparos
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            Fire();
            timer = 0f;  // Reinicia el temporizador después de disparar
        }
    }

    private void Fire()
    {
        if (enemyBullet)  // Asegúrate de que el prefab de la bala esté asignado
        {
            int bulletCount = 5;  // Número de balas en el abanico
            float angleStep = 10f;  // Separación en grados entre cada bala
            float initialAngle = -(angleStep * (bulletCount - 1)) / 2;  // Ángulo inicial para centrar el abanico

            for (int i = 0; i < bulletCount; i++)
            {
                // Calcula el ángulo de disparo para cada bala
                float currentAngle = initialAngle + (i * angleStep);
                Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);  // Rotación en Y

                // Instancia una nueva bala en la posición y rotación del spawner
                GameObject spawnedBullet = Instantiate(enemyBullet, transform.position, transform.rotation * rotation);

                // Configura la velocidad de la bala
                EnemyBullet bulletScript = spawnedBullet.GetComponent<EnemyBullet>();
                if (bulletScript != null)
                {
                    bulletScript.speed = speed;
                }
            }
        }
    }
}

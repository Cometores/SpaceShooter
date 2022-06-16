using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    float movementDirection = 1;
    int life = 3;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject explosion;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
        EnemyFire();
        rb.velocity = Vector2.up * speed;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "UpperBound" || collision.gameObject.name == "LowerBound")
        {
            movementDirection *= -1;
            rb.velocity = movementDirection * speed * Vector2.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            life -= 1;
        }

        if (life == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void EnemyFire()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
        Invoke(nameof(EnemyFire), Random.Range(2.5f, 4));
    }
}

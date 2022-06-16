using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 15f;
    [SerializeField] float movementDirection;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start() => rb.velocity = Vector2.right * speed * movementDirection;

    private void OnBecameInvisible() => Destroy(gameObject);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("HUi");
            Destroy(gameObject);
        }
    }
}

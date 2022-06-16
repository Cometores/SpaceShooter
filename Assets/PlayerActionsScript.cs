using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerActionsScript : MonoBehaviour
{
    public PlayerInputActions playerControls;
    InputAction move;
    InputAction fire;
    [SerializeField] GameObject bullet;

    AudioSource auSource;
    [SerializeField] AudioClip shootClip;
    [SerializeField] AudioClip explosionClip;
    SpriteRenderer sr;

    [SerializeField] GameObject explosion;

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    float moveDirection = 0;
    bool isFail;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        auSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();

        fire.performed += Fire;
    }

    void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    void Update()
    {
        if (!isFail)
            moveDirection = move.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (isFail)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(0, moveDirection * moveSpeed);
    }

    void Fire(InputAction.CallbackContext context)
    {
        if (!isFail)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
            auSource.PlayOneShot(shootClip);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            isFail = true;
            sr.enabled = false;
            Instantiate(explosion, transform.position, Quaternion.identity);
            auSource.PlayOneShot(explosionClip);
            Invoke(nameof(restartGame), 1.2f);
        }
    }

    void restartGame() => SceneManager.LoadScene("SpaceShooter");
}

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerActionsScript : MonoBehaviour
{
    public PlayerInputActions playerControls;
    InputAction move;
    InputAction fire;
    [SerializeField] GameObject bullet; 

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    float moveDirection = 0;
    bool isFail;

    private void Awake()
    {
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
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            isFail = true;
            Invoke(nameof(restartGame), 1.2f);
        }
    }

    void restartGame() => SceneManager.LoadScene("SpaceShooter");
}

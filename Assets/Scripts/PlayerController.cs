using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    private bool isGrounded = false;

    [Header("Sounds")] 
    public AudioClip jumpSound; 
    public AudioClip coinSound; 
    private AudioSource audioSource; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        transform.Translate(Vector2.right * runSpeed * Time.deltaTime);

        if ((Keyboard.current.spaceKey.wasPressedThisFrame || 
            Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Detecta si tocó el suelo
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Detecta si chocó con obstáculo
        if (col.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Recoge monedas
        if (col.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(1);
            col.gameObject.SetActive(false); 
            audioSource.PlayOneShot(coinSound);
        }
    }
}
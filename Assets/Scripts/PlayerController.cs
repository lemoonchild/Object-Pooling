using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float runSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    private bool isGrounded = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        // Corre hacia la derecha
        transform.Translate(Vector2.right * runSpeed * Time.deltaTime);

        // Salto 
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
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
            GameManager.instance.AddScore(10);
            col.gameObject.SetActive(false); 
        }
    }
}
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    private Transform player;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        // Se mueve hacia la izquierda
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Si está muy lejos del jugador hacia la izquierda, regresa al pool
        if (transform.position.x < player.position.x - 20f)
        {
            gameObject.SetActive(false); 
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offsetX = 3f;

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = new Vector3(
            player.position.x + offsetX,
            transform.position.y,
            transform.position.z
        );
    }
}
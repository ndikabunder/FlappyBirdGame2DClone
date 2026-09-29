using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;
    const float width = 6.5625f;   // lebar 1 sprite base

    void Update()
    {
        if (GameManager.Instance.State == GameState.GameOver) return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x <= -width)
            transform.position += Vector3.right * width;
    }
}
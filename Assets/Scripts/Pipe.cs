using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float speed = 2.5f;

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < -4f) Destroy(gameObject);
    }
}
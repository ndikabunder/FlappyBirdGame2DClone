using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float jumpVelocity = 6f;

    Rigidbody2D rb;
    float startX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;              // diam dulu sebelum tap pertama
        startX = transform.position.x;
    }

    void Update()
    {
        var gm = GameManager.Instance;
        if (gm.State == GameState.GameOver) return;

        if (gm.State == GameState.Ready)   // efek melayang naik-turun
            transform.position = new Vector3(startX, Mathf.Sin(Time.time * 5f) * 0.15f, 0);

        if (GameManager.Tapped())
        {
            if (gm.State == GameState.Ready)
            {
                gm.StartGame();
                rb.simulated = true;
            }
            rb.linearVelocity = new Vector2(0f, jumpVelocity);
            AudioManager.Instance.PlayWing();
        }

        if (gm.State == GameState.Playing)  // miringkan burung sesuai kecepatan
        {
            float angle = Mathf.Clamp(rb.linearVelocity.y * 5f, -90f, 30f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnCollisionEnter2D(Collision2D _)
    {
        if (GameManager.Instance.State == GameState.Playing)
            GameManager.Instance.GameOver();
    }

    void OnTriggerEnter2D(Collider2D _)
    {
        if (GameManager.Instance.State == GameState.Playing)
            GameManager.Instance.AddScore();
    }
}
using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] frames;   // isi: downflap, midflap, upflap, midflap
    [SerializeField] float fps = 10f;

    SpriteRenderer sr;

    void Awake() => sr = GetComponent<SpriteRenderer>();

    void Update()
    {
        if (GameManager.Instance.State == GameState.GameOver) return;
        sr.sprite = frames[(int)(Time.time * fps) % frames.Length];
    }
}
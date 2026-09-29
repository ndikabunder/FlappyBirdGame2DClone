using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState { Ready, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Ready;

    [SerializeField] ScoreDisplay scoreDisplay;
    [SerializeField] GameObject readyGroup;
    [SerializeField] GameObject gameOverImage;
    [SerializeField] ScoreBoard scoreBoard;

    int score;
    float gameOverTime;

    void Awake() => Instance = this;

    void Start()
    {
        scoreDisplay.Show(0);
        readyGroup.SetActive(true);
        gameOverImage.SetActive(false);
    }

    void Update()
    {
        if (State == GameState.GameOver && Time.time - gameOverTime > 0.7f && Tapped())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        State = GameState.Playing;
        readyGroup.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        scoreDisplay.Show(score);
        AudioManager.Instance.PlayPoint();
    }

    public void GameOver()
    {
        State = GameState.GameOver;
        gameOverTime = Time.time;
        int prevBest = PlayerPrefs.GetInt("Best", 0);
        bool isNew = score > prevBest;
        int best = Mathf.Max(score, prevBest);
        PlayerPrefs.SetInt("Best", best);
        StartCoroutine(GameOverRoutine(best, isNew));
    }

    IEnumerator GameOverRoutine(int best, bool isNew)
    {
        AudioManager.Instance.PlayHit();
        yield return new WaitForSeconds(0.25f);
        AudioManager.Instance.PlayDie();
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlaySwoosh();
        scoreDisplay.gameObject.SetActive(false);   // skor besar disembunyikan
        gameOverImage.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        AudioManager.Instance.PlaySwoosh();
        scoreBoard.Show(score, best, isNew);
    }

    public static bool Tapped()
    {
        return (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);
    }
}
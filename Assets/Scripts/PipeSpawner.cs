using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab;
    [SerializeField] float interval = 1.7f;
    [SerializeField] float minY = -1.5f;
    [SerializeField] float maxY = 2f;

    float timer;

    void Update()
    {
        if (GameManager.Instance.State != GameState.Playing) return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = interval;
            Instantiate(pipePrefab,
                new Vector3(transform.position.x, Random.Range(minY, maxY), 0f),
                Quaternion.identity);
        }
    }
}
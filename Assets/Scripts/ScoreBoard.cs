using System.Collections;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] ScoreDisplay scoreDigits;
    [SerializeField] ScoreDisplay bestDigits;
    [SerializeField] SpriteRenderer medal;
    [SerializeField] Sprite[] medalSprites;   // urut: bronze, silver, gold, platinum
    [SerializeField] GameObject newBadge;
    [SerializeField] GameObject okButton;

    public void Show(int score, int best, bool isNew)
    {
        gameObject.SetActive(true);
        okButton.SetActive(true);

        scoreDigits.Show(score);
        bestDigits.Show(best);
        newBadge.SetActive(isNew);

        int tier = score >= 4 ? 3 : score >= 3 ? 2 : score >= 2 ? 1 : score >= 1 ? 0 : -1;
        medal.enabled = tier >= 0;
        if (tier >= 0) medal.sprite = medalSprites[tier];

        StartCoroutine(SlideIn());
    }

    IEnumerator SlideIn()
    {
        Vector3 target = transform.position;
        Vector3 start = target + Vector3.down * 1.5f;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.35f;
            transform.position = Vector3.Lerp(start, target, 1f - Mathf.Pow(1f - t, 3f));
            yield return null;
        }
        transform.position = target;
    }
}
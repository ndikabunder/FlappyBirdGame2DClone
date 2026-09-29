using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] digits;          // isi urut 0 sampai 9
    [SerializeField] float spacing = 0.04f;    // jarak antar digit
    [SerializeField] int sortingOrder = 20;
    [SerializeField] bool alignRight = false;

    readonly List<SpriteRenderer> pool = new();

    public void Show(int value)
    {
        string s = value.ToString();

        // buat SpriteRenderer tambahan jika digit bertambah (misal 9 -> 10)
        while (pool.Count < s.Length)
        {
            var go = new GameObject("Digit");
            go.transform.SetParent(transform, false);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = sortingOrder;
            pool.Add(sr);
        }

        // hitung lebar total supaya angka selalu rata tengah
        float total = spacing * (s.Length - 1);
        for (int i = 0; i < s.Length; i++)
            total += digits[s[i] - '0'].bounds.size.x;

        float x = alignRight ? -total : -total / 2f;
        for (int i = 0; i < pool.Count; i++)
        {
            if (i >= s.Length) { pool[i].enabled = false; continue; }

            Sprite sp = digits[s[i] - '0'];
            float w = sp.bounds.size.x;
            pool[i].sprite = sp;
            pool[i].enabled = true;
            pool[i].transform.localPosition = new Vector3(x + w / 2f, 0f, 0f);
            x += w + spacing;
        }
    }
}
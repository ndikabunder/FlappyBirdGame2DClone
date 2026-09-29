using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioClip wing;
    [SerializeField] AudioClip point;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip die;
    [SerializeField] AudioClip swoosh;

    AudioSource src;

    void Awake()
    {
        Instance = this;
        src = GetComponent<AudioSource>();
    }

    public void PlayWing() => src.PlayOneShot(wing);
    public void PlayPoint() => src.PlayOneShot(point);
    public void PlayHit() => src.PlayOneShot(hit);
    public void PlayDie() => src.PlayOneShot(die);
    public void PlaySwoosh() => src.PlayOneShot(swoosh);
}
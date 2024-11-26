using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;
    [SerializeField] private AudioClip completeBGM;
    [SerializeField] private AudioClip insertBGM;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if(Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void PlayCompleteBGM() => audioSource.PlayOneShot(completeBGM);
    public void PlayInsertBGM() => audioSource.PlayOneShot(insertBGM);
}

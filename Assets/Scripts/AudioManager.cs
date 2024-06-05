using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clip -----------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip portalIn;
    public AudioClip running;
    public AudioClip jumpLand;
    public AudioClip jumpSound;
    public AudioClip itemEquipment;
    public AudioClip axeSound;
    public AudioClip pickaxeSound;
    public AudioClip leverSound;

    public static AudioManager instance;

    private HashSet<AudioClip> activeClips = new HashSet<AudioClip>();

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!activeClips.Contains(clip))
        {
            SFXSource.PlayOneShot(clip);
            activeClips.Add(clip);
            StartCoroutine(RemoveClipFromActive(clip, clip.length));
        }
    }

    private IEnumerator RemoveClipFromActive(AudioClip clip, float duration)
    {
        yield return new WaitForSeconds(duration);
        activeClips.Remove(clip);
    }

    void Update()
    {

    }
}

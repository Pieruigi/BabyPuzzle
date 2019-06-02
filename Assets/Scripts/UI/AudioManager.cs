using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Conf")]
    [SerializeField]
    AudioMixer mixer;

    [SerializeField]
    AudioSource fxSource;

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    AudioSource winSource;


    [Header("FX Clips")]
    [SerializeField]
    AudioClip clickClip = null;

    [SerializeField]
    AudioClip dropClip = null;

    bool musicOff = false;
    public bool IsMuted
    {
        get { return musicOff; }
    }

    float musicVolumeDefault;

    // Start is called before the first frame update
    void Start()
    {
        //mixer = GameManager.FindObjectOfType<AudioMixer>();
        //GetComponent<Button>().onClick.AddListener(OnMusicOnOff);
        musicVolumeDefault = musicSource.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MusicOnOff()
    {
        musicOff = !musicOff;
        mixer.SetFloat("Volume", musicOff ? -80 : 0);
    }

    public void PlayClick()
    {
        fxSource.clip = clickClip;
        PlayFx();
    }

    public void PlayDrop()
    {
        fxSource.clip = dropClip;
        PlayFx(0.2f);
    }

    public void PlayWin()
    {
        StartCoroutine(CoroPlayWin());
        
    }

    private void PlayFx()
    {
        fxSource.Play();
    }

    private void PlayFx(float delay)
    {
        fxSource.PlayDelayed(delay);
    }

    IEnumerator CoroPlayWin()
    {
        musicSource.volume = 0.1f;
        winSource.Play();

        while (winSource.isPlaying)
            yield return null;

        musicSource.volume = musicVolumeDefault;
    }
}

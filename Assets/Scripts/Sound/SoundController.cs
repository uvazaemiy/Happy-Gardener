using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    
    [SerializeField] private SoundButtonData fxButton;
    [SerializeField] private SoundButtonData musicButton;
    [Space]
    [Range(0, 1)] 
    [SerializeField] private float musicVolume = 0.5f;
    [Range(0, 1)] 
    public float fxVolume;
    [Range(0.7f, 1)]
    [SerializeField] private float lowPitch = 0.8f;
    [Range(1, 1.3f)]
    [SerializeField] private float highPitch = 1.2f;
    [Space]
    [SerializeField] private AudioClip winSound; 
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip moveSound;
    [SerializeField] private AudioSource music;

    private AudioSource currentMovingSource;

    private void Start()
    {
        instance = this;
        
        if (PlayerPrefs.GetFloat("fxVolume") == -1)
            ChangeFxVolume(fxButton);
        if (PlayerPrefs.GetFloat("MusicVolume") == -1)
            ChangeMusicVolume(musicButton);
    }

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1)
    {
        if (clip != null)
        {
            GameObject go = new GameObject("SoundFX " + clip.name);
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            float randomPitch = Random.Range(lowPitch, highPitch);
            //source.pitch = randomPitch;
            source.volume = volume;

            source.Play();
            Destroy(go, clip.length);
            return source;
        }

        return null;
    }

    private AudioSource PlayRandom(AudioClip[] clips, Vector3 position, float volume = 1)
    {
        if (clips != null)
        {
            if (clips.Length != 0)
            {
                int randomIndex = Random.Range(0, clips.Length);

                if (clips[randomIndex] != null)
                {
                    AudioSource source = PlayClipAtPoint(clips[randomIndex], position, volume);
                    return source;
                }
            }
        }

        return null;
    }
    
    private IEnumerator PlayRandomMusicRoutine()
    {
        yield return new WaitForSeconds(music.clip.length);
        StartCoroutine(PlayRandomMusicRoutine());
    }

    public void PlayWinSound()
    {
        PlayClipAtPoint(winSound, Vector3.zero, fxVolume);
    }
    
    public void PlayLoseSound()
    {
        PlayClipAtPoint(loseSound, Vector3.zero, fxVolume);
    }
    
    public void PlayMoveSound()
    {
        if (currentMovingSource != null)
            Destroy(currentMovingSource.gameObject);
        
        currentMovingSource = PlayClipAtPoint(moveSound, Vector3.zero, fxVolume);
    }

    public void ChangeMusicVolume(SoundButtonData soundButtonData)
    {
        //music = MusicPlayer.Instance.music;
        musicVolume = soundButtonData.ChangeVolume(musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        if (music != null)
            music.volume = musicVolume;
    }

    public void ChangeFxVolume(SoundButtonData soundButtonData)
    {
        fxVolume = soundButtonData.ChangeVolume(fxVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);
    }
}
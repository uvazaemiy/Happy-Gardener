using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    
    public AudioSource music;
 
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(PlayMusic());
        }
    }

    private IEnumerator PlayMusic()
    {
        music.Play();
        yield return new WaitForSeconds(music.clip.length);
        StartCoroutine(PlayMusic());
    }
}

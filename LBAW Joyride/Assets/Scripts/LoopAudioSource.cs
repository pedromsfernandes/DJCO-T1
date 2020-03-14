using UnityEngine;
using System.Collections;
 
 public class LoopAudioSource : MonoBehaviour
 {
    private static AudioSource instance = null;
    public static AudioSource Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this.transform.GetComponentInChildren<AudioSource>()) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this.transform.GetComponentInChildren<AudioSource>();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    static public void PlayMusic()
    {
        if (instance.isPlaying) instance.Stop();
        instance.Play();
    }

    static public void PlayMusic(AudioClip clip)
    {
        if(instance == null) return;
        if (instance.isPlaying) instance.Stop();
        instance.clip = clip;
        instance.Play();
    }
 
    static public void StopMusic()
    {
        if(instance == null) return;
        instance.Stop();
    }
 }
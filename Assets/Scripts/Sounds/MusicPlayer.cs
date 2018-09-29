using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    static MusicPlayer Instance;
    AudioSource source;

    static bool _enabled;
    public static bool Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            if (_enabled)
            {
                Instance.source.UnPause();
                Debug.Log("Music is enabled");
            }
                
            else
                Instance.source.Pause();
        }
    }

    void Awake()
    {

        DontDestroyOnLoad(this);
        Instance = this;        
        source = GetComponent<AudioSource>();
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music");

        if (clips.Length > 0)
        {
            source.clip = clips[0];
            source.Play();
        }
        Enabled = true;
    }    

}

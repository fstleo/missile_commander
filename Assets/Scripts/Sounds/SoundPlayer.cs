using UnityEngine;
using System.Collections.Generic;

namespace Common.Sound
{
    public class SoundPlayer
    {      
        private readonly Dictionary<string, AudioClip> _sounds;
        private readonly AudioSource [] _sources;
        int currentChannel = 0;

        public static bool Enabled;
        

        public SoundPlayer(int sourcesCount) 
        {
            _sounds = new Dictionary<string, AudioClip>();
            var sourcesGameObject = new GameObject("SoundSources");
            
            _sources = new AudioSource[sourcesCount];            
            for (int i = 0; i < sourcesCount; i++)
            {
                _sources[i] = sourcesGameObject.AddComponent<AudioSource>();                 
            }
            
            AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");
            foreach(AudioClip clip in clips)
            {
                _sounds.Add(clip.name, clip);
            }
            Enabled = true;
        }

        public void PlaySound(string name, bool loop = false)
        {
            if (Enabled)
            {
                _sources[currentChannel].clip = _sounds[GetRandomSound(name)];
                _sources[currentChannel].pitch = 1 + Random.Range(-0.3f, 0.3f);
                _sources[currentChannel].Play();
                _sources[currentChannel].loop = loop;
                currentChannel = (currentChannel + 1) % _sources.Length;
            }
        }

        string GetRandomSound(string name)
        {
            List<string> clips = new List<string>();
            foreach (KeyValuePair<string, AudioClip> pair in _sounds)
            {
                if (pair.Key.Contains(name))
                {
                    clips.Add(pair.Key);
                }
            }
            return clips[(int)(UnityEngine.Random.value * clips.Count)];
        }

    }
   
}


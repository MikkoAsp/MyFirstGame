using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip clip;
    }

    public List<SoundEffect> soundEffects;
    private AudioSource audioSoure;
    private Dictionary<string, AudioClip> audioDict;

    private void Start()
    {
        audioDict = new Dictionary<string, AudioClip>();
        audioSoure = GetComponent<AudioSource>();
        foreach (SoundEffect effect in soundEffects)
        {
            audioDict.Add(effect.name, effect.clip);
        }
    }

    public void PlayAudioClip(string clipName)
    {
        audioSoure.PlayOneShot(audioDict[clipName]);
    }
}

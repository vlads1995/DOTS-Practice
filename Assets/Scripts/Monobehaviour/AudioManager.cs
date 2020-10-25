using System;
using UnityEngine;

namespace Monobehaviour
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        
        public AudioSource musicSource;

        private void Awake()
        {
            instance = this;
        }

        public void PlaySfxRequest(string name)
        {
            var audio = Resources.Load<AudioClip>("SFX/" + name);

            if (audio == null)
            {
                return;
            }

            AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position);
        }

        public void PlayMusicRequest(string name)
        {
            var audio = Resources.Load<AudioClip>("Music/" + name);

            if (audio == null)
            {
                return;
            }

            if (musicSource.clip != audio)
            {
                musicSource.clip = audio;
                musicSource.Play();
            }
        }
    }
}
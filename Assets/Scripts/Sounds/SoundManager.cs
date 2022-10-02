using System;
using UnityEngine;

namespace Helzinko
{
    [Serializable]
    public class Sound
    {
        public GameType.SoundTypes type;
        public AudioClip clip;
    }

    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSO soundsSo;

        public static SoundManager instance;

        [SerializeField] private GameObject _musicSource, _effectSource;

        private AudioSource music;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayEffect(GameType.SoundTypes clip, float deviation = 0)
        {
            var sound = Instantiate(_effectSource, transform);
            var audioClip = FindClip(clip, soundsSo.sounds);
            var source = sound.GetComponent<AudioSource>();
            source.PlayOneShot(audioClip);
            source.pitch += UnityEngine.Random.Range(-deviation, deviation);

            Destroy(sound, audioClip.length);
        }

        public void PlayMusic(GameType.SoundTypes clip)
        {
            var sound = Instantiate(_musicSource, transform);
            var audioClip = FindClip(clip, soundsSo.sounds);

            if (music) music.Stop();

            music = sound.GetComponent<AudioSource>();
            music.clip = audioClip;
            music.loop = true;
            music.Play();
        }

        private AudioClip FindClip(GameType.SoundTypes clip, Sound[] sounds)
        {
            foreach (var sound in sounds)
            {
                if (sound.type == clip)
                    return sound.clip;
            }

            return null;
        }
    }
}
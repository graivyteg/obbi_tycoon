using System;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _buildSound;
        [SerializeField] private AudioClip _finishSound;
        [SerializeField] private AudioClip _cashSound;
        
        public enum SoundType
        {
            Build,
            Finish,
            Cash
        }
        
        private AudioSource _source;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
        }

        public void PlaySound(SoundType type)
        {
            if (type == SoundType.Build) _source.PlayOneShot(_buildSound);
            else if (type == SoundType.Finish) _source.PlayOneShot(_finishSound);
            else if (type == SoundType.Cash) _source.PlayOneShot(_cashSound);
        }
    }
}
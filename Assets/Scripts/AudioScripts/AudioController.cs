using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace AudioScripts
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioTrack[] _tracks;

        private Dictionary<AudioType, AudioTrack> _audioDictionary;
        private static AudioController _instance;

        public AudioController Instance => _instance;

        private void Awake()
        {
            if (!_instance)
            {
                Init();
            }
            else
            {
                Debug.LogWarning("Object already created");
            }
        }

        private void Init()
        {
            _instance = this;
            _audioDictionary = new Dictionary<AudioType, AudioTrack>();

            foreach (var track in _tracks)
            {
                foreach (var audioObject in track.Audio)
                {
                    if (!_audioDictionary.ContainsKey(audioObject.Type))
                    {
                        _audioDictionary.Add(audioObject.Type, track);
                    }
                }
            }
        }

        public void PlayAudio(AudioType type)
        {
            var track = _audioDictionary[type];
            track.Source.clip = GetClipFromTrack(type, track);
            track.Source.Play();
        }

        public void StopAudio(AudioType type)
        {
            var track = _audioDictionary[type];
            track.Source.clip = GetClipFromTrack(type, track);
            track.Source.Stop();
        }
        
        [CanBeNull]
        private AudioClip GetClipFromTrack(AudioType type, AudioTrack track)
        {
            foreach (var audioObject in track.Audio)
            {
                if (audioObject.Type == type)
                {
                    return audioObject.Clip;
                }
            }

            return null;
        }
    }
}
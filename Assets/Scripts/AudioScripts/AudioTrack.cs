using System;
using UnityEngine;

namespace AudioScripts
{
    [Serializable]
    public class AudioTrack
    {
        public AudioSource Source;
        public AudioObject[] Audio;
    }
}
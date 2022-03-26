using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager AM;
        public List<AudioSource> AudioSourceList = new List<AudioSource>();
        private void Start() {
        AM = this;

        }                            
    }

}

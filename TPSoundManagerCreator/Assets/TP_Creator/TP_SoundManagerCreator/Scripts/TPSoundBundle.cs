using UnityEngine;

namespace TP.SoundManager
{
    public class TPSoundBundle : ScriptableObject
    {
        [System.Serializable]
        public struct TPSound
        {
            public string AudioClipName;
            public AudioClip AudioClip;
        }
        public TPSound[] Sounds = new TPSound[0];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TestBundle")]
public class TPSoundBundle : ScriptableObject
{
    [System.Serializable]
    public struct TPSound
    {
        public string AudioClipName;
        public AudioClip AudioClip;
    }

    public TPSound[] Sounds;
}

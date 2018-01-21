using UnityEngine;

namespace TP_SoundManagerEditor
{
    public class TPSoundManagerGUIData : ScriptableObject
    {
        [HideInInspector] public GUISkin GUISkin;
        [HideInInspector] public string BundlePath;
    }
}
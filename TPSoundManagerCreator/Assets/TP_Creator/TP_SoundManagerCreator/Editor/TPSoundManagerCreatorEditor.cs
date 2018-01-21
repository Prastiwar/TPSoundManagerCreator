using UnityEngine;
using UnityEditor;
using TP_SoundManager;

namespace TP_SoundManagerEditor
{
    //[CustomEditor(typeof(TPSoundManagerCreator))]
    public class TPSoundManagerCreatorEditor : ScriptlessSoundEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Script which allows you managing sounds");
            OpenCreator();
        }

    }
}
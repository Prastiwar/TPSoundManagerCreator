using UnityEngine;
using UnityEditor;
using TP.SoundManager;

namespace TP.SoundManagerEditor
{
    [CustomEditor(typeof(TPSoundManagerGUIData))]
    internal class TPSoundManagerGUIDataEditor : ScriptlessSoundEditor
    {
        TPSoundManagerGUIData TPSoundData;

        void OnEnable()
        {
            if (hideFlags != HideFlags.NotEditable)
                hideFlags = HideFlags.NotEditable;
            TPSoundData = (TPSoundManagerGUIData)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Container for editor data");
            if (!TPSoundManagerCreator.DebugMode)
                return;

            EditorGUILayout.LabelField("GUI Skin");
            TPSoundData.GUISkin =
                (EditorGUILayout.ObjectField(TPSoundData.GUISkin, typeof(GUISkin), true) as GUISkin);
        }
    }
}
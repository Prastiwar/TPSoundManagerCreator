using UnityEngine;
using UnityEditor;
using TP_SoundManager;

namespace TP_SoundManagerEditor
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
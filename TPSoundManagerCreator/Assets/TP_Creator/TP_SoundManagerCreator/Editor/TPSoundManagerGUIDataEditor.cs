using UnityEngine;
using UnityEditor;

namespace TP_SoundManagerEditor
{
    [CustomEditor(typeof(TPSoundManagerGUIData))]
    public class TPSoundManagerGUIDataEditor : ScriptlessSoundEditor
    {
        TPSoundManagerGUIData TPSoundData;

        void OnEnable()
        {
            TPSoundData = (TPSoundManagerGUIData)target;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("GUI Skin");
            TPSoundData.GUISkin =
                (EditorGUILayout.ObjectField(TPSoundData.GUISkin, typeof(GUISkin), true) as GUISkin);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Directory to new Audio Bundles");
            TPSoundData.BundlePath = EditorGUILayout.TextField(TPSoundData.BundlePath);

            if (GUI.changed)
                EditorUtility.SetDirty(TPSoundData);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
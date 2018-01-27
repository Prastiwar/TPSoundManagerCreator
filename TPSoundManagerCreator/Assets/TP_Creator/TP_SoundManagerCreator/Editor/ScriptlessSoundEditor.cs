using UnityEngine;
using UnityEditor;
using TP_SoundManager;

namespace TP_SoundManagerEditor
{
    internal class ScriptlessSoundEditor : Editor
    {
        public readonly string scriptField = "m_Script";

        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, scriptField);

            OpenCreator();
        }

        public void OpenCreator()
        {
            if (TPSoundManagerCreator.DebugMode)
            {
                if (serializedObject.targetObject.hideFlags != UnityEngine.HideFlags.NotEditable)
                    serializedObject.targetObject.hideFlags = UnityEngine.HideFlags.NotEditable;
                return;
            }

            if (serializedObject.targetObject.hideFlags != HideFlags.None)
                serializedObject.targetObject.hideFlags = HideFlags.None;

            if (GUILayout.Button("Open Sound Manager", GUILayout.Height(30)))
                TPSoundManagerDesigner.OpenWindow();
        }
    }
}
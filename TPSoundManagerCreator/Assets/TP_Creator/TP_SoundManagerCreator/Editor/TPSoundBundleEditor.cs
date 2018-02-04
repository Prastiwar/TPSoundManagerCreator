using UnityEngine;
using UnityEditor;
using TP.SoundManager;

namespace TP.SoundManagerEditor
{
    [CustomEditor(typeof(TPSoundBundle))]
    internal class TPSoundBundleEditor : ScriptlessSoundEditor
    {
        TPSoundBundle SoundBundle;
        SerializedProperty list;
        SerializedProperty size;

        GUIContent content = new GUIContent("Amount of Sounds");
        bool[] toggle;
        bool isChanging = false;
        string AssetName;

        void OnEnable()
        {
            SoundBundle = target as TPSoundBundle;
            list = serializedObject.FindProperty("Sounds");
            size = list.FindPropertyRelative("Array.size");

            toggle = new bool[SoundBundle.Sounds.Length];
            int length = toggle.Length;
            for (int i = 0; i < length; i++)
                toggle[i] = true;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            
            DrawName();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            DrawSounds();

            serializedObject.ApplyModifiedProperties();
        }

        void RenameAsset()
        {
            string oldName = System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(SoundBundle));
            int oldNameIndex = AssetDatabase.GetAssetPath(SoundBundle).IndexOf(oldName);
            string newPath = AssetDatabase.GetAssetPath(SoundBundle).Substring(oldNameIndex, 0) + AssetName;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(SoundBundle), newPath);
        }

        void DrawName()
        {
            if (GUILayout.Button("Change name of Bundle"))
                isChanging = !isChanging;

            if (isChanging)
            {
                EditorGUILayout.BeginHorizontal();
                AssetName = EditorGUILayout.TextField(AssetName, GUILayout.Width(220));
                if (GUILayout.Button("Apply"))
                {
                    isChanging = !isChanging;
                    RenameAsset();
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        void DrawSounds()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(content, GUILayout.Width(120));
            EditorGUILayout.PropertyField(size, GUIContent.none, GUILayout.Width(50));
            if (GUILayout.Button("Add new"))
                list.arraySize++;
            EditorGUILayout.EndHorizontal();

            int length = SoundBundle.Sounds.Length;
            for (int i = 0; i < length; i++)
            {
                if (i >= toggle.Length)
                {
                    SetToggle();
                    return;
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Sound Index: " + i))
                {
                    toggle[i] = !toggle[i];
                }
                if (toggle[i])
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Name: ", GUILayout.Width(80));
                    SoundBundle.Sounds[i].AudioClipName = EditorGUILayout.TextField(SoundBundle.Sounds[i].AudioClipName);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Audio Clip: ", GUILayout.Width(80));
                    SoundBundle.Sounds[i].AudioClip =
                        EditorGUILayout.ObjectField(SoundBundle.Sounds[i].AudioClip, typeof(AudioClip), false) as AudioClip;
                    EditorGUILayout.EndHorizontal();
                }
            }
        }
        void SetToggle()
        {
            bool[] temp = toggle;
            toggle = new bool[SoundBundle.Sounds.Length];

            int length = toggle.Length;
            int tempLength = temp.Length;
            for (int i = 0; i < length; i++)
            {
                if (i < tempLength)
                    toggle[i] = temp[i];
                else
                    toggle[i] = true;
            }
            Repaint();
        }
    }
}
using UnityEditor;
using TP.SoundManager;

namespace TP.SoundManagerEditor
{
    [CustomEditor(typeof(TPSoundManagerCreator))]
    internal class TPSoundManagerCreatorEditor : ScriptlessSoundEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Script which allows you managing sounds");

            if (TPSoundManagerCreator.DebugMode)
            {
                DrawPropertiesExcluding(serializedObject, scriptField);
            }

            OpenCreator();
        }

    }
}
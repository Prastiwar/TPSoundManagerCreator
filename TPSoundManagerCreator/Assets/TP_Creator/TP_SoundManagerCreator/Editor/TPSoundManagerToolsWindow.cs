using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace TP_SoundManagerEditor
{
    [InitializeOnLoad]
    public class TPSoundManagerToolsWindow : EditorWindow
    {
        public static TPSoundManagerToolsWindow window;

        SerializedProperty SoundBundles;

        GUIContent content0 = new GUIContent("Resolution Dropdown");

        Texture2D mainTexture;
        Texture2D tooltipTexture;
        Texture2D previewTexture;

        Vector2 scrollPos = Vector2.zero;
        Vector2 textureVec;

        Rect mainRect;

        static float windowSize = 515;
        static string currentScene;

        public static void OpenToolWindow()
        {
            if (window != null)
                window.Close();

            window = (TPSoundManagerToolsWindow)GetWindow(typeof(TPSoundManagerToolsWindow));

            currentScene = EditorSceneManager.GetActiveScene().name;
            EditorApplication.hierarchyWindowChanged += hierarchyWindowChanged;

            window.minSize = new Vector2(windowSize, windowSize);
            window.maxSize = new Vector2(windowSize, windowSize);
            window.Show();
        }

        static void hierarchyWindowChanged()
        {
            if (currentScene != EditorSceneManager.GetActiveScene().name)
            {
                if (TPSoundManagerDesigner.window)
                    TPSoundManagerDesigner.window.Close();
                if (window)
                    window.Close();
            }
        }

        void OnEnable()
        {
            InitTextures();

            FindLayoutProperties();
        }

        void FindLayoutProperties()
        {
            SoundBundles = TPSoundManagerDesigner.creator.FindProperty("SoundBundles");
        }

        void InitTextures()
        {
            Color color = new Color(0.19f, 0.19f, 0.19f);
            mainTexture = new Texture2D(1, 1);
            mainTexture.SetPixel(0, 0, color);
            mainTexture.Apply();
        }

        void OnGUI()
        {
            mainRect = new Rect(0, 0, Screen.width, Screen.height);
            GUI.DrawTexture(mainRect, mainTexture);
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false, GUIStyle.none, GUI.skin.verticalScrollbar);
            DrawTool();
            GUILayout.EndScrollView();
        }

        void DrawTool()
        {
            DrawSoundBundles();
        }

        void DrawSoundBundles()
        {
            if (SoundBundles == null)
                return;

            if (GUILayout.Button("Create new", TPSoundManagerDesigner.EditorData.GUISkin.button))
            {
                CreateBundle();
            }
            if (SoundBundles.arraySize == 0)
            {
                EditorGUILayout.HelpBox("No bundles loaded!", MessageType.Error);
                return;
            }

            EditorGUILayout.LabelField("Sound Bundles loaded:", GUILayout.Width(180));

            TPSoundManagerDesigner.creator.Update();
            SoundBundles.serializedObject.UpdateIfRequiredOrScript();
            ShowBundles();
        }

        void ShowBundles()
        {
            //int length = list.arraySize;
            //for (int i = 0; i < length; i++)
            //{
            //    GUILayout.BeginHorizontal();
            //    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            //    EditAsset(list, i);
            //    RemoveAsset(list, i);
            //    GUILayout.EndHorizontal();
            //}
            foreach (UnityEngine.Object element in TPSoundManagerDesigner.SoundCreator.SoundBundles)
            {
                _object = element;
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(element, typeof(TPSoundBundle), false);
                EditAsset(element);
                RemoveAsset(element);
                GUILayout.EndHorizontal();
                EditorUtility.SetDirty(element == null ? this : element);
            }

            if (GUI.changed)
                SoundBundles.serializedObject.ApplyModifiedProperties();
        }
        object _object;
        void RemoveAsset(Object obj)
        {
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                AssetDatabase.MoveAssetToTrash(assetPath);

                DrawTool();
                TPSoundManagerDesigner.UpdateManager();
                DrawTool();
            }
        }

        void EditAsset(Object obj)
        {
            if (GUILayout.Button("Edit", GUILayout.Width(35)))
            {
                AssetDatabase.OpenAsset(obj);
            }
        }

        void CreateBundle()
        {
            string assetPath = "Assets/" + TPSoundManagerDesigner.EditorData.BundlePath;
            UnityEngine.Object newObj = null;

            newObj = ScriptableObject.CreateInstance<TPSoundBundle>();
            assetPath += "/New Audio Bundle.asset";

            if (!AssetDatabase.IsValidFolder("Assets/" + TPSoundManagerDesigner.EditorData.BundlePath))
                System.IO.Directory.CreateDirectory("Assets/" + TPSoundManagerDesigner.EditorData.BundlePath);

            AssetDatabase.CreateAsset(newObj, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log(newObj.name + " created in Assets/" + TPSoundManagerDesigner.EditorData.BundlePath);
            TPSoundManagerDesigner.UpdateManager();
            DrawTool();
        }

        void Update()
        {
            if (EditorApplication.isCompiling)
                this.Close();
        }
    }

}
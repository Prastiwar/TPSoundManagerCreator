using UnityEngine;
using UnityEditor;
using TP_SoundManager;
using UnityEditor.SceneManagement;

namespace TP_SoundManagerEditor
{
    [InitializeOnLoad]
    internal class TPSoundManagerDesigner : EditorWindow
    {
        public static TPSoundManagerDesigner window;
        static string currentScene;

        [MenuItem("TP_Creator/TP_SoundManagerCreator")]
        public static void OpenWindow()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.Log("You can't change Sound Manager Designer runtime!");
                return;
            }
            window = (TPSoundManagerDesigner)GetWindow(typeof(TPSoundManagerDesigner));
            currentScene = EditorSceneManager.GetActiveScene().name;
            EditorApplication.hierarchyWindowChanged += hierarchyWindowChanged;
            window.minSize = new Vector2(615, 290);
            window.maxSize = new Vector2(615, 290);
            window.Show();
        }

        static void hierarchyWindowChanged()
        {
            if (currentScene != EditorSceneManager.GetActiveScene().name)
            {
                if (TPSoundManagerToolsWindow.window)
                    TPSoundManagerToolsWindow.window.Close();
                if (window)
                    window.Close();
            }
        }

        public static TPSoundManagerGUIData EditorData;
        public static TPSoundManagerCreator SoundCreator;
        public static GUISkin skin;

        Texture2D headerTexture;
        Texture2D managerTexture;
        Texture2D toolTexture;

        Rect headerSection;
        Rect managerSection;
        Rect toolSection;

        bool existManager;
        bool toggleChange;

        public static SerializedObject creator;

        void OnEnable()
        {
            InitEditorData();
            InitTextures();
            InitCreator();

            if(SoundCreator)
                creator = new SerializedObject(SoundCreator);
        }

        void InitEditorData()
        {
            EditorData = AssetDatabase.LoadAssetAtPath(
                   "Assets/TP_Creator/TP_SoundManagerCreator/EditorResources/SoundManagerEditorGUIData.asset",
                   typeof(TPSoundManagerGUIData)) as TPSoundManagerGUIData;
            
            if (EditorData == null)
                CreateEditorData();
            else
                CheckGUIData();

            skin = EditorData.GUISkin;
        }

        void CheckGUIData()
        {
            if (EditorData.GUISkin == null)
                EditorData.GUISkin = AssetDatabase.LoadAssetAtPath(
                      "Assets/TP_Creator/TP_SoundManagerCreator/EditorResources/TPSoundGUISkin.guiskin",
                      typeof(GUISkin)) as GUISkin;

            if (EditorData.BundlePath == null || EditorData.BundlePath.Length < 5)
                EditorData.BundlePath = "TP_Creator/TP_SoundManagerCreator/AudioBundlesData/";

            EditorUtility.SetDirty(EditorData);
        }

        void CreateEditorData()
        {
            TPSoundManagerGUIData newEditorData = ScriptableObject.CreateInstance<TPSoundManagerGUIData>();
            AssetDatabase.CreateAsset(newEditorData, "Assets/TP_Creator/TP_SoundManagerCreator/EditorResources/SoundManagerEditorGUIData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorData = newEditorData;
            CheckGUIData();
        }

        void InitTextures()
        {
            Color colorHeader = new Color(0.19f, 0.19f, 0.19f);
            Color color = new Color(0.15f, 0.15f, 0.15f);

            headerTexture = new Texture2D(1, 1);
            headerTexture.SetPixel(0, 0, colorHeader);
            headerTexture.Apply();

            managerTexture = new Texture2D(1, 1);
            managerTexture.SetPixel(0, 0, color);
            managerTexture.Apply();

            toolTexture = new Texture2D(1, 1);
            toolTexture.SetPixel(0, 0, color);
            toolTexture.Apply();
        }

        static void InitCreator()
        {
            if (SoundCreator == null)
            {
                SoundCreator = FindObjectOfType<TPSoundManagerCreator>();

                if (SoundCreator != null)
                    UpdateManager();
            }
        }

        void OnGUI()
        {
            if (EditorApplication.isPlaying)
            {
                if (TPSoundManagerToolsWindow.window)
                    TPSoundManagerToolsWindow.window.Close();
                this.Close();
            }
            DrawLayouts();
            DrawHeader();
            DrawManager();
            DrawTools();
        }

        void DrawLayouts()
        {
            headerSection = new Rect(0, 0, Screen.width, 50);
            managerSection = new Rect(0, 50, Screen.width / 2, Screen.height);
            toolSection = new Rect(Screen.width / 2, 50, Screen.width / 2, Screen.height);

            GUI.DrawTexture(headerSection, headerTexture);
            GUI.DrawTexture(managerSection, managerTexture);
            GUI.DrawTexture(toolSection, toolTexture);
        }

        void DrawHeader()
        {
            GUILayout.BeginArea(headerSection);
            GUILayout.Label("TP Sound Creator - Manage your Sounds!", skin.GetStyle("HeaderLabel"));
            GUILayout.EndArea();
        }

        void DrawManager()
        {
            GUILayout.BeginArea(managerSection);
            GUILayout.Label("Sound Manager - Core", skin.box);

            if (SoundCreator == null)
            {
                InitializeManager();
            }
            else
            {
                ToggleDebugMode();
                EditorGUILayout.Space();
                ResetManager();

                if (GUILayout.Button("Refresh and update", skin.button, GUILayout.Height(70)))
                {
                    UpdateManager();
                }
            }

            GUILayout.EndArea();
        }

        void InitializeManager()
        {
            if (GUILayout.Button("Initialize New Manager", skin.button, GUILayout.Height(60)))
            {
                GameObject go = (new GameObject("TP_SoundManager", typeof(TPSoundManagerCreator)));
                GameObject goTheme = (new GameObject("TP_ThemeSound", typeof(AudioSource)));
                goTheme.transform.SetParent(go.transform);
                SoundCreator = go.GetComponent<TPSoundManagerCreator>();
                UpdateManager();
                Debug.Log("Sound Manager created!");
            }

            if (GUILayout.Button("Initialize Exist Manager", skin.button, GUILayout.Height(60)))
                existManager = !existManager;

            if (existManager)
                SoundCreator = EditorGUILayout.ObjectField(SoundCreator, typeof(TPSoundManagerCreator), true,
                    GUILayout.Height(30)) as TPSoundManagerCreator;
        }

        void ResetManager()
        {
            if (GUILayout.Button("Reset Manager", skin.button, GUILayout.Height(45)))
                SoundCreator = null;
        }

        void ToggleDebugMode()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Toggle Debug Mode", skin.button, GUILayout.Height(45)))
                TPSoundManagerCreator.DebugMode = !TPSoundManagerCreator.DebugMode;
            GUILayout.Toggle(TPSoundManagerCreator.DebugMode, GUIContent.none, GUILayout.Width(15));
            GUILayout.EndHorizontal();
        }

        public static void UpdateManager()
        {
            if (SoundCreator)
            {
                SoundCreator.Refresh();
                SoundCreator.OnValidate();
                EditorUtility.SetDirty(SoundCreator);
            }

            if (SoundCreator)
                creator = new SerializedObject(SoundCreator);

            if (creator != null)
            if (creator.targetObject != null)
            {
                creator.UpdateIfRequiredOrScript();
                creator.ApplyModifiedProperties();
            }
        }

        void DrawTools()
        {

            GUILayout.BeginArea(toolSection);
            GUILayout.Label("Sound Manager - Tools", skin.box);

            if (SoundCreator == null)
            {
                GUILayout.EndArea();
                return;
            }

            if (GUILayout.Button("Sound Bundles", skin.button, GUILayout.Height(60)))
            {
                TPSoundManagerToolsWindow.OpenToolWindow();
            }
            GUILayout.EndArea();
        }

    }
}
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using TP.SoundManager;

namespace TP.SoundManagerEditor
{
    [InitializeOnLoad]
    internal class TPSoundManagerToolsWindow : EditorWindow
    {
        public static TPSoundManagerToolsWindow window;

        SerializedProperty SoundBundles;

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
            
            SoundBundles.serializedObject.UpdateIfRequiredOrScript();
            if (GUI.changed)
                SoundBundles.serializedObject.ApplyModifiedProperties();
            ShowBundles();
        }

        void ShowBundles()
        {
            foreach (Object element in TPSoundManagerDesigner.SoundCreator.SoundBundles)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(element, typeof(TPSoundBundle), false);
                EditAsset(element);
                RemoveAsset(element);
                GUILayout.EndHorizontal();

                if(element != null)
                    EditorUtility.SetDirty(element);
            }

            if (GUI.changed)
                SoundBundles.serializedObject.ApplyModifiedProperties();
        }

        void RemoveAsset(Object obj)
        {
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                string assetPath = AssetDatabase.GetAssetPath(obj);
                AssetDatabase.MoveAssetToTrash(assetPath);

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
            string assetPath = TPSoundManagerDesigner.EditorData.Paths[0];
            string newAssetPath = assetPath;
            UnityEngine.Object newObj = null;

            newObj = ScriptableObject.CreateInstance<TPSoundBundle>();
            newAssetPath += "New Audio Bundle.asset";

            if (!AssetDatabase.IsValidFolder(assetPath))
                System.IO.Directory.CreateDirectory(assetPath);
            
            AssetDatabase.CreateAsset(newObj, AssetDatabase.GenerateUniqueAssetPath(newAssetPath));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.OpenAsset(newObj);

            Debug.Log(newObj.name + " created in " + TPSoundManagerDesigner.EditorData.Paths[0]);
            TPSoundManagerDesigner.UpdateManager();
            DrawTool();
            ShowBundles();
        }

        void Update()
        {
            if (EditorApplication.isCompiling || TPSoundManagerDesigner.SoundCreator == null)
                this.Close();
        }
    }

}
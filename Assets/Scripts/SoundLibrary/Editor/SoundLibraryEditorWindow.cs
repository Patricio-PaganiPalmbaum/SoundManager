using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility = PalmaDevelops.SoundLibraryWindowUtility;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for draw the sound library editor window.
    /// </summary>
    public sealed class SoundLibraryEditorWindow : EditorWindow
    {
        private bool IsWindowInitialized => (soundLibrary != null) && (simpleClipDataDrawer != null) && (compoundClipDataDrawer != null);

        private SoundLibrary soundLibrary;

        private int currentToolBar;

        private SimpleClipDataDrawer simpleClipDataDrawer;
        private CompoundClipDataDrawer compoundClipDataDrawer;

        private Texture2D backgroundTexture;
        private Color backgroundColor = new Color(0.3f, 0.3f, 0.3f);

        private GUISkin skin;

        [MenuItem("PalmaSM/SoundLibrary")]
        public static void ShowWindow()
        {
            SoundLibraryEditorWindow window = GetWindow<SoundLibraryEditorWindow>();

            window.titleContent = Utility.titleWindowGUIContent;
            window.minSize = new Vector2(520, 540);
            window.Show();
        }

        private void OnEnable()
        {
            Initialize();
        }

        /// <summary>
        /// Find the sound library dependencies and makes the window ready for use.
        /// </summary>
        private void Initialize()
        {
            soundLibrary = PalmaEditorUtility.FindAssetObject<SoundLibrary>(AssetsDirectory.SOUND_LIBRARY_CREATION_PATH + ".asset");

            skin = Resources.Load<GUISkin>(AssetsDirectory.RESOURCES_WINDOW_STYLE);

            simpleClipDataDrawer = new SimpleClipDataDrawer(skin, soundLibrary.singleClips);
            compoundClipDataDrawer = new CompoundClipDataDrawer(skin, soundLibrary.compoundClips);

            backgroundTexture = new Texture2D(1, 1);
            backgroundTexture.SetPixel(0, 0, backgroundColor);
            backgroundTexture.Apply();
        }

        /// <summary>
        /// Main method to draw the content of the editor window.
        /// </summary>
        private void OnGUI()
        {
            if (!IsWindowInitialized) { Initialize(); }

            GUI.DrawTexture(new Rect(0, 0, position.width, position.height), backgroundTexture);

            if (!soundLibrary) { return; }

            DrawBody();
            DrawFooter();

            Repaint();
        }

        /// <summary>
        /// Method to draw the body of the window. Draws a toolbar for switch the draw between single sound editor and compound sound editor.
        /// </summary>
        private void DrawBody()
        {
            currentToolBar = GUILayout.Toolbar(currentToolBar, Utility.toolBarOptions, skin.GetStyle("Tab"));

            if (currentToolBar == 0)
            {
                soundLibrary.singleClips = simpleClipDataDrawer.DrawClipDataLibrary();
            } else
            {
                soundLibrary.compoundClips = compoundClipDataDrawer.DrawClipDataLibrary();
            }
        }

        /// <summary>
        /// Method to draw the footer of the window. Draws buttons to save or build the sound library.
        /// </summary>
        private void DrawFooter()
        {
            EditorGUILayout.Space(2);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(Utility.saveGUIContent))
            {
                EditorUtility.SetDirty(soundLibrary);
            }

            if (GUILayout.Button(Utility.buildGUIContent))
            {
                Debug.Log("Starting build library...");
                BuildLibrary();
                EditorUtility.SetDirty(soundLibrary);
                Debug.Log("Build completed!");
                Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(2);
        }

        /// <summary>
        /// Method that generate enums from the library of single and compound clips.
        /// </summary>
        private void BuildLibrary()
        {
            EnumGenerator.GenerateEnum(GenerateSingleClipKeys(), AssetsDirectory.SOUND_ENUM_DIRECTORY, AssetsDirectory.SINGLE_ENUM_NAME);
            EnumGenerator.GenerateEnum(GenerateCompoundClipKeys(), AssetsDirectory.SOUND_ENUM_DIRECTORY, AssetsDirectory.COMPOUND_ENUM_NAME);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Method that remove the empty clips data and assign a unique key for each clip data. Returns a collection of keys from the library of single clips.
        /// </summary>
        /// <returns>Collection of keys from the library of single clips.</returns>
        private List<string> GenerateSingleClipKeys()
        {
            List<string> keys = new List<string>();

            for (int i = soundLibrary.singleClips.Length - 1; i >= 0; i--)
            {
                if (!soundLibrary.singleClips[i].soundConfig.clip)
                {
                    Debug.LogWarning(soundLibrary.singleClips[i].name + " has no clip assigned and was removed from the single clip list.");
                    soundLibrary.singleClips = ArrayUtility.RemoveArrayElement(soundLibrary.singleClips, i);
                    continue;
                }

                soundLibrary.singleClips[i].name = GenerateName(keys, soundLibrary.singleClips[i].name);
                keys.Add(soundLibrary.singleClips[i].name);
            }

            return keys;
        }

        /// <summary>
        /// Method that remove the empty clips data and assign a unique key for each clip data. Returns a collection of keys from the library of compound clips.
        /// </summary>
        /// <returns>Collection of keys from the library of compound clips.</returns>
        private List<string> GenerateCompoundClipKeys()
        {
            List<string> keys = new List<string>();

            for (int i = soundLibrary.compoundClips.Length - 1; i >= 0; i--)
            {
                for (int j = soundLibrary.compoundClips[i].soundConfigs.Length - 1; j >= 0; j--)
                {
                    if (!soundLibrary.compoundClips[i].soundConfigs[j].clip)
                    {
                        Debug.LogWarning(soundLibrary.compoundClips[i].name + " remove the clip [" + j + "] because  has no clip assigned.");
                        soundLibrary.compoundClips[i].soundConfigs = ArrayUtility.RemoveArrayElement(soundLibrary.compoundClips[i].soundConfigs, j);
                    }
                }

                if (soundLibrary.compoundClips[i].soundConfigs.Length == 0)
                {
                    Debug.LogWarning(soundLibrary.compoundClips[i].name + " has no any clip assigned and was removed from the single clip list.");
                    soundLibrary.compoundClips = ArrayUtility.RemoveArrayElement(soundLibrary.compoundClips, i);
                    continue;
                }

                soundLibrary.compoundClips[i].name = GenerateName(keys, soundLibrary.compoundClips[i].name);
                keys.Add(soundLibrary.compoundClips[i].name);
            }

            return keys;
        }

        /// <summary>
        /// Method to generate a unique key from clip data name. Returns the new unique key.
        /// </summary>
        /// <param name="currentKeys">List of stored keys.</param>
        /// <param name="processingKey">The current processing key.</param>
        /// <returns>The new unique key.</returns>
        private string GenerateName(List<string> currentKeys, string processingKey)
        {
            if (currentKeys.Contains(processingKey))
            {
                return GenerateName(currentKeys, processingKey, 1);
            }

            return processingKey;
        }

        /// <summary>
        /// Method to generate a unique key from clip data name. Returns the new unique key.
        /// </summary>
        /// <param name="currentKeys">List of stored keys.</param>
        /// <param name="processingKey">The current processing key.</param>
        /// <param name="step">Current step of key generation.</param>
        /// <returns>The new unique key.</returns>
        private string GenerateName(List<string> currentKeys, string processingKey, int step)
        {
            if (currentKeys.Contains(processingKey + "_" + step))
            {
                return GenerateName(currentKeys, processingKey, step + 1);
            }

            return processingKey + "_" + step;
        }
    }
}
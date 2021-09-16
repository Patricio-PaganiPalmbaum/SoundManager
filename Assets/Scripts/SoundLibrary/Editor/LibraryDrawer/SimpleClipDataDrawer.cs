using UnityEditor;
using UnityEngine;
using Utility = PalmaDevelops.SoundLibraryWindowUtility;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the drawing of the Single clip Data in editor window.
    /// </summary>
    public sealed class SimpleClipDataDrawer : SoundLibraryDrawer<SingleClipData>
    {
        private SingleClipData[] singleClips;

        public SimpleClipDataDrawer(GUISkin guiSkin, SingleClipData[] clipData) : base(guiSkin)
        {
            singleClips = clipData;
            foldOutController = new bool[singleClips.Length];
        }

        /// <summary>
        /// Draws the stored collection of single clip data. Use to add/remove single clip data or update their values.
        /// Returns the updated collection of single clip data.
        /// </summary>
        /// <returns>The updated collection of single clip data.</returns>
        public override SingleClipData[] DrawClipDataLibrary()
        {
            EditorGUILayout.Space(5);
            AddClipData();
            EditorGUILayout.Space(5);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, EditorStyles.helpBox);

            for (int i = singleClips.Length - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.BeginHorizontal();

                foldOutController[i] = EditorGUILayout.Foldout(foldOutController[i], singleClips[i].name, foldOutStyle);
                if (GUILayout.Button(Utility.clipBoardGUIContent, GUILayout.ExpandWidth(false))) { EditorGUIUtility.systemCopyBuffer = singleClips[i].name; }
                if (GUILayout.Button(Utility.resetSetupValues, GUILayout.ExpandWidth(false))) { singleClips[i].soundConfig.ResetValues(); }
                if (RemoveClipData(i)) { continue; }

                EditorGUILayout.EndHorizontal();

                if (foldOutController[i])
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();

                    DrawClipData(singleClips[i], Utility.singleClipNameGUIContent);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();

            return singleClips;
        }

        /// <summary>
        /// Draw the single clip data values.
        /// </summary>
        /// <param name="clipToDraw">Single clip data to draw.</param>
        /// <param name="guiContent">GUIContent to draw with the clip data.</param>
        protected override void DrawClipData(SingleClipData clipToDraw, GUIContent guiContent)
        {
            base.DrawClipData(clipToDraw, guiContent);
            DrawSound(clipToDraw.soundConfig);
        }

        /// <summary>
        /// Method that draw a button to add a single clip data to collection.
        /// </summary>
        protected override void AddClipData()
        {
            if (GUILayout.Button(Utility.addSimpleClipDataGUIContent, GUILayout.ExpandWidth(false)))
            {
                singleClips = ArrayUtility.ExpandArray(singleClips);
                foldOutController = ArrayUtility.ExpandArray(foldOutController);

                singleClips[singleClips.Length - 1] = new SingleClipData();
                foldOutController[foldOutController.Length - 1] = true;
            }
        }

        /// <summary>
        /// Method that draw a button to remove a single clip data from the collection. Returns the state of the button[Clicked or not clicked].
        /// </summary>
        /// <param name="index">Index to remove.</param>
        /// <returns>The state of the button[Clicked or not clicked].</returns>
        protected override bool RemoveClipData(int index)
        {
            if (GUILayout.Button(Utility.removeSimpleGUIContent, GUILayout.ExpandWidth(false)))
            {
                foldOutController = ArrayUtility.RemoveArrayElement(foldOutController, index);
                singleClips = ArrayUtility.RemoveArrayElement(singleClips, index);

                return true;
            }

            return false;
        }
    }
}
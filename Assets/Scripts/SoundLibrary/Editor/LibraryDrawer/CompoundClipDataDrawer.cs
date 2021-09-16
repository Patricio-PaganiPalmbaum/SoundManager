using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility = PalmaDevelops.SoundLibraryWindowUtility;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the drwing of the Compound clip data in editor window.
    /// </summary>
    public sealed class CompoundClipDataDrawer : SoundLibraryDrawer<CompoundClipData>
    {
        private CompoundClipData[] compoundClips;
        private List<bool[]> compoundClipsFoldOutController;

        public CompoundClipDataDrawer(GUISkin guiSkin, CompoundClipData[] clipData) : base(guiSkin)
        {
            compoundClips = clipData;

            compoundClipsFoldOutController = new List<bool[]>();
            foldOutController = new bool[compoundClips.Length];

            for (int i = 0; i < compoundClips.Length; i++)
            {
                compoundClipsFoldOutController.Add(new bool[compoundClips[i].soundConfigs.Length]);
            }
        }

        /// <summary>
        /// Draws the stored collection of compound clip data. Use to add/remove compound clip data or update their values.
        /// Returns the updated collection of compound clip data.
        /// </summary>
        /// <returns>The updated collection of compound clip data.</returns>
        public override CompoundClipData[] DrawClipDataLibrary()
        {
            EditorGUILayout.Space(5);
            AddClipData();
            EditorGUILayout.Space(5);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, EditorStyles.helpBox);

            for (int i = compoundClips.Length - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();

                foldOutController[i] = EditorGUILayout.Foldout(foldOutController[i], compoundClips[i].name, foldOutStyle);
                if (GUILayout.Button(Utility.clipBoardGUIContent, GUILayout.ExpandWidth(false))) { EditorGUIUtility.systemCopyBuffer = compoundClips[i].name; }

                if (RemoveClipData(i)) { continue; }

                EditorGUILayout.EndHorizontal();

                if (foldOutController[i])
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();

                    DrawCompoundClipList(compoundClips[i], i);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();

            return compoundClips;
        }

        /// <summary>
        /// Draws the compound clip data values.
        /// </summary>
        /// <param name="clipToDraw">Compound clip data to draw.</param>
        /// <param name="guiContent">GUIContent to draw with the clip data.</param>
        protected override void DrawClipData(CompoundClipData clipToDraw, GUIContent guiContent)
        {
            base.DrawClipData(clipToDraw, guiContent);
            clipToDraw.playRandom = EditorGUILayout.Toggle(Utility.playRandomGUIContent, clipToDraw.playRandom);
        }

        /// <summary>
        /// Draw the list of sound contained in the compound clip data.
        /// </summary>
        /// <param name="clipToDraw">Compound clip data to draw.</param>
        /// <param name="indexDrawing">The current index of the compound clip data drawing.</param>
        private void DrawCompoundClipList(CompoundClipData clipToDraw, int indexDrawing)
        {
            DrawClipData(clipToDraw, Utility.compoundClipNameGUIContent);

            EditorGUILayout.Space(4);
            AddClipTrack(clipToDraw, indexDrawing);
            EditorGUILayout.Space(4);

            for (int i = clipToDraw.soundConfigs.Length - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.BeginVertical();

                compoundClipsFoldOutController[indexDrawing][i] = EditorGUILayout.Foldout(compoundClipsFoldOutController[indexDrawing][i], "Clip_" + i);

                if (compoundClipsFoldOutController[indexDrawing][i])
                {
                    DrawSound(clipToDraw.soundConfigs[i]);
                }

                EditorGUILayout.EndVertical();

                if (GUILayout.Button(Utility.resetSetupValues, GUILayout.ExpandWidth(false))) { clipToDraw.soundConfigs[i].ResetValues(); }

                if (GUILayout.Button(Utility.removeClipGUIContent, GUILayout.ExpandWidth(false)))
                {
                    clipToDraw.soundConfigs = ArrayUtility.RemoveArrayElement(clipToDraw.soundConfigs, i);
                    compoundClipsFoldOutController[indexDrawing] = ArrayUtility.RemoveArrayElement(compoundClipsFoldOutController[indexDrawing], i);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// Method that draw a button to add a compound clip data to collection.
        /// </summary>
        protected override void AddClipData()
        {
            if (GUILayout.Button(Utility.addCompoundClipDataGUIContent, GUILayout.ExpandWidth(false)))
            {
                compoundClips = ArrayUtility.ExpandArray(compoundClips);
                foldOutController = ArrayUtility.ExpandArray(foldOutController);

                compoundClips[compoundClips.Length - 1] = new CompoundClipData();
                foldOutController[foldOutController.Length - 1] = true;
                compoundClipsFoldOutController.Add(new bool[0]);
            }
        }

        /// <summary>
        /// Method that draw a button to add a Sound to the compound clip data.
        /// </summary>
        /// <param name="clipDrawing">The current compound clip data drawing.</param>
        /// <param name="indexDrawing">The current index of the compound clip data drawing.</param>
        private void AddClipTrack(CompoundClipData clipDrawing, int indexDrawing)
        {
            if (GUILayout.Button(Utility.addClipGUIContent, GUILayout.ExpandWidth(false)))
            {
                clipDrawing.soundConfigs = ArrayUtility.ExpandArray(clipDrawing.soundConfigs);
                clipDrawing.soundConfigs[clipDrawing.soundConfigs.Length - 1] = new Sound();
                compoundClipsFoldOutController[indexDrawing] = ArrayUtility.ExpandArray(compoundClipsFoldOutController[indexDrawing]);
                compoundClipsFoldOutController[indexDrawing][compoundClipsFoldOutController[indexDrawing].Length - 1] = true;
            }
        }

        /// <summary>
        /// Method that draw a button to remove a compound clip data from the collection. Returns the state of the button[Clicked or not clicked].
        /// </summary>
        /// <param name="index">Index to remove.</param>
        /// <returns>The state of the button[Clicked or not clicked].</returns>
        protected override bool RemoveClipData(int index)
        {
            if (GUILayout.Button(Utility.removeCompundGUIContent, GUILayout.ExpandWidth(false)))
            {
                compoundClips = ArrayUtility.RemoveArrayElement(compoundClips, index);
                foldOutController = ArrayUtility.RemoveArrayElement(foldOutController, index);
                compoundClipsFoldOutController.RemoveAt(index);

                return true;
            }

            return false;
        }
    }
}
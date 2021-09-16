using UnityEditor;
using UnityEngine;

namespace PalmaDevelops
{
    /// <summary>
    /// Class that prevents the modifcation of the sound library by editor.
    /// </summary>
    [CustomEditor(typeof(SoundLibrary))]
    public sealed class SoundLibraryCustomEditor : Editor
    {
        /// <summary>
        /// Provides a direct access to the editor of the sound library.
        /// </summary>
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button(SoundLibraryWindowUtility.openSoundLibrary))
            {
                SoundLibraryEditorWindow.ShowWindow();
                Selection.activeObject = null;
            }
        }
    }
}
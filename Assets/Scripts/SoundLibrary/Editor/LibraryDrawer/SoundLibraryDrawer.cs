using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Utility = PalmaDevelops.SoundLibraryWindowUtility;

namespace PalmaDevelops
{
    /// <summary>
    /// Base class for handling the drawing of the clip data in the editor window.
    /// </summary>
    /// <typeparam name="T">Type of clip data to draw.</typeparam>
    public abstract class SoundLibraryDrawer<T> where T : ClipData
    {
        protected bool[] foldOutController;
        protected Vector2 scrollPosition;

        protected GUISkin skin;
        protected GUIStyle foldOutStyle;

        public SoundLibraryDrawer(GUISkin guiSkin)
        {
            skin = guiSkin;

            foldOutStyle = new GUIStyle(EditorStyles.foldout);
            foldOutStyle.normal.textColor = Color.white;
            foldOutStyle.onNormal.textColor = Color.white;
            foldOutStyle.fontStyle = FontStyle.Bold;
        }

        public abstract T[] DrawClipDataLibrary();
        protected abstract void AddClipData();
        protected abstract bool RemoveClipData(int index);

        /// <summary>
        /// Draw the base clip data values.
        /// </summary>
        /// <param name="clipToDraw">Base clip data to draw.</param>
        /// <param name="guiContent">GUIContent to draw with the clip data.</param>
        protected virtual void DrawClipData(T clipToDraw, GUIContent guiContent)
        {
            clipToDraw.name = EditorGUILayout.TextField(guiContent, clipToDraw.name);
            clipToDraw.loop = EditorGUILayout.Toggle(Utility.loopGUIContent, clipToDraw.loop);
        }

        /// <summary>
        /// Draw the sound settings.
        /// </summary>
        /// <param name="sound">Sound to draw.</param>
        protected void DrawSound(Sound sound)
        {
            sound.output = (AudioMixerGroup)EditorGUILayout.ObjectField(Utility.soundOutputGUIContent, sound.output, typeof(AudioMixerGroup), false);
            sound.clip = (AudioClip)EditorGUILayout.ObjectField(Utility.soundClipGUIContent, sound.clip, typeof(AudioClip), false);
            sound.volume = DrawSlider(Utility.soundVolumeGUIContent, sound.volume, Sound.minVolume, Sound.maxVolume);
            sound.randomVolume = DrawSlider(Utility.soundRandomVolumeGUIContent, sound.randomVolume, Sound.minRandomVolume, Sound.maxRandomVolume);
            sound.pitch = DrawSlider(Utility.soundPitchGUIContent, sound.pitch, Sound.minPitch, Sound.maxPitch);
            sound.randomPitch = DrawSlider(Utility.soundRandomPitchGUIContent, sound.randomPitch, Sound.minRandomPitch, Sound.maxRandomPitch);
            sound.stereoPan = DrawSlider(Utility.soundStereoPanGUIContent, sound.stereoPan, Sound.minStereoPan, Sound.maxStereoPan);
        }

        /// <summary>
        /// Draw a custom slider. Returns the current value of the slider.
        /// </summary>
        /// <param name="content">GUIContent of slider to display.</param>
        /// <param name="currentValue">Current value of slider.</param>
        /// <param name="min">Min value of the slider.</param>
        /// <param name="max">Max value of the slider.</param>
        /// <returns>The current value of the slider.</returns>
        private float DrawSlider(GUIContent content, float currentValue, float min, float max)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(content, GUILayout.Width(145));
            currentValue = GUILayout.HorizontalSlider(currentValue, min, max, skin.GetStyle("horizontalslider"), skin.GetStyle("horizontalsliderthumb"));
            currentValue = EditorGUILayout.FloatField((float)Math.Round(currentValue, 2), GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();

            return currentValue;
        }
    }
}
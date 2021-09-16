using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PalmaDevelops
{
    public class SoundLibraryLoaderWindow : EditorWindow
    {
        private SoundLibrary soundLibrary;

        private bool showClips;
        private List<AudioClip> clips;

        private int amountSingle;
        private int AmountCompound;
        private int minCompound;
        private int maxCompound;

        private Vector2 scrollPosition;

        [MenuItem("PalmaSM/LibraryLoader")]
        private static void ShowWindow()
        {
            GetWindow<SoundLibraryLoaderWindow>().Show();
        }

        private void OnEnable()
        {
            soundLibrary = PalmaEditorUtility.FindAssetObject<SoundLibrary>(AssetsDirectory.SOUND_LIBRARY_CREATION_PATH + ".asset");
            clips = new List<AudioClip>();
        }

        private void OnGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Library:", soundLibrary, typeof(SoundLibrary), false);
            GUI.enabled = true;

            DrawClipManagment();
            DrawBuildData();

            Repaint();
        }

        private void DrawClipManagment()
        {
            if (GUILayout.Button("+ clip")) { clips.Add(null); }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, EditorStyles.helpBox);

            showClips = EditorGUILayout.Foldout(showClips, "Clips:");

            if (showClips)
            {
                EditorGUI.indentLevel++;

                for (int i = clips.Count - 1; i >= 0; i--)
                {
                    clips[i] = (AudioClip)EditorGUILayout.ObjectField("Clip_" + i, clips[i], typeof(AudioClip), false);
                }
                EditorGUI.indentLevel--;

            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawBuildData()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            amountSingle = EditorGUILayout.IntField("Amount single:", amountSingle);
            AmountCompound = EditorGUILayout.IntField("Amount compound:", AmountCompound);
            minCompound = EditorGUILayout.IntField("Min compound:", minCompound);
            maxCompound = EditorGUILayout.IntField("Min compound:", maxCompound);

            if (GUILayout.Button("Build"))
            {
                BuildData();
            }

            EditorGUILayout.EndVertical();
        }

        private void BuildData()
        {
            soundLibrary.singleClips = new SingleClipData[0];
            soundLibrary.compoundClips = new CompoundClipData[0];

            if (clips.Count > 0)
            {
                FillSingleClips();
                FillCompoundClips();
            }

            EditorUtility.SetDirty(soundLibrary);

        }

        private void FillSingleClips()
        {
            for (int i = 0; i < amountSingle; i++)
            {
                soundLibrary.singleClips = ArrayUtility.ExpandArray(soundLibrary.singleClips);
                soundLibrary.singleClips[i] = new SingleClipData
                {
                    name = "Sound_" + i,
                    soundConfig = new Sound
                    {
                        clip = GetRandomClip(),
                        volume = UnityEngine.Random.Range(0.1f, 1f),
                        pitch = UnityEngine.Random.Range(-3f, 3f),
                        randomVolume = UnityEngine.Random.Range(0f, 0.5f),
                        randomPitch = UnityEngine.Random.Range(0f, 0.5f),
                        stereoPan = UnityEngine.Random.Range(-1f, 1f)
                    }
                };

            }
        }

        private void FillCompoundClips()
        {
            for (int i = 0; i < AmountCompound; i++)
            {
                soundLibrary.compoundClips = ArrayUtility.ExpandArray(soundLibrary.compoundClips);
                soundLibrary.compoundClips[i] = new CompoundClipData
                {
                    name = "Sound_" + i,
                    soundConfigs = new Sound[UnityEngine.Random.Range(minCompound, maxCompound + 1)]
                };

                for (int j = 0; j < soundLibrary.compoundClips[i].soundConfigs.Length; j++)
                {
                    soundLibrary.compoundClips[i].soundConfigs[j] = new Sound
                    {
                        clip = GetRandomClip(),
                        volume = UnityEngine.Random.Range(0.1f, 1f),
                        pitch = UnityEngine.Random.Range(-3f, 3f),
                        randomVolume = UnityEngine.Random.Range(0f, 0.5f),
                        randomPitch = UnityEngine.Random.Range(0f, 0.5f),
                        stereoPan = UnityEngine.Random.Range(-1f, 1f)
                    };
                }
            }
        }

        private AudioClip GetRandomClip()
        {
            return clips[UnityEngine.Random.Range(0, clips.Count)];
        }
    }
}
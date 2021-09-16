using System.Collections.Generic;
using UnityEngine;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the audio sources.
    /// </summary>
    public sealed class AudioSourceManagment : MonoBehaviour
    {
        public static AudioSourceManagment instance;

        private Stack<AudioSource> availableAudioSources;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                Initialize();
            } else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Initializes and makes the component ready for use.
        /// </summary>
        private void Initialize()
        {
            availableAudioSources = new Stack<AudioSource>();
            CreateAudioSources(5);
        }

        /// <summary>
        /// Return a AudioSource ready to use.
        /// </summary>
        /// <returns>AudioSource to use.</returns>
        public AudioSource GetAudioSource()
        {
            if (availableAudioSources.Count <= 0)
            {
                CreateAudioSources(5);
            }

            AudioSource src = availableAudioSources.Pop();
            src.loop = false;
            src.gameObject.SetActive(true);

            return src;
        }

        /// <summary>
        /// Stores an AudioSource to be reused.
        /// </summary>
        /// <param name="audioSource">AudioSource to store.</param>
        public void ReleaseAudioSource(AudioSource audioSource)
        {
            audioSource.gameObject.SetActive(false);
            availableAudioSources.Push(audioSource);
        }

        /// <summary>
        /// Create and store AudioSource for later use.
        /// </summary>
        /// <param name="amount">Number of AudioSource to create.</param>
        private void CreateAudioSources(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AudioSource src = new GameObject("AudioSourceComponent").AddComponent<AudioSource>();

                src.playOnAwake = false;

                src.gameObject.transform.parent = transform;
                src.gameObject.SetActive(false);

                availableAudioSources.Push(src);
            }
        }

    }
}
using UnityEngine;
using UnityEngine.Audio;

namespace PalmaDevelops
{
    /// <summary>
    /// Sound settings.
    /// </summary>
    [System.Serializable]
    public sealed class Sound
    {
        public AudioMixerGroup output;
        public AudioClip clip;
        public float volume;
        public float randomVolume;
        public float pitch;
        public float randomPitch;
        public float stereoPan;

        public Sound()
        {
            ResetValues();
        }

        /// <summary>
        /// Set the AudioSource according to the stored settings. Return configured AudioSource.
        /// </summary>
        /// <param name="src">AudioSource to configure,</param>
        /// <returns>Configured AudioSource.</returns>
        public AudioSource ConfigureSource(AudioSource src)
        {
            src.outputAudioMixerGroup = output;
            src.clip = clip;
            src.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
            src.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
            src.panStereo = stereoPan;

            return src;
        }

        /// <summary>
        /// Reset the configuration of the values to the default.
        /// </summary>
        public void ResetValues()
        {
            volume = 1f;
            randomVolume = 0f;
            pitch = 1f;
            randomPitch = 0f;
            stereoPan = 0f;
        }

        public const float minVolume = 0.1f;
        public const float maxVolume = 1f;

        public const float minRandomVolume = 0f;
        public const float maxRandomVolume = 0.5f;

        public const float minPitch = -3f;
        public const float maxPitch = 3f;

        public const float minRandomPitch = 0f;
        public const float maxRandomPitch = 0.5f;

        public const float minStereoPan = -1f;
        public const float maxStereoPan = 1f;

    }
}
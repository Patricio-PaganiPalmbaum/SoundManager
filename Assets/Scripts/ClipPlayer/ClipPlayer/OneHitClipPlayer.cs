using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the simultaneous reproduction of sounds.
    /// </summary>
    public sealed class OneHitClipPlayer : ClipPlayer<CompoundClipData>
    {
        private List<AudioSource> sourcesInUse;

        private float maxSoundDuration;

        public OneHitClipPlayer()
        {
            sourcesInUse = new List<AudioSource>();
        }
        
        /// <summary>
        /// Reproduces the sounds simultaneously contained in the CompoundClipData with their corresponding configuration.
        /// </summary>
        /// <param name="clipToPlay">Sounds container and its settings.</param>
        public override void Play(CompoundClipData clipToPlay)
        {
            base.Play(clipToPlay);

            AudioSource src = null;

            foreach (Sound sound in clipToPlay.soundConfigs)
            {
                src = sound.ConfigureSource(AudioSourceManagment.instance.GetAudioSource());
                src.Play();

                sourcesInUse.Add(src);
            }

            maxSoundDuration = clipToPlay.soundConfigs.Max(sound => sound.clip.length);

            if (clipToPlay.loop)
            {
                timer.ActiveTimer(maxSoundDuration, RepetPlay);
            } else
            {
                timer.ActiveTimer(maxSoundDuration, OnClipPlayedComplete);
            }
        }

        /// <summary>
        /// Pause the sounds that are playing of this OneHitClipPlayer.
        /// </summary>
        public override void Pause()
        {
            base.Pause();

            foreach (AudioSource source in sourcesInUse)
            {
                source.Pause();
            }
        }

        /// <summary>
        /// Resume the paused playback of this OneHitClipPlayer.
        /// </summary>
        public override void Resume()
        {
            base.Resume();

            foreach (AudioSource source in sourcesInUse)
            {
                source.UnPause();
            }
        }

        /// <summary>
        /// Stops playing sounds of this OneHitClipPlayer
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            ClearAudioSourcesInUse();
        }

        /// <summary>
        /// Action executed when all sounds end.
        /// </summary>
        protected override void OnClipPlayedComplete()
        {
            ClearAudioSourcesInUse();
            base.OnClipPlayedComplete();
        }

        /// <summary>
        /// Repeats the playback of sounds when they are finished. It only runs if it is configured in loop mode.
        /// </summary>
        private void RepetPlay()
        {
            foreach (AudioSource source in sourcesInUse)
            {
                source.Play();
            }

            timer.ActiveTimer(maxSoundDuration, RepetPlay);
        }

        /// <summary>
        /// Release the used AudioSource for playback.
        /// </summary>
        private void ClearAudioSourcesInUse()
        {
            foreach (AudioSource source in sourcesInUse)
            {
                source.Stop();
                AudioSourceManagment.instance.ReleaseAudioSource(source);
            }

            sourcesInUse.Clear();
        }
    }
}
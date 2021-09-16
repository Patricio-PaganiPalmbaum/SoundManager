namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the sequential reproduction of sounds.
    /// </summary>
    public sealed class SequentialClipPlayer : SimpleClipPlayer<CompoundClipData>
    {
        public bool HasNext => currentClipPlaying < soundConfigs.Length - 1;
        public bool HasPrevius => currentClipPlaying > 0;

        private Sound[] soundConfigs;

        private int currentClipPlaying;
        private bool loop;

        /// <summary>
        /// Reproduces the sounds sequentially contained in the CompoundClipData with their corresponding configuration.
        /// </summary>
        /// <param name="clipToPlay">Sounds container and its settings.</param>
        public override void Play(CompoundClipData clipToPlay)
        {
            base.Play(clipToPlay);

            sourceInUse = AudioSourceManagment.instance.GetAudioSource();
            currentClipPlaying = 0;
            loop = clipToPlay.loop;

            soundConfigs = clipToPlay.soundConfigs;

            if (clipToPlay.playRandom)
            {
                soundConfigs = soundConfigs.Shuffle();
            }

            PlayClip();
        }

        /// <summary>
        /// Internal action to set and play the current sound.
        /// </summary>
        private void PlayClip()
        {
            soundConfigs[currentClipPlaying].ConfigureSource(sourceInUse);
            timer.ActiveTimer(soundConfigs[currentClipPlaying].clip.length, OnClipEnd);

            sourceInUse.Play();
        }

        /// <summary>
        /// Play the next sound on the list.
        /// </summary>
        public void PlayNext()
        {
            if (IsPlaying && HasNext)
            {
                sourceInUse.Stop();
                currentClipPlaying++;
                PlayClip();
            }
        }

        /// <summary>
        /// Play the previous sound on the list.
        /// </summary>
        public void PlayPrevious()
        {
            if (IsPlaying && HasPrevius)
            {
                sourceInUse.Stop();
                currentClipPlaying--;
                PlayClip();
            }
        }

        /// <summary>
        /// Action that is executed when the current sound ends 
        /// and it will reproduce all the sounds in the list 
        /// and repeat the entire playback if it is in loop mode.
        /// </summary>
        private void OnClipEnd()
        {
            if (HasNext)
            {
                currentClipPlaying++;
                PlayClip();
            } else if (loop)
            {
                currentClipPlaying = 0;
                PlayClip();
            } else
            {
                OnClipPlayedComplete();
            }
        }
    }
}
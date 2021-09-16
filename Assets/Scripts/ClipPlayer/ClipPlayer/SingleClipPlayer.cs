namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the single reproduction of sounds.
    /// </summary>
    public sealed class SingleClipPlayer : SimpleClipPlayer<SingleClipData>
    {
        /// <summary>
        /// Reproduces the sound contained in the SingleClipData with their corresponding configuration.
        /// </summary>
        /// <param name="clipToPlay">Sound container and its setting.</param>
        public override void Play(SingleClipData clipToPlay)
        {
            base.Play(clipToPlay);

            sourceInUse = clipToPlay.soundConfig.ConfigureSource(AudioSourceManagment.instance.GetAudioSource());
            sourceInUse.loop = clipToPlay.loop;

            if (!clipToPlay.loop)
            {
                timer.ActiveTimer(clipToPlay.soundConfig.clip.length, OnClipPlayedComplete);
            }

            sourceInUse.Play();
        }
    }
}
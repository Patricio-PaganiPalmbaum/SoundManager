using UnityEngine;

namespace PalmaDevelops
{
    /// <summary>
    /// Base class for handling the single reproduction of sounds.
    /// </summary>
    /// <typeparam name="T">ClipData to be used.</typeparam>
    public abstract class SimpleClipPlayer<T> : ClipPlayer<T> where T : ClipData
    {
        protected AudioSource sourceInUse;

        /// <summary>
        /// Pause the current sound playing in this component.
        /// </summary>
        public override void Pause()
        {
            base.Pause();
            sourceInUse.Pause();
        }

        /// <summary>
        /// Resume the current sound playing in this component.
        /// </summary>
        public override void Resume()
        {
            base.Resume();
            sourceInUse.UnPause();
        }

        /// <summary>
        /// Stops sound playing in this component.
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            sourceInUse.Stop();
            OnClipPlayedComplete();
        }

        /// <summary>
        /// Action executed when all sounds end.
        /// </summary>
        protected override void OnClipPlayedComplete()
        {
            base.OnClipPlayedComplete();
            AudioSourceManagment.instance.ReleaseAudioSource(sourceInUse);
        }
    }
}
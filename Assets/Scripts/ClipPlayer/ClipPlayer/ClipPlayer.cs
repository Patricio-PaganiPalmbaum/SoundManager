using System;

namespace PalmaDevelops
{
    /// <summary>
    /// Base class for handling the reproduction of ClipData.
    /// </summary>
    /// <typeparam name="T">ClipData to be used.</typeparam>
    public abstract class ClipPlayer<T> where T : ClipData
    {
        private Action ReleaseCallback = delegate { };

        public string CurrentTrackPlaying { get; private set; }
        public bool IsPlaying { get; private set; }

        protected Timer timer;

        public ClipPlayer()
        {
            timer = new Timer();
        }
        
        /// <summary>
        /// Base logic to reproduce ClipData setup.
        /// </summary>
        /// <param name="clipToPlay">Clip data setup.</param>
        public virtual void Play(T clipToPlay)
        {
            CurrentTrackPlaying = clipToPlay.name;
            IsPlaying = true;
        }

        /// <summary>
        /// Base logic to pause playing clip.
        /// </summary>
        public virtual void Pause()
        {
            IsPlaying = false;
            timer.PauseTimer();
        }

        /// <summary>
        /// Base logic to resume paused clip.
        /// </summary>
        public virtual void Resume()
        {
            IsPlaying = true;
            timer.ResumeTimer();
        }

        /// <summary>
        /// Base logic to stop playing clips.
        /// </summary>
        public virtual void Stop()
        {
            IsPlaying = false;
            timer.DeactiveTimer();
        }

        /// <summary>
        /// Action setup for the reproduction end.
        /// </summary>
        /// <param name="releaseCallback"></param>
        public void SetReleaseCallback(Action releaseCallback)
        {
            ReleaseCallback = releaseCallback;
        }

        /// <summary>
        /// Action executed when all sounds end.
        /// </summary>
        protected virtual void OnClipPlayedComplete()
        {
            ReleaseCallback.Invoke();
        }
    }
}
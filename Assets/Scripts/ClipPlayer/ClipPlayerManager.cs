namespace PalmaDevelops
{
    /// <summary>
    /// Class for control and manage clip players controllers.
    /// </summary>
    public sealed class ClipPlayerManager
    {
        public static ClipPlayerManager Instance
        {
            get
            {
                if (instance == null) { instance = new ClipPlayerManager(); }
                return instance;
            }
        }

        private static ClipPlayerManager instance;

        private ClipPlayerController<SingleClipPlayer, SingleClipData> simpleClipPlayer;
        private ClipPlayerController<SequentialClipPlayer, CompoundClipData> sequentialClipPlayer;
        private ClipPlayerController<OneHitClipPlayer, CompoundClipData> oneHitClipPlayer;

        private ClipPlayerManager()
        {
            simpleClipPlayer = new ClipPlayerController<SingleClipPlayer, SingleClipData>();
            sequentialClipPlayer = new ClipPlayerController<SequentialClipPlayer, CompoundClipData>();
            oneHitClipPlayer = new ClipPlayerController<OneHitClipPlayer, CompoundClipData>();
        }

        /// <summary>
        /// Plays the requested single clip. Returns the instance that is currently playing the request.
        /// </summary>
        /// <param name="singleClipData">Single clip to play.</param>
        /// <returns>the instance that is currently playing the requested single clip.</returns>
        public SingleClipPlayer PlaySimpleClip(SingleClipData singleClipData)
        {
            return simpleClipPlayer.PlayClip(singleClipData);
        }

        /// <summary>
        /// Pause all active single clip playbacks.
        /// </summary>
        public void PauseSimpleClip()
        {
            simpleClipPlayer.Pause();
        }

        /// <summary>
        /// Pause all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseSimpleClip(string clipToPause)
        {
            simpleClipPlayer.Pause(clipToPause);
        }

        /// <summary>
        /// Resume all paused single clip playbacks.
        /// </summary>
        public void ResumeSimpleClip()
        {
            simpleClipPlayer.Resume();
        }

        /// <summary>
        /// Resume all single clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeSimpleClip(string clipToResume)
        {
            simpleClipPlayer.Resume(clipToResume);
        }

        /// <summary>
        /// Stops all active single clip playbacks.
        /// </summary>
        public void StopSimpleClip()
        {
            simpleClipPlayer.Stop();
        }

        /// <summary>
        /// Stops all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopSimpleClip(string clipToStop)
        {
            simpleClipPlayer.Stop(clipToStop);
        }

        /// <summary>
        /// Play the requested compound clip as sequentially. Returns the instance that is currently playing the request.
        /// </summary>
        /// <param name="sequentialClipData">Compound clip to play.</param>
        /// <returns>Returns the instance that is currently playing the requested sequential clip.</returns>
        public SequentialClipPlayer PlaySequentialClip(CompoundClipData sequentialClipData)
        {
            return sequentialClipPlayer.PlayClip(sequentialClipData);
        }

        /// <summary>
        /// Pause all active sequential clip playbacks.
        /// </summary>
        public void PauseSequentialClip()
        {
            sequentialClipPlayer.Pause();
        }

        /// <summary>
        /// Pause all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseSequentialClip(string clipToPause)
        {
            sequentialClipPlayer.Pause(clipToPause);
        }

        /// <summary>
        /// Resume all paused sequential clip playbacks.
        /// </summary>
        public void ResumeSequentialClip()
        {
            sequentialClipPlayer.Resume();
        }

        /// <summary>
        /// Resume all sequential clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeSequentialClip(string clipToResume)
        {
            sequentialClipPlayer.Resume(clipToResume);
        }

        /// <summary>
        /// Stops all sequential clip playbacks.
        /// </summary>
        public void StopSequentialClip()
        {
            sequentialClipPlayer.Stop();
        }

        /// <summary>
        /// Stops all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopSequentialClip(string clipToStop)
        {
            sequentialClipPlayer.Stop(clipToStop);
        }

        /// <summary>
        /// Play the requested compound clip as one hit. Returns the instance that is currently playing the request.
        /// </summary>
        /// <param name="oneHitClipData">Compound clip to play.</param>
        /// <returns> Returns the instance that is currently playing the requested.</returns>
        public OneHitClipPlayer PlayOneHitClip(CompoundClipData oneHitClipData)
        {
            return oneHitClipPlayer.PlayClip(oneHitClipData);
        }

        /// <summary>
        /// Pause all active one hit clip playbacks.
        /// </summary>
        public void PauseOneHitClip()
        {
            oneHitClipPlayer.Pause();
        }

        /// <summary>
        /// Pause all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseOneHitClip(string clipToPause)
        {
            oneHitClipPlayer.Pause(clipToPause);
        }

        /// <summary>
        /// Resume all paused one hit clip playbacks.
        /// </summary>
        public void ResumeOneHitClip()
        {
            oneHitClipPlayer.Resume();
        }

        /// <summary>
        /// Resume all one hit clip paused playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeOneHitClip(string clipToResume)
        {
            oneHitClipPlayer.Resume(clipToResume);
        }

        /// <summary>
        /// Stops all active one hit clip playbacks.
        /// </summary>
        public void StopOneHitClip()
        {
            oneHitClipPlayer.Stop();
        }

        /// <summary>
        /// Stops all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopOneHitClip(string clipToStop)
        {
            oneHitClipPlayer.Stop(clipToStop);
        }
    }
}
using System.Collections.Generic;

namespace PalmaDevelops
{
    public sealed class ClipPlayerController<T, U> where T : ClipPlayer<U>, new() where U : ClipData
    {
        private Stack<T> availableClipPlayers;

        private Dictionary<string, List<T>> playingClips;
        private Dictionary<string, List<T>> pausedClips;

        public ClipPlayerController()
        {
            Initialize(5);
        }

        /// <summary>
        /// Initializes and makes the component ready for use.
        /// </summary>
        /// <param name="initialClipPlayerAmount">Initial number of clip players to create.</param>
        private void Initialize(int initialClipPlayerAmount)
        {
            availableClipPlayers = new Stack<T>();

            playingClips = new Dictionary<string, List<T>>();
            pausedClips = new Dictionary<string, List<T>>();

            AddClipPlayer(initialClipPlayerAmount);
        }

        /// <summary>
        /// Plays the requested clip. Returns the instance that is currently playing the requested.
        /// </summary>
        /// <param name="clipData">Clip to play.</param>
        /// <returns>Returns the instance that is currently playing the requested.</returns>
        public T PlayClip(U clipData)
        {
            if (availableClipPlayers.Count <= 0) { AddClipPlayer(5); }

            if (!playingClips.ContainsKey(clipData.name))
            {
                playingClips.Add(clipData.name, new List<T>());
            }

            T clipPlayer = availableClipPlayers.Pop();
            playingClips[clipData.name].Add(clipPlayer);

            clipPlayer.Play(clipData);

            return clipPlayer;
        }

        /// <summary>
        /// Pause all active clips playbacks.
        /// </summary>
        public void Pause()
        {
            foreach (KeyValuePair<string, List<T>> item in playingClips)
            {
                PauseClips(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Pause all clips playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void Pause(string clipToPause)
        {
            if (playingClips.TryGetValue(clipToPause, out List<T> clipsToPause))
            {
                PauseClips(clipToPause, clipsToPause);
            }
        }

        /// <summary>
        /// Logic for pause all clips playbacks that match the requested key.
        /// </summary>
        /// <param name="key">Key to pause</param>
        /// <param name="clipsToPause">List of clips to pause.</param>
        private void PauseClips(string key, List<T> clipsToPause)
        {
            if (!pausedClips.ContainsKey(key))
            {
                pausedClips.Add(key, new List<T>());
            }

            foreach (T clip in clipsToPause)
            {
                clip.Pause();
                pausedClips[key].Add(clip);
            }

            playingClips[key].Clear();
        }

        /// <summary>
        /// Resume all paused clip playbacks.
        /// </summary>
        public void Resume()
        {
            foreach (KeyValuePair<string, List<T>> item in pausedClips)
            {
                ResumeClips(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Resume all clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void Resume(string clipToResume)
        {
            if (pausedClips.TryGetValue(clipToResume, out List<T> clipsToResume))
            {
                ResumeClips(clipToResume, clipsToResume);
            }
        }

        /// <summary>
        /// Logic for resume all clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="key">Key to resume.</param>
        /// <param name="clipsToResume">List of clips to resume.</param>
        private void ResumeClips(string key, List<T> clipsToResume)
        {
            foreach (T clip in clipsToResume)
            {
                clip.Resume();
                playingClips[key].Add(clip);
            }

            pausedClips[key].Clear();
        }

        /// <summary>
        /// Stops all active clip playbacks.
        /// </summary>
        public void Stop()
        {
            foreach (KeyValuePair<string, List<T>> item in playingClips)
            {
                StopClips(item.Key, item.Value);
                playingClips[item.Key].Clear();
            }

            foreach (KeyValuePair<string, List<T>> item in pausedClips)
            {
                StopClips(item.Key, item.Value);
                pausedClips[item.Key].Clear();
            }
        }

        /// <summary>
        /// Stops all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void Stop(string clipToStop)
        {
            if (playingClips.TryGetValue(clipToStop, out List<T> clipsToStop))
            {
                StopClips(clipToStop, clipsToStop);
                playingClips[clipToStop].Clear();
            }
        }

        /// <summary>
        /// Logic for stop all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="clipsToStop"></param>
        private void StopClips(string key, List<T> clipsToStop)
        {
            foreach (T clip in clipsToStop)
            {
                clip.Stop();
            }
        }

        /// <summary>
        /// Create and store a ready-to-use clip player.
        /// </summary>
        /// <param name="amount">Number of clips to create.</param>
        private void AddClipPlayer(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                T newObject = new T();
                newObject.SetReleaseCallback(() => { ReleaseClipPlayer(newObject); });
                availableClipPlayers.Push(newObject);
            }
        }

        /// <summary>
        /// Action that is executed when the clip ends. Stores the clip player and removes it from the active playlist.
        /// </summary>
        /// <param name="clipPlayer">Free clip player.</param>
        public void ReleaseClipPlayer(T clipPlayer)
        {
            playingClips[clipPlayer.CurrentTrackPlaying].Remove(clipPlayer);
            availableClipPlayers.Push(clipPlayer);
        }
    }
}
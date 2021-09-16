using System.Collections.Generic;
using UnityEngine;

namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the access to the sound library and reproduction of clips data.
    /// </summary>
    public sealed class SoundManager
    {
        public static SoundManager Instance
        {
            get
            {
                if (instance == null) { instance = new SoundManager(); }

                return instance;
            }
        }
        private static SoundManager instance;

        private Dictionary<string, SingleClipData> simpleClipMap;
        private Dictionary<string, CompoundClipData> compoundClipMap;

        private SoundManager()
        {
            simpleClipMap = new Dictionary<string, SingleClipData>();
            compoundClipMap = new Dictionary<string, CompoundClipData>();

            SoundLibrary library = Resources.Load<SoundLibrary>(AssetsDirectory.SOUND_LIBRARY_PATH);

            if (!library) { throw new System.Exception("No sound library found "); }

            RegisterClipData(library.singleClips, simpleClipMap);
            RegisterClipData(library.compoundClips, compoundClipMap);
        }

        #region SingleClipManagment

        /// <summary>
        /// Plays the requested single clip. Returns a SingleClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="singleClipToPlay">Single clip to play.</param>
        /// <returns>SingleClipPlayer instance that is currently playing the request.</returns>
        public SingleClipPlayer PlaySingleClip(ESingleClip singleClipToPlay)
        {
            return PlaySingleClip(singleClipToPlay.ToString());
        }

        /// <summary>
        /// Plays the requested single clip. Returns a SingleClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="clipDataName">Single clip to play.</param>
        /// <returns>SingleClipPlayer instance that is currently playing the request.</returns>
        public SingleClipPlayer PlaySingleClip(string clipDataName)
        {
            if (simpleClipMap.ContainsKey(clipDataName))
            {
                return ClipPlayerManager.Instance.PlaySimpleClip(simpleClipMap[clipDataName]);
            }

            return null;
        }

        /// <summary>
        /// Pause all active single clip playbacks.
        /// </summary>
        public void PauseSingleClip()
        {
            ClipPlayerManager.Instance.PauseSimpleClip();
        }

        /// <summary>
        /// Pause all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="singleClipToPause">Key to pause.</param>
        public void PauseSingleClip(ESingleClip singleClipToPause)
        {
            PauseSingleClip(singleClipToPause.ToString());
        }

        /// <summary>
        /// Pause all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseSingleClip(string clipToPause)
        {
            ClipPlayerManager.Instance.PauseSimpleClip(clipToPause);
        }

        /// <summary>
        /// Resume all paused single clip playbacks.
        /// </summary>
        public void ResumeSingleClip()
        {
            ClipPlayerManager.Instance.ResumeSimpleClip();
        }

        /// <summary>
        /// Resume all single clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="singleClipToResume">Key to resume.</param>
        public void ResumeSingleClip(ESingleClip singleClipToResume)
        {
            ResumeSingleClip(singleClipToResume.ToString());
        }

        /// <summary>
        /// Resume all single clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeSingleClip(string clipToResume)
        {
            ClipPlayerManager.Instance.ResumeSimpleClip(clipToResume);
        }

        /// <summary>
        /// Stops all active single clip playbacks.
        /// </summary>
        public void StopSingleClip()
        {
            ClipPlayerManager.Instance.StopSimpleClip();
        }

        /// <summary>
        /// Stops all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="singleClipToStop">Key to stop.</param>
        public void StopSingleClip(ESingleClip singleClipToStop)
        {
            StopSingleClip(singleClipToStop.ToString());
        }

        /// <summary>
        /// Stops all single clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopSingleClip(string clipToStop)
        {
            ClipPlayerManager.Instance.StopSimpleClip(clipToStop);
        }

        #endregion

        #region SequentialClipManagment

        /// <summary>
        /// Play the requested compound clip as sequentially. Returns a SequentialClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="coumpodClipToPlay">Compound clip to play.</param>
        /// <returns>SequentialClipPlayer instance that is currently playing the request.</returns>
        public SequentialClipPlayer PlaySequentialClip(ECompoundClip coumpodClipToPlay)
        {
            return PlaySequentialClip(coumpodClipToPlay.ToString());
        }

        /// <summary>
        /// Play the requested compound clip as sequentially. Returns a SequentialClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="clipDataName">Compound clip to play.</param>
        /// <returns>SequentialClipPlayer instance that is currently playing the request.</returns>
        public SequentialClipPlayer PlaySequentialClip(string clipDataName)
        {
            if (compoundClipMap.ContainsKey(clipDataName))
            {
                return ClipPlayerManager.Instance.PlaySequentialClip(compoundClipMap[clipDataName]);
            }

            return null;
        }

        /// <summary>
        /// Pause all active sequential clip playbacks.
        /// </summary>
        public void PauseSequentialClip()
        {
            ClipPlayerManager.Instance.PauseSequentialClip();
        }

        /// <summary>
        /// Pause all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="coumpodClipToPause">Key to pause.</param>
        public void PauseSequentialClip(ECompoundClip coumpodClipToPause)
        {
            PauseSequentialClip(coumpodClipToPause.ToString());
        }

        /// <summary>
        /// Pause all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseSequentialClip(string clipToPause)
        {
            ClipPlayerManager.Instance.PauseSequentialClip(clipToPause);
        }

        /// <summary>
        /// Resume all paused sequential clip playbacks.
        /// </summary>
        public void ResumeSequentialClip()
        {
            ClipPlayerManager.Instance.ResumeSequentialClip();
        }

        /// <summary>
        /// Resume all sequential clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="coumpodClipToResume">Key to resume.</param>
        public void ResumeSequentialClip(ECompoundClip coumpodClipToResume)
        {
            ResumeSequentialClip(coumpodClipToResume.ToString());
        }

        /// <summary>
        /// Resume all sequential clip paused playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeSequentialClip(string clipToResume)
        {
            ClipPlayerManager.Instance.ResumeSequentialClip(clipToResume);
        }

        /// <summary>
        /// Stops all sequential clip playbacks.
        /// </summary>
        public void StopSequentialClip()
        {
            ClipPlayerManager.Instance.StopSequentialClip();
        }

        /// <summary>
        /// Stops all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="coumpodClipToStop">Key to stop.</param>
        public void StopSequentialClip(ECompoundClip coumpodClipToStop)
        {
            StopSequentialClip(coumpodClipToStop.ToString());
        }

        /// <summary>
        /// Stops all sequential clip playbacks that match the requested key.
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopSequentialClip(string clipToStop)
        {
            ClipPlayerManager.Instance.StopSequentialClip(clipToStop);
        }

        #endregion

        #region OneHitClipManagment

        /// <summary>
        /// Play the requested compound clip as one hit. Returns a OneHitClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="coumpodClipToPlay">Compound clip to play.</param>
        /// <returns>OneHitClipPlayer instance that is currently playing the request.</returns>
        public OneHitClipPlayer PlayOneHitClip(ECompoundClip coumpodClipToPlay)
        {
            return PlayOneHitClip(coumpodClipToPlay.ToString());
        }

        /// <summary>
        /// Play the requested compound clip as one hit. Returns a OneHitClipPlayer instance that is currently playing the request.
        /// </summary>
        /// <param name="clipDataName">Compound clip to play.</param>
        /// <returns>OneHitClipPlayer instance that is currently playing the request.</returns>
        public OneHitClipPlayer PlayOneHitClip(string clipDataName)
        {
            if (compoundClipMap.ContainsKey(clipDataName))
            {
                return ClipPlayerManager.Instance.PlayOneHitClip(compoundClipMap[clipDataName]);
            }

            return null;
        }

        /// <summary>
        /// Pause all active one hit clip playbacks.
        /// </summary>
        public void PauseOneHitClip()
        {
            ClipPlayerManager.Instance.PauseOneHitClip();
        }

        /// <summary>
        /// Pause all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="coumpodClipToPause">Key to pause.</param>
        public void PauseOneHitClip(ECompoundClip coumpodClipToPause)
        {
            PauseOneHitClip(coumpodClipToPause.ToString());
        }

        /// <summary>
        /// Pause all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToPause">Key to pause.</param>
        public void PauseOneHitClip(string clipToPause)
        {
            ClipPlayerManager.Instance.PauseOneHitClip(clipToPause);
        }

        /// <summary>
        /// Resume all paused one hit clip playbacks.
        /// </summary>
        public void ResumeOneHitClip()
        {
            ClipPlayerManager.Instance.ResumeOneHitClip();
        }

        /// <summary>
        /// Resume all one hit clip paused playbacks that match the requested key. 
        /// </summary>
        /// <param name="coumpodClipToResume">Key to resume.</param>
        public void ResumeOneHitClip(ECompoundClip coumpodClipToResume)
        {
            ResumeOneHitClip(coumpodClipToResume.ToString());
        }

        /// <summary>
        /// Resume all one hit clip paused playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToResume">Key to resume.</param>
        public void ResumeOneHitClip(string clipToResume)
        {
            ClipPlayerManager.Instance.ResumeOneHitClip(clipToResume);
        }

        /// <summary>
        /// Stops all active one hit clip playbacks.
        /// </summary>
        public void StopOneHitClip()
        {
            ClipPlayerManager.Instance.StopOneHitClip();
        }

        /// <summary>
        /// Stops all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="coumpodClipToStop">Key to stop.</param>
        public void StopOneHitClip(ECompoundClip coumpodClipToStop)
        {
            StopOneHitClip(coumpodClipToStop.ToString());
        }

        /// <summary>
        /// Stops all one hit clip playbacks that match the requested key. 
        /// </summary>
        /// <param name="clipToStop">Key to stop.</param>
        public void StopOneHitClip(string clipToStop)
        {
            ClipPlayerManager.Instance.StopOneHitClip(clipToStop);
        }

        #endregion

        /// <summary>
        /// Pause playback of all clips.
        /// </summary>
        public void PauseAll()
        {
            ClipPlayerManager.Instance.PauseSimpleClip();
            ClipPlayerManager.Instance.PauseSequentialClip();
            ClipPlayerManager.Instance.PauseOneHitClip();
        }

        /// <summary>
        /// Resume playback of all clips.
        /// </summary>
        public void ResumeAll()
        {
            ClipPlayerManager.Instance.ResumeSimpleClip();
            ClipPlayerManager.Instance.ResumeSequentialClip();
            ClipPlayerManager.Instance.ResumeOneHitClip();
        }

        /// <summary>
        /// Stops playback of all clips.
        /// </summary>
        public void StopAll()
        {
            ClipPlayerManager.Instance.StopSimpleClip();
            ClipPlayerManager.Instance.StopSequentialClip();
            ClipPlayerManager.Instance.StopOneHitClip();
        }

        /// <summary>
        /// Method that records the collection of clips granted in a dictionary.
        /// </summary>
        /// <typeparam name="T">Type of clip data.</typeparam>
        /// <param name="data">Collection of clip data to register in diccionary</param>
        /// <param name="registerMap">Dictionary to register the clip data collection.</param>
        private void RegisterClipData<T>(T[] data, Dictionary<string, T> registerMap) where T : ClipData
        {
            foreach (T clipData in data)
            {
                registerMap.Add(clipData.name, clipData);
            }
        }
    }
}
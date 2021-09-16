namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the single clip data setup.
    /// </summary>
    [System.Serializable]
    public sealed class SingleClipData : ClipData
    {
        public Sound soundConfig;

        public SingleClipData()
        {
            name = "noname";
            soundConfig = new Sound();
        }
    }
}
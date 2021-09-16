namespace PalmaDevelops
{
    /// <summary>
    /// Class for handling the compound clip data setup.
    /// </summary>
    [System.Serializable]
    public sealed class CompoundClipData : ClipData
    {
        public Sound[] soundConfigs;
        public bool playRandom;

        public CompoundClipData()
        {
            name = "noname";
            soundConfigs = new Sound[0];
        }
    }
}
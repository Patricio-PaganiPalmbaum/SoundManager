using UnityEngine;

namespace PalmaDevelops
{
    public sealed class SoundLibrary : ScriptableObject
    {
        public SingleClipData[] singleClips;
        public CompoundClipData[] compoundClips;

        public SoundLibrary()
        {
            singleClips = new SingleClipData[0];
            compoundClips = new CompoundClipData[0];
        }
    }
}
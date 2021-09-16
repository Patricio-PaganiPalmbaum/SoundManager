using UnityEngine;

namespace PalmaDevelops
{
    public static class SoundLibraryWindowUtility
    {
        public static readonly GUIContent titleWindowGUIContent = new GUIContent("Palma Sound Library");

        public static readonly GUIContent toolBarSimpleGUIContent = new GUIContent("Simple Clips Data", "Collection of simple clips data.");
        public static readonly GUIContent toolBarCompoundGUIContent = new GUIContent("Compound Clips Data", "Collection of Compound clips data.");
        public static readonly GUIContent[] toolBarOptions = new GUIContent[] { toolBarSimpleGUIContent, toolBarCompoundGUIContent };

        public static readonly GUIContent singleClipNameGUIContent = new GUIContent("SingleClipDataName:", "Single clip data identifier.");
        public static readonly GUIContent compoundClipNameGUIContent = new GUIContent("CompoundClipDataName:", "Compound clip data identifier.");

        public static readonly GUIContent loopGUIContent = new GUIContent("Loop:", "Set the clip to loop.");
        public static readonly GUIContent playRandomGUIContent = new GUIContent("Play Random:", "Set the clip to play random.");

        public static readonly GUIContent soundOutputGUIContent = new GUIContent("Output:", "Set whether the sound should play through an Audio Mixer first or directly to the Audio Listener.");
        public static readonly GUIContent soundClipGUIContent = new GUIContent("Clip:", "The AudioClip asset played");
        public static readonly GUIContent soundVolumeGUIContent = new GUIContent("Volume:", "Sets the overall volume of the sound.");
        public static readonly GUIContent soundRandomVolumeGUIContent = new GUIContent("Random Volume:", "Every play add a random volume variation [between -RandomVolume/2 & RandomVolume/2]");
        public static readonly GUIContent soundPitchGUIContent = new GUIContent("Pitch:", "Sets the frequency of the sound. Use this to slow down or speed up the sound.");
        public static readonly GUIContent soundRandomPitchGUIContent = new GUIContent("Random Pitch:", "Every play add a random pitch variation [between -RandomPitch/2 & RandomPitch/2]");
        public static readonly GUIContent soundStereoPanGUIContent = new GUIContent("Stereo Pan:", "Only valid for Mono andStereo AudioClips. Mono sounds will be panned at constant power left and right. Stereo sounds will have each left/right value faded up and down acoording to the specified pan value.");

        public static readonly GUIContent saveGUIContent = new GUIContent("Save library", "Save library asset to avoid data loss.");
        public static readonly GUIContent buildGUIContent = new GUIContent("Build library", "It performs a validation of the configurations and creates the necessary files for the correct functioning of the sound system.");

        public static readonly GUIContent addSimpleClipDataGUIContent = new GUIContent("+ Clip data", "Add single clip data to the library.");
        public static readonly GUIContent addCompoundClipDataGUIContent = new GUIContent("+ Clip data", "Add compound clip data to the library.");
        public static readonly GUIContent addClipGUIContent = new GUIContent("+ Clip", "Add new clip setup to the compound data.");

        public static readonly GUIContent resetSetupValues = new GUIContent("Reset","Reset the setup values to default.");

        public static readonly GUIContent removeSimpleGUIContent = new GUIContent(" X ", "Remove single clip data from the library.");
        public static readonly GUIContent removeCompundGUIContent = new GUIContent(" X ", "Remove compound clip data from the library.");
        public static readonly GUIContent removeClipGUIContent = new GUIContent(" X ", "Remove clip setup from the compound data.");

        public static readonly GUIContent clipBoardGUIContent = new GUIContent("Copy", "copy to cliboard the name of the clip data");

        public static readonly GUIContent openSoundLibrary = new GUIContent("Open sound library", "Open sound library editor window.");

    }
}
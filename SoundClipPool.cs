using UnityEngine;

[CreateAssetMenu(fileName = "SoundClipPool", menuName = "Audio/SoundClipPool", order = 1)]
public class SoundClipPool : ScriptableObject
{
    public AudioClip[] soundClips; // Array of sound clips
}

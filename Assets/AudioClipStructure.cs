using System;
using UnityEngine;

[Serializable]
public class AudioClipStructure
{
    public string alias;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volumeScale = 1f;
}

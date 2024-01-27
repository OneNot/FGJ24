using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Serializable]
    private class AudioClipStruct
    {
        public string alias;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volumeScale = 1f;
    }

    [SerializeField]
    private List<AudioClipStruct> rubberChickenNoises = new List<AudioClipStruct>(),
    dashNoises = new List<AudioClipStruct>(),
    shoeNoises = new List<AudioClipStruct>();

    private AudioSource auS;

    private void Awake()
    {
        auS = GetComponent<AudioSource>();
    }


    public void PlayRandomRubberChicken()
    {
        int index = UnityEngine.Random.Range(0, rubberChickenNoises.Count);
        auS.PlayOneShot(rubberChickenNoises[index].clip, rubberChickenNoises[index].volumeScale);
    }

    public void PlayRandomDash()
    {
        int index = UnityEngine.Random.Range(0, dashNoises.Count);
        auS.PlayOneShot(dashNoises[index].clip, dashNoises[index].volumeScale);
    }

    public void PlayRandomShoe()
    {
        int index = UnityEngine.Random.Range(0, shoeNoises.Count);
        auS.PlayOneShot(shoeNoises[index].clip, shoeNoises[index].volumeScale);
    }
}

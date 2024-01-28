using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private List<AudioClipStructure> rubberChickenNoises = new List<AudioClipStructure>(),
    dashNoises = new List<AudioClipStructure>(),
    shoeNoises = new List<AudioClipStructure>();

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

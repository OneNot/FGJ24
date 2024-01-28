using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLegGuyAudio : MonoBehaviour
{
    [SerializeField]
    private float delayMin, delayMax;

    [SerializeField]
    private List<AudioClipStructure> laughNoises = new List<AudioClipStructure>();
    private AudioSource auS;

    private void Awake()
    {
        auS = GetComponent<AudioSource>();
        StartCoroutine(PlayLaughAfterRandomDelay());
    }

    public void PlayRandomLaugh()
    {
        int index = UnityEngine.Random.Range(0, laughNoises.Count);
        auS.PlayOneShot(laughNoises[index].clip, laughNoises[index].volumeScale);
    }

    private IEnumerator PlayLaughAfterRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        PlayRandomLaugh();
        StartCoroutine(PlayLaughAfterRandomDelay());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

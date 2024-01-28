using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightLifterAudio : MonoBehaviour
{
    [SerializeField]
    private List<AudioClipStructure> dropNoises = new List<AudioClipStructure>(),
    liftNoises = new List<AudioClipStructure>();

    private AudioSource auS;

    private void Awake()
    {
        auS = GetComponent<AudioSource>();
    }

    public void PlayRandomDrop()
    {
        int index = UnityEngine.Random.Range(0, dropNoises.Count);
        StartCoroutine(DelayedPlayOneShot(dropNoises[index].clip, dropNoises[index].volumeScale, 0.5f));
    }

    private IEnumerator DelayedPlayOneShot(AudioClip clip, float volumeScale, float delay)
    {
        yield return new WaitForSeconds(delay);
        auS.PlayOneShot(clip, volumeScale);
    }

    public void PlayRandomLift()
    {
        int index = UnityEngine.Random.Range(0, liftNoises.Count);
        auS.PlayOneShot(liftNoises[index].clip, liftNoises[index].volumeScale);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

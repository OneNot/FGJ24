using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLegGuyAudioAndFace : MonoBehaviour
{
    [SerializeField]
    private float delayMin, delayMax;

    [SerializeField]
    private List<AudioClipStructure> laughNoises = new List<AudioClipStructure>();
    [SerializeField]
    private Sprite faceNeutral, faceLaugh;
    private AudioSource auS;
    [SerializeField]
    private SpriteRenderer faceRenderer;

    private void Awake()
    {
        auS = GetComponent<AudioSource>();
        faceRenderer.sprite = faceNeutral;
        StartCoroutine(PlayLaughAfterRandomDelay());
    }

    public void PlayLaugh(AudioClipStructure clipStructure)
    {
        auS.PlayOneShot(clipStructure.clip, clipStructure.volumeScale);
    }

    private IEnumerator PlayLaughAfterRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        AudioClipStructure clipStructure = laughNoises[Random.Range(0, laughNoises.Count)];
        StartCoroutine(LaughFace(clipStructure.clip.length));
        PlayLaugh(clipStructure);
        StartCoroutine(PlayLaughAfterRandomDelay());
    }

    private IEnumerator LaughFace(float time)
    {
        faceRenderer.sprite = faceLaugh;
        yield return new WaitForSeconds(time);
        faceRenderer.sprite = faceNeutral;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaningBackAndForth : MonoBehaviour
{
    [SerializeField]
    private float maxLeanAngle = 20f, leanSmoothTime = 0.3f;
    [SerializeField]
    private float leanRotationCheckMarginOfError = 0.01f;

    private float currentZRotationTarget;
    private float velocity;
    private bool leaningPositive;

    private void Awake()
    {
        leaningPositive = Random.Range(0, 2) == 1;
        RandomizeNewRotationTarget();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y,
            Mathf.SmoothDampAngle(ConvertStupidRotation(transform.localRotation.eulerAngles.z), currentZRotationTarget, ref velocity, leanSmoothTime));

        if (Mathf.Abs(currentZRotationTarget - ConvertStupidRotation(transform.localRotation.eulerAngles.z)) <= leanRotationCheckMarginOfError)
            RandomizeNewRotationTarget();
    }


    private void RandomizeNewRotationTarget()
    {
        if (leaningPositive)
            currentZRotationTarget = Random.Range(-maxLeanAngle, currentZRotationTarget);
        else
            currentZRotationTarget = Random.Range(currentZRotationTarget, maxLeanAngle);

        leaningPositive = !leaningPositive;
    }

    private float ConvertStupidRotation(float stupidRotation)
    {
        if (stupidRotation > 180f)
            return stupidRotation - 360f;
        else return stupidRotation;
    }
}

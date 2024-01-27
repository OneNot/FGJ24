using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class TutuBear : MonoBehaviour
{
    [SerializeField]
    private Transform bodyPivotTransform, ballTransform;
    [SerializeField]
    private float maxLeanAngle = 20f, leanSmoothTime = 0.3f;
    [SerializeField]
    private float leanRotationCheckMarginOfError = 0.01f;

    [SerializeField]
    private float ballRotationSpeed = 40000f;

    private float currentZRotationTarget;
    private float velocity;
    private bool leaningPositive;
    private SplineFollow splineFollow;

    private void Awake()
    {
        splineFollow = GetComponent<SplineFollow>();
        leaningPositive = Random.Range(0, 2) == 1;
        RandomizeNewRotationTarget();
    }

    private void Update()
    {
        bodyPivotTransform.localRotation = Quaternion.Euler(bodyPivotTransform.localRotation.eulerAngles.x, bodyPivotTransform.localRotation.eulerAngles.y,
            Mathf.SmoothDampAngle(ConvertStupidRotation(bodyPivotTransform.localRotation.eulerAngles.z), currentZRotationTarget, ref velocity, leanSmoothTime));

        if (Mathf.Abs(currentZRotationTarget - ConvertStupidRotation(bodyPivotTransform.localRotation.eulerAngles.z)) <= leanRotationCheckMarginOfError)
            RandomizeNewRotationTarget();

        ballTransform.Rotate(ballTransform.forward, Time.deltaTime * ballRotationSpeed * splineFollow.finalMoveSpeed);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FlipAtEndsOfSpline : MonoBehaviour
{
    private enum XDirection
    {
        Left,
        Right
    }

    private SplineAnimate splineAnimate;
    [SerializeField]
    private XDirection splinePositiveDirection = XDirection.Right, naturalCharacterFacingDirection = XDirection.Right;
    [SerializeField]
    private float characterNaturalYRotation;

    private bool isMovingInPositiveDirection;
    private bool isFacingPositive;

    private float lastFrameSplineNormalizedTime = 0f;

    private void Awake()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        isFacingPositive = splinePositiveDirection == naturalCharacterFacingDirection;
    }

    private void Update()
    {
        isMovingInPositiveDirection = splineAnimate.NormalizedTime > lastFrameSplineNormalizedTime;
        lastFrameSplineNormalizedTime = splineAnimate.NormalizedTime;

        Debug.Log("isMovingInPositiveDirection" + isMovingInPositiveDirection);

        if (isMovingInPositiveDirection && !isFacingPositive)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                naturalCharacterFacingDirection == splinePositiveDirection ? characterNaturalYRotation : characterNaturalYRotation + 180f,
                transform.localRotation.eulerAngles.z);
            isFacingPositive = true;
        }
        else if (!isMovingInPositiveDirection && isFacingPositive)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                naturalCharacterFacingDirection != splinePositiveDirection ? characterNaturalYRotation : characterNaturalYRotation + 180f,
                transform.localRotation.eulerAngles.z);
            isFacingPositive = false;
        }
    }
}

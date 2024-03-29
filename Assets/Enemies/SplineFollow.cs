using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Spline = UnityEngine.Splines.Spline;

public class SplineFollow : MonoBehaviour
{
    private enum MoveDirection
    {
        Positive,
        Negative
    }

    [SerializeField]
    private SplineContainer splineContainerToFollow;
    private Spline splineToFollow;

    [SerializeField]
    private float moveSpeed = 10f;
    private float realMoveSpeed;

    [SerializeField]
    private AnimationCurve accelerationCurve = AnimationCurve.Linear(0, 1f, 1, 1f);

    [SerializeField]
    [Range(0f, 1f)]
    private float initialPositionOnSpline = 0;
    private float currentPositionOnSpline;

    [SerializeField]
    private MoveDirection initialMoveDirection = MoveDirection.Positive;

    private int moveDirection = 1;
    [HideInInspector]
    public float finalMoveSpeed;


    private void Awake()
    {
        splineToFollow = splineContainerToFollow.Spline; //TODO: Figure out the random nullreference error from this line despite working just fine
        moveDirection = initialMoveDirection == MoveDirection.Positive ? 1 : -1;
        realMoveSpeed = moveSpeed / splineToFollow.GetLength();

        transform.position = GetWorldPointFromSplinePoint(initialPositionOnSpline);
        currentPositionOnSpline = initialPositionOnSpline;
    }


    private void Update()
    {
        float acceleration = accelerationCurve.Evaluate(currentPositionOnSpline);

        finalMoveSpeed = Time.deltaTime * realMoveSpeed * moveDirection * acceleration;
        //get 0-1 position on spline from current 0-1 position on spline +/- move
        float targetPositionOnSpline = Mathf.Clamp01(currentPositionOnSpline + finalMoveSpeed);

        transform.position = GetWorldPointFromSplinePoint(targetPositionOnSpline);

        //update current 0-1 position
        currentPositionOnSpline = targetPositionOnSpline;
        //if after movement, we are at the end or the beginning, flip 0-1 move direction
        if (currentPositionOnSpline == 1 || currentPositionOnSpline == 0)
            moveDirection *= -1;
    }

    private Vector3 GetWorldPointFromSplinePoint(float pointOnSpline)
    {
        Vector3 newPosLocal = splineToFollow.EvaluatePosition(pointOnSpline);
        return splineContainerToFollow.transform.position + newPosLocal;
    }
}

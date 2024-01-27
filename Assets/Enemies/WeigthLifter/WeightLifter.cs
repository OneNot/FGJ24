using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class WeightLifter : MonoBehaviour
{

    private enum WeightMoveDirection
    {
        Top,
        Bottom
    }

    [SerializeField]
    private Transform weightTransform;

    [SerializeField]
    private Vector3 weightTopLocalPosition, weightBottomLocalPosition;
    [SerializeField]
    private float weightMoveSmoothTimeUp = 0.3f, weightMoveSmoothTimeDown = 0f;

    private Vector3 weightVelocity = Vector3.zero;
    private WeightMoveDirection currentWeightMoveDirection;
    private Vector3 currentWeightTargetLocalPosition;
    private float currentMoveSmoothTime;

    [SerializeField]
    private float weightPositionCheckMarginOfError = 0.01f;


    [SerializeField]
    private SpriteRenderer faceSpriteRenderer;
    [SerializeField]
    private Sprite faceNeutral, faceStruggle;
    [SerializeField]
    private float distanceFromTopEyesClosed = 1f;


    private void Awake()
    {
        currentWeightMoveDirection = WeightMoveDirection.Top;
        currentWeightTargetLocalPosition = weightTopLocalPosition;
        currentMoveSmoothTime = weightMoveSmoothTimeUp;
    }

    private void Update()
    {
        //if at top, target bottom
        if (Vector3.Distance(weightTransform.localPosition, weightTopLocalPosition) <= weightPositionCheckMarginOfError)
        {
            currentWeightMoveDirection = WeightMoveDirection.Bottom;
            currentWeightTargetLocalPosition = weightBottomLocalPosition;
            currentMoveSmoothTime = weightMoveSmoothTimeDown;
        }
        //if at bottom, target top
        else if (Vector3.Distance(weightTransform.localPosition, weightBottomLocalPosition) <= weightPositionCheckMarginOfError)
        {
            currentWeightMoveDirection = WeightMoveDirection.Top;
            currentWeightTargetLocalPosition = weightTopLocalPosition;
            currentMoveSmoothTime = weightMoveSmoothTimeUp;
        }

        weightTransform.localPosition = Vector3.SmoothDamp(weightTransform.localPosition, currentWeightTargetLocalPosition, ref weightVelocity, currentMoveSmoothTime);

        if (currentWeightMoveDirection == WeightMoveDirection.Top && Vector3.Distance(weightTransform.localPosition, weightTopLocalPosition) <= distanceFromTopEyesClosed)
        {
            if (faceSpriteRenderer.sprite != faceStruggle)
                faceSpriteRenderer.sprite = faceStruggle;
        }
        else if (faceSpriteRenderer.sprite != faceNeutral)
            faceSpriteRenderer.sprite = faceNeutral;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Rendering;

public class BallRotate : MonoBehaviour
{
    [SerializeField]
    private SplineFollow splineFollower;
    [SerializeField]
    private float ballRotationSpeed = 40000f;

    private void Update()
    {
        transform.Rotate(transform.forward, Time.deltaTime * ballRotationSpeed * splineFollower.finalMoveSpeed);
    }
}

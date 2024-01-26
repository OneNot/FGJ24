using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;
    [SerializeField]
    private float smoothTime = 0.3f;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        if (transform.position != followTarget.position + offset)
            transform.position = Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref velocity, smoothTime);
    }
}

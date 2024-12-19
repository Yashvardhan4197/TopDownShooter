using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;
    [SerializeField] Transform target;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothedPos=Vector3.Lerp(targetPos, offset, speed*Time.deltaTime);
        transform.position = smoothedPos;
    }
}

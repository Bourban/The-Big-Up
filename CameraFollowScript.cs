using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        //offset.y = 0.1f;
        offset.z = -1;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position, target.transform.position, smoothSpeed);
            transform.position = smoothPos + offset;
        }
    }
}

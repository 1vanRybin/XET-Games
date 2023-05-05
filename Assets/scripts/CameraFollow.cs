using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        if (target == null) return;
        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target == null) return;

        var pos = target.position;
        //if (pos.y > -1.536252 && pos.x < 5.192284 && pos.y< 52.3863 && pos.x > -37.43445)
        {
            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 start;
    [SerializeField] private bool isForward;

    private float progress;

    private void Start()
    {
        transform.position = pointA.position;

        start = pointA.position;
        target = pointB.position;

        isForward = true;
        progress = 0f;
    }

    private void Update()
    {
        progress += Time.deltaTime * moveSpeed;

        transform.position = Vector3.LerpUnclamped(start, target, progress);

        if(progress >= 1f)
        {
            ChangeTarget(isForward);
        }
    }

    private void ChangeTarget(bool isForward)
    {
        if(isForward)
        {
            start = pointA.position;
            target = pointB.position;
        }
        else
        {
            start = pointB.position;
            target = pointA.position;
        }

        this.isForward = !isForward;
        progress = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}

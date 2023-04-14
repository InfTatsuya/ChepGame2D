using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0f, 1.5f, -10f);

    private Transform player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = player.position + offset;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject hitEffect;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        rb.velocity = transform.right * moveSpeed;

        Invoke(nameof(OnDespawn), 4f);
    }

    private void OnDespawn()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitEffect, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] float airControl = 0.3f;

    [SerializeField] Kunai kunaiPrefab;
    [SerializeField] Transform spawnPoint;

    [SerializeField] GameObject attackArea;

    private Rigidbody2D rb;
    private Vector2 moveVector = new Vector2();

    private bool isPressJump;
    private bool isPressAttack;
    private bool isPressThrow;

    private bool isGrounded;
    private bool isJumping;
    private bool isAttacking;
    private bool isThrowing;
    private bool isDead;

    private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetSavePoint(transform.position);

        UIManager.Instance.onAttackButtonDown += UIManager_onAttackButtonDown;
        UIManager.Instance.onJumpButtonDown += UIManager_onJumpButtonDown;
        UIManager.Instance.onThrownButtonDown += UIManager_onThrowButtonDown;

        coin = PlayerPrefs.GetInt(UIManager.COIN_KEY, 0);
        UIManager.Instance.SetCoinText(coin);
    }

    private void UIManager_onAttackButtonDown(object sender, EventArgs e)
    {
        isPressAttack = true;
    }

    private void UIManager_onJumpButtonDown(object sender, EventArgs e)
    {
        isPressJump = true;
    }

    private void UIManager_onThrowButtonDown(object sender, EventArgs e)
    {
        isPressThrow = true;
    }

    public override void OnInit()
    {
        base.OnInit();

        isDead = false;
        isAttacking = false;
        isThrowing = false;

        transform.position = savePoint;
        ChangeAnim("idle");
        DeactiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        OnInit();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        //moveVector.x = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckOnGround();

        if (isGrounded)
        {
            if (isAttacking || isThrowing || isJumping)
            {
                return;
            }

            if (moveVector != Vector2.zero)
            {
                Move(1f); // fully control when on the ground
            }
            else
            {
                Idle();
            }

            if (isPressAttack)
            {
                isPressAttack = false;
                Attack();
            }
            if(isPressThrow)
            {
                isPressThrow = false;
                Throw();
            }

            if (isPressJump)
            {
                isPressJump = false;
                Jump();
            }
        }
        else
        {
            if(isPressAttack || isPressThrow)
            {
                isPressAttack = false;
                isPressThrow = false;
            }

            if(rb.velocity.y < 0.1f)
            {
                isJumping = false;
                Falling();
            }

            if(moveVector != Vector2.zero)
            {
                Move(airControl);
            }
        }
    }

    private void Falling()
    {
        ChangeAnim("fall");
    }

    private void Idle()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("idle");
    }

    private void Move(float controlFraction)
    {
        if (moveVector.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2(moveVector.x * moveSpeed * controlFraction, rb.velocity.y);

        if (controlFraction < 1f) return;
        ChangeAnim("run");
    }

    private void Attack()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        isAttacking = true;
        ChangeAnim("attack");

        ActiveAttack();
        Invoke(nameof(DeactiveAttack), 0.5f);
    }

    private void Throw()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        isThrowing = true;
        ChangeAnim("throw");

        Instantiate(kunaiPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void Jump()
    {
        isGrounded = false;
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce);
        ChangeAnim("jump");
    }

    protected override void OnDeath()   
    {
        base.OnDeath();
        isDead = true;
    }

    private bool CheckOnGround()
    {
        float halfSize = GetComponent<CapsuleCollider2D>().size.y / 2 - 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down * halfSize, Vector2.down, 0.1f, groundLayer);
        if(hit.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coin++;
            UIManager.Instance.SetCoinText(coin);
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            OnDeath();
        }
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void ResetAttack()
    {
        isAttacking = false;
        ChangeAnim("idle");
    }

    public void ResetThrow()
    {
        isThrowing = false;
        ChangeAnim("idle");
    }

    public void SetSavePoint(Vector3 position)
    {
        savePoint = position;
    }

    public void SetMove(float horizontal)
    {
        moveVector.x = horizontal;
    }
}

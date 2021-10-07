using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region private variables
    [SerializeField]float speed;
    bool isFacingRight = true;
    int FacingDirection = 1;
    float HorizontalDirection;
    bool isGrounded;
    bool canJump;
    Rigidbody2D rb; 
    [SerializeField]float JumpVelocity;
    [SerializeField] float GroundCheckRadius;
    bool isRolling;
    [SerializeField]float RollTime;
    [SerializeField]float RollSpeed;
    [SerializeField]float RollCooldown;
    float RollTimeLeft;
    float LastRoll = -100f;
    bool canMove = true;
    bool canFlip = true;
    MainMenu ui;
    #endregion

    #region public variables
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public Animator anim;
    public bool isAttacking = false;
    public static PlayerController instance;
    public Transform AttackPos;
    public LayerMask WhatIsEnimies;
    public float AttackRange;
    public int Damage = 1;
    public int CurrentHealth;
    public int MaxHealth = 3;
    public ParticleSystem Dust;
    public HealthBar HealthBar;
    public AudioSource AttackAudio;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AttackAudio = GetComponent<AudioSource>();
        ui =GameObject.Find("GameOver").GetComponent<MainMenu>();
        instance = this;
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }
    private void Update()
    {
        MovementDirection();
        CheckIfcanJump();
        DodgeRoll();
        CheckRoll();
        HandleAttacks();
        UpdateAnimation();
        LoadScene();
    }

    private void CheckIfcanJump()
    {
        if(isGrounded && rb.velocity.y <= 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    void FixedUpdate()
    {
        Move();
        CheckSurroundings();
        Jump();
    }
    private void UpdateAnimation() //only for jump
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("y_velocity", rb.velocity.y);
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
    }
    private void Move()
    {
        HorizontalDirection = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Horizontal") && isRolling)
        {
            canMove = false;
            canFlip = false;
        }
        if(!canMove && !canFlip)
        {
            canMove = true;
            canFlip = true;
        }
        anim.SetFloat("P_run",Mathf.Abs(HorizontalDirection));
        if(canMove)
        {
            transform.position += new Vector3(HorizontalDirection, 0, 0) * Time.fixedDeltaTime * speed;
        }
    }
    private void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(canJump)
            {
                Dust.Play();
                rb.velocity = Vector2.up * JumpVelocity;
            }
        }
    }
    private void MovementDirection()
    {
        if(isFacingRight && HorizontalDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && HorizontalDirection > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if(canFlip)
        {
            if(isGrounded)
            {
                CreateDust();
            }
            FacingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
        
    }
    private void DodgeRoll()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            anim.SetBool("P_dodgeroll", true);
            Dust.Play();
            if(Time.time >= (LastRoll + RollCooldown))
            {
                AttemptToRoll();
            }
        }
    }
    private void CheckRoll()
    {
        if (isRolling)
        {
            if (RollTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(RollSpeed * FacingDirection, rb.velocity.y);
                RollTimeLeft -= Time.deltaTime;
            }
            if (RollTimeLeft <= 0)
            {
                anim.SetBool("P_dodgeroll", false);
                isRolling = false;
                canMove = true;
                canFlip = true;
            }
        }
    }
    private void AttemptToRoll()
    {
        isRolling = true;
        RollTimeLeft = RollTime;
        LastRoll = Time.time;
    }
    private void HandleAttacks()
    {
        if(Input.GetButtonDown("Attack") && !isAttacking)
        {
            isAttacking = true;
            AttackAudio.Play();
            Collider2D[] EnemiesToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, WhatIsEnimies);
            foreach (Collider2D enemy in EnemiesToDamage)
            {
                enemy.GetComponentInParent<Enemy_behaviour>().TakeDamage(Damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);

    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        HealthBar.SetHealth(CurrentHealth);
        anim.SetTrigger("P_hurt");
        if (CurrentHealth <= 0)
        {
            Die();
            ui.GameOver();
        }
    }
    private void Die()
    {
        anim.SetBool("P_dead",true);
        GetComponent<Collider2D>().enabled = false;
    }
    private void CreateDust()
    {
        Dust.Play();
    }
    public void LoadScene()
    {
        if(Input.GetKeyDown(KeyCode.R) && anim.GetCurrentAnimatorStateInfo(0).IsName("P_death"))
        {
            SceneManager.LoadScene(2);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}

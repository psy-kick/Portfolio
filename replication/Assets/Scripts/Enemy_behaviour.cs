using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_behaviour : MonoBehaviour
{

    #region Public Variables
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public int CurrentHealth;
    public int MaxHealth = 3;
    public Transform AttackPos;
    public LayerMask WhatIsPlayer;
    public float AttackRange;
    public int Damage = 1;
    public Transform LeftLimit;
    public Transform RightLimit;
    [HideInInspector]public bool inRange;
    [HideInInspector]public Transform target;
    public GameObject HotZone;
    public GameObject TriggerArea;
    #endregion

    #region Private Variables
    Animator anim;
    float distance;
    bool attackMode;
    bool cooling;
    float intTimer;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }
        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("E_attack"))
        {
            SelectTarget();
        }
        if (inRange)
        {
            EnemyLogic();
        }
    }
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
                Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("E_attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("E_walk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("E_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("E_walk", false);
        anim.SetBool("E_attack", true);
        Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, WhatIsPlayer);
        foreach (Collider2D enemy in PlayerToDamage)
        {
            enemy.GetComponentInParent<PlayerController>().TakeDamage(Damage);
        }
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("E_attack", false);
    }
    public void TriggerCooling()
    {
        cooling = true;
    }
    public void TakeDamage(int damage)
    {
        anim.SetTrigger("E_hurt");
        StartCoroutine(AttackWait());
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("E_death", true);
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
    private bool InsideOfLimits()
    {
        return transform.position.x > LeftLimit.position.x && transform.position.x < RightLimit.position.x;
    }
    public void SelectTarget()
    {
        float DistanceToLeft = Vector2.Distance(transform.position, LeftLimit.position);
        float DistanceToRight = Vector2.Distance(transform.position, RightLimit.position);
        if (DistanceToLeft > DistanceToRight)
        {
            target = LeftLimit;
        }
        else
        {
            target = RightLimit;
        }
        Flip();
    }

    public void Flip()
    {
        Vector3 Rotation = transform.eulerAngles;
        if(transform.position.x < target.position.x)
        {
            Rotation.y = 180f;
        }
        else
        {
            Rotation.y = 0;
        }
        transform.eulerAngles = Rotation;
    }
    private IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(1f);
    }
}

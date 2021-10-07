using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneScript : MonoBehaviour
{
    private Enemy_behaviour EnemyParent;
    private Animator anim;
    private bool inRange;
    private void Awake()
    {
        EnemyParent = GetComponentInParent<Enemy_behaviour>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("E_attack"))
        {
            EnemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            EnemyParent.TriggerArea.SetActive(true);
            EnemyParent.inRange = false;
            EnemyParent.SelectTarget();
        }
    }
}

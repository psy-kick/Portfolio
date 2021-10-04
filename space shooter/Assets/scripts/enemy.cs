using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private float _speed = 4f;
    private player _p;
    private Animator _anim;
    private AudioSource _audiosource;
    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _p = FindObjectOfType<player>();
        if(_p==null)
        {
            Debug.LogError("dead");
        }
        _anim = GetComponent<Animator>();
        if(_anim==null)
        {
            Debug.LogError("anim is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5.5f)
        {
            float randomx = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomx, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player player = other.transform.GetComponent<player>();
            if (player != null)
            {
                player.damage();
            }
            _anim.SetTrigger("on_enemy_death");
            _speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject,2.8f);
        }
        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            if(_p!=null)
            {
                _p.addscore(10);
            }
            _anim.SetTrigger("on_enemy_death");
            _speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject,0.5f);
        }
    }
}

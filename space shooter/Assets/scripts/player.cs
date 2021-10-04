using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player : MonoBehaviour
{
    [SerializeField]
    private float _speed =10;
    private float _speedmul = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _firerate = 0.2f;
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private spawn_manager _spawn;
    [SerializeField]
    private bool Triple_shot=false;
    [SerializeField]
    private GameObject _triple_shotPrefab;
    [SerializeField]
    private bool _speedpoweup = false;
    [SerializeField]
    private GameObject _speed_Prefab;
    [SerializeField]
    private bool _shieldpowerup = false;
    [SerializeField]
    private GameObject _shield_Prefab;
    [SerializeField]
    private GameObject _shieldvis;
    [SerializeField]
    private int _score;
    [SerializeField]
    private ui_manager _ui_manager;
    [SerializeField]
    private GameObject _left_engine, _right_engine;
    [SerializeField]
    private GameObject _explosionprefab;
    [SerializeField]
    private AudioClip _laseraudio;
    private AudioSource _audiosource;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
        _spawn = GameObject.Find("spawn_manager").GetComponent<spawn_manager>();
        _ui_manager = GameObject.Find("Canvas").GetComponent<ui_manager>();
        _audiosource = GetComponent<AudioSource>();

        if(_spawn==null)
        {
            Debug.LogError("ahahahahahahah");
        }
        if(_ui_manager==null)
        {
            Debug.LogError("donforlife");
        }
        if(_audiosource==null)
        {
            Debug.LogError("audio missing");
        }
        else
        {
            _audiosource.clip = _laseraudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        if (Input.GetKeyUp(KeyCode.Space) && Time.time > _canfire)
            shoot();
      
    }
    void movement()
    {
        float movement = Input.GetAxis("Horizontal");
        float mve = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(movement, mve, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0);
        if(transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y,0);
        }
        else if(transform.position.x <-9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
    }
    void shoot()
    {
        _canfire = Time.time + _firerate;
        if(Triple_shot==true)
        {
            Instantiate(_triple_shotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audiosource.Play();
    }
    public void damage()
    {
        if(_shieldpowerup==true)
        {
            _shieldpowerup = false;
            _shieldvis.SetActive(false);
            return;
        }
        _lives--;
        if(_lives==2)
        {
            _left_engine.SetActive(true);
        }
        else if(_lives==1)
        {
            _right_engine.SetActive(true);
        }
        _ui_manager.updatelives(_lives);
        if(_lives<1)
        {
            _spawn.onplayerdeath();
            Instantiate(_explosionprefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _ui_manager.updategameover();
        }
    }
    public void TripleShotActive()
    {
        Triple_shot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void SpeedActive()
    {
        _speedpoweup = true;
        _speed *= _speedmul;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    public void ShieldActive()
    {
        _shieldpowerup = true;
        _shieldvis.SetActive(true);
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speedpoweup = false;
        _speed /= _speedmul;
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        Triple_shot = false;
    }
    public void addscore(int point)
    {
        _score +=point;
        _ui_manager.updatescore(_score);
    }
}

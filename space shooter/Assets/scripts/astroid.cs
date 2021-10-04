using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroid : MonoBehaviour
{
    [SerializeField]
    private float _rotatespeed = 3f;
    [SerializeField]
    private GameObject _explosionprefab;
    private spawn_manager _spawn_manger;
    // Start is called before the first frame update
    void Start()
    {
        _spawn_manger = FindObjectOfType<spawn_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotatespeed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="laser")
        {
            Instantiate(_explosionprefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawn_manger.spawn();
            Destroy(this.gameObject,0.1f);
        }

    }
}

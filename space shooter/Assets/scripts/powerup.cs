using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerup_id;
    [SerializeField]
    private AudioClip _clip;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8f, 8), 5, 0);
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y>=6f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player player = other.transform.GetComponent<player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if(player !=null)
            {
                switch(_powerup_id)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default :
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

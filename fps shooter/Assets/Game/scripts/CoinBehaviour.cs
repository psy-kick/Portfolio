using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public AudioClip CoinSound;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player p = other.GetComponent<player>();
                if (p != null)
                {
                    p.HasCoin = true;
                    AudioSource.PlayClipAtPoint(CoinSound, transform.position, 1f);
                    uimanager ui = GameObject.Find("Canvas").GetComponent<uimanager>();
                    if(ui != null)
                    {
                        ui.Inventory();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

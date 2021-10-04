using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player p = other.GetComponent<player>();
                if (p != null)
                {
                    if (p.HasCoin == true)
                    {
                        p.HasCoin = false;
                        uimanager ui = GameObject.Find("Canvas").GetComponent<uimanager>();
                        if (ui != null)
                        {
                            ui.RemoveItem();
                        }
                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                        p.EnableWeapon();
                    }
                    else
                    {
                        Debug.Log("Get outta here");
                    }
                }
            }
        }
    }
}


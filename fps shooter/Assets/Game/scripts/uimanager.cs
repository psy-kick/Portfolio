using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uimanager : MonoBehaviour
{
    public Text Ammotext;
    public GameObject Coin;
    player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<player>();
        if(Input.GetKeyDown(KeyCode.Escape)==false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateAmmoCount(int Count)
    {
        Ammotext.text = "Ammo: " + Count;
    }
    public void Inventory()
    {
        Coin.SetActive(true);
    }
    public void RemoveItem()
    {
        Coin.SetActive(false);
    }
}

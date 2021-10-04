using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public GameObject CreateCracked;
    public void DestroyCrate()
    {
        Instantiate(CreateCracked, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}

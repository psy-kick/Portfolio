using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looky : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mousey = Input.GetAxis("Mouse Y");
        Vector3 newrotation = transform.localEulerAngles;
        newrotation.x -= _mousey*_sensitivity;
        transform.localEulerAngles = newrotation;
    }
}

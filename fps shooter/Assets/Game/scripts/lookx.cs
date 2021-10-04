using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookx : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mousex = Input.GetAxis("Mouse X");
        Vector3 newrotation = transform.localEulerAngles;
        newrotation.y += _mousex * _sensitivity;
        transform.localEulerAngles = newrotation;
    }
}

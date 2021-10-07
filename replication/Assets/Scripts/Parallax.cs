using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float Startpos,length;
    public GameObject Cam;
    public float ParallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        Startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (Cam.transform.position.x * (1 - ParallaxEffect));
        float dist = (Cam.transform.position.x * ParallaxEffect);
        transform.position = new Vector3(Startpos + dist, transform.position.y, transform.position.z);
        if (temp > Startpos + length) Startpos += length;
        else if(temp < Startpos -length) Startpos -= length;
    }
}

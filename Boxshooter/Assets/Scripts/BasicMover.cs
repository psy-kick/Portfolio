using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    public float spinspeed = 180f;
    public float motionspeed = 0.1f;
    public bool dospin = true;
    public bool motion = true;
    // Update is called once per frame
    void Update()
    {
        if(dospin)
        {
            this.gameObject.transform.Rotate(Vector3.up * spinspeed * Time.deltaTime);
        }
        if(motion)
        {
            this.gameObject.transform.Translate(Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad) * motionspeed);
        }
    }
}

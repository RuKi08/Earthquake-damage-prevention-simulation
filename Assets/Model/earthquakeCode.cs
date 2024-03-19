using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquakeCode : MonoBehaviour
{
    public bool play;

    public float shakeSpeed = 2.0f;
    public float shakeAmount = 1.0f;
 
    Rigidbody rb;

    Vector3 originPosition;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originPosition = transform.localPosition;
    }

    void Update()
    {
        if (play)
        {
            float elapsedTime = 0.0f;

            float x = Mathf.Cos(Time.time * 360 * Mathf.Deg2Rad * shakeSpeed)*shakeAmount;

            rb.velocity = new Vector3(x, 0, 0);
            }
    }
}

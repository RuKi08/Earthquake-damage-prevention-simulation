using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquakeCode : MonoBehaviour
{
    float shakeSpeed;
    float shakeAmount;
 
    Rigidbody rb;

    Vector3 originPosition;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        originPosition = transform.localPosition;

        shakeSpeed  = gameManager.instance.shakeSpeed;
        shakeAmount = gameManager.instance.shakeAmount;
    }

    void Update()
    {
        if (gameManager.instance.play)
        {
            float y = Mathf.Cos(Time.time * 360 * Mathf.Deg2Rad * shakeSpeed)*shakeAmount;
            rb.velocity = new Vector3(y, 0, y);
        }
    }
}

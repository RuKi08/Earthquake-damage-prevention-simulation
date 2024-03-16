using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquakeCode : MonoBehaviour
{
    public bool play;

    public float shakeTime = 1.0f;
    public float shakeSpeed = 2.0f;
    public float shakeAmount = 1.0f;
 
    private Transform cam;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        cam = transform;
        rb = gameObject.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            StartCoroutine(Shake());
        }
    }
 
    IEnumerator Shake()
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        //Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
        Vector3 randomPoint = Random.insideUnitSphere * shakeAmount;
 
        while (elapsedTime < shakeTime)
        {
            rb.velocity = randomPoint;
            //cam.localPosition = Vector3.Lerp(cam.localPosition, new Vector3(randomPoint.x, randomPoint.y/10, randomPoint.z), Time.deltaTime * shakeSpeed);
 
            yield return null;
 
            elapsedTime += Time.deltaTime;
        }

        //cam.localPosition = Vector3.Lerp(cam.localPosition, originPosition, Time.deltaTime * shakeSpeed/100);
        //cam.localPosition = originPosition;
    }
}

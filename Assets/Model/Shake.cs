using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool Play;
    public float amount;

    Vector3 originPos;
    
    void Start() {
        originPos = transform.localPosition;
    }

    void Update() {
        if(Play) transform.localPosition = (Vector3)Random.insideUnitCircle * amount + originPos;
    }
}


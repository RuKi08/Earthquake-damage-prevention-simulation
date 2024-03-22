using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float playerSpeed;

    int r = 0;
    
    void Update()
    {
        int Move = 0;

        if(Input.GetKey(KeyCode.A) 
        || Input.GetKey(KeyCode.D) 
        || Input.GetKey(KeyCode.W) 
        || Input.GetKey(KeyCode.S)) Move = 1;

        if      (Input.GetKey(KeyCode.A)) r = 3;
        else if (Input.GetKey(KeyCode.D)) r = 1;
        else if (Input.GetKey(KeyCode.W)) r = 0;
        else if (Input.GetKey(KeyCode.S)) r = 2;

        transform.Translate(Move * Vector3.right * playerSpeed * Time.deltaTime);
        
        transform.eulerAngles = Vector3.up * r * 90;
    }
}

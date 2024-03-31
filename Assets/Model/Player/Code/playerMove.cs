using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public int DNASize;
    public float ActionInterval;
    public float playerSpeed;

    public int[] DNA;

    float StartTime;
    int r = 0;

    void Start()
    {
        DNA = new int[DNASize];
        for(int i = 0; i < DNA.Length; i++) DNA[i] = Random.Range(0,6);

        StartTime = Time.time;
    }
    
    void Update()
    {
        int time = (int)((Time.time - StartTime)/ActionInterval);
        int Move = 1;

        if(time < DNA.Length)
        {
            switch (DNA[time])
            {
                case 1:  r = 1;     break;
                case 2:  r = 2;     break;
                case 3:  r = 3;     break;
                case 4:  r = 4;     break;
                default: Move = 0;  break;
            }

            transform.Translate(Move * Vector3.right * playerSpeed * Time.deltaTime);
            
            transform.eulerAngles = Vector3.up * r * 90;
        }
    }
}

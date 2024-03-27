using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore : MonoBehaviour
{
    public int score;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tile") 
            score = int.Parse(other.transform.parent.name);
    }   
}

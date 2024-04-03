using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "box" || other.tag == "book") 
        {
            float damage = other.GetComponent<Rigidbody>().velocity.magnitude;
            
            if(damage > 1) GameOver();
        }
        else if(other.transform.parent.gameObject.tag == "wall") GameOver();
    }   

    void GameOver()
    {
        gameManager.instance.LiveObjectNumber -= 1;
        gameObject.SetActive(false);
    }
}   
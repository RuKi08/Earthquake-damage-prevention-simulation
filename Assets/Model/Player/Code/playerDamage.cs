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
            if(damage > 1) gameObject.SetActive(false);
        }
        else if(other.transform.parent.gameObject.tag == "wall") gameObject.SetActive(false);
    }   
}   
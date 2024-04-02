using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    
    private void Awake() 
    {
        instance = this;
    } 
    public int Count; 
    public GameObject BaseObject; 
    public GameObject ParentObject; 
    public List<GameObject> ObjectList = new List<GameObject>(); 

    void Start()
    {
        SetObject();
    }

    void SetObject()
    {
        for (int i = 0; i < Count; i++) 
        {
            ObjectList.Add(Instantiate(BaseObject, ParentObject.transform));
            ObjectList[i].SetActive(true);
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public int LiveObjectNumber;

    void Start()
    {
        LiveObjectNumber = Count;

        SetObject();
    }

    void Update()
    {
        if(LiveObjectNumber <= Count/2) {Reset(); EditorApplication.isPaused = true;}
    }

    void Reset()
    {
        float AllScore = 0;

        List<int> Survivor  = new List<int>();
        int[] PlayerScores  = new int[Count];

        for (int N = 0; N < Count; N++)
        {
            if(ObjectList[N].gameObject.activeSelf) 
            {
                PlayerScores[N] = ObjectList[N].GetComponent<playerScore>().score;
                AllScore += PlayerScores[N];

                Survivor.Add(N);
            }
        }

        float Survivor_Count = Survivor.Count;

        for (int i = 0; i < Survivor.Count; i++)
        {
            if(PlayerScores[Survivor[i]] < AllScore / Survivor_Count) 
            {
                Survivor.RemoveAt(i);
                i -= 1;
            }
        }

        for (int i = 0; i < Count; i++)         ObjectList[i].SetActive(false);
        for (int i = 0; i < Survivor.Count; i++)ObjectList[Survivor[i]].SetActive(true);
    }

    void SetObject()
    {
        for (int i = 0; i < Count; i++) 
        {
            ObjectList.Add(Instantiate(BaseObject, ParentObject.transform));
            ObjectList[i].name = i.ToString();
            ObjectList[i].SetActive(true);
        } 
    }
}

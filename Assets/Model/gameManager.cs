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

        int Survivor_Count_Num = 0;

        for (int i = 0; i < Count; i++)
        {
            if(i == Survivor[Survivor_Count_Num]) 
            {
                if(Survivor_Count_Num < Survivor.Count-1) Survivor_Count_Num++;
            }
            else
            {
                int ParentDNA_num1 = Random.Range(0, Survivor.Count);
                int ParentDNA_num2 = Random.Range(0, Survivor.Count);

                int[] ParentDNA_1 = ObjectList[Survivor[ParentDNA_num1]].GetComponent<playerMove>().DNA;
                int[] ParentDNA_2 = ObjectList[Survivor[ParentDNA_num2]].GetComponent<playerMove>().DNA;

                ObjectList[i].GetComponent<playerMove>().DNA = Mutation(Heredity((ParentDNA_1, ParentDNA_2)));
            }
        }
    }
    
    public int GeneSize;

    int[] Heredity((int[],int[]) DNA)
    {
        int[] childDNA = new int[DNA.Item1.Length];

        for(int i = 0; i < DNA.Item1.Length / GeneSize; i++)
        {
            bool randomBool = (Random.value > 0.5f);

            if(randomBool)  for (int n = 0; n < GeneSize; n++) childDNA[i*GeneSize + n] = DNA.Item1[i*GeneSize + n];
            else            for (int n = 0; n < GeneSize; n++) childDNA[i*GeneSize + n] = DNA.Item2[i*GeneSize + n];
        }

        return childDNA;
    }

    public int MutationSize;

    int[] Mutation(int[] TargetDNA)
    {
        int M_size = Random.Range(0, MutationSize);

        for (int i = 0; i < M_size; i++)
        {
            TargetDNA[Random.Range(0, TargetDNA.Length)] = Random.Range(0, 6);
        }
        
        return TargetDNA;
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

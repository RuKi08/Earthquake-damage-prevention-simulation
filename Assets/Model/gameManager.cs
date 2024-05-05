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
    public float speed;
    public float time;


    [Header ("DNA Setting")]
    public int DNASize;
    public int GeneSize;
    public int MutationSize;

    [Header ("Player Number")]
    public int PlayerNumber; 

    [Header ("Player Setting")]
    public float ActionInterval;
    public float playerSpeed;

    [Header ("Earthquake Setting")]
    public bool play;
    public float shakeSpeed = 2.0f;
    public float shakeAmount = 1.0f;
    
    [Header ("etc.")]
    public GameObject BaseObject; 
    public GameObject ParentObject; 
    public List<GameObject> ObjectList = new List<GameObject>(); 

    public int LiveObjectNumber;
    float StartTime;

    void Start()
    {
        LiveObjectNumber = PlayerNumber;
        StartTime = Time.time;

        SetObject();
    }

    void Update()
    {
        Time.timeScale = speed;
        if(time < Time.time - StartTime) Reset();
    }

    void Reset()
    {
        StartTime = Time.time;

        float AllScore = 0;

        List<int> Survivor  = new List<int>();
        int[] PlayerScores  = new int[PlayerNumber];

        for (int N = 0; N < PlayerNumber; N++)
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

        int Survivor_Count_Num = 0;

        for (int i = 0; i < PlayerNumber; i++)
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

        LiveObjectNumber = PlayerNumber;
        for (int i = 0; i < PlayerNumber; i++) Re_Start(ObjectList[i]);

    }

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
        for (int i = 0; i < PlayerNumber; i++) 
        {
            ObjectList.Add(Instantiate(BaseObject, ParentObject.transform));

            playerMove ObjectCode   = ObjectList[i].GetComponent<playerMove>();

            ObjectCode.DNA = new int[DNASize];
            for(int L = 0; L < DNASize; L++) ObjectCode.DNA[L] = Random.Range(0,6);

            Re_Start(ObjectList[i]);
        } 
    }

    void Re_Start(GameObject player)
    {
        playerMove playerCode   = player.GetComponent<playerMove>();
        playerCode.StartTime    = Time.time;

        player.transform.position       = ParentObject.transform.position;
        player.transform.eulerAngles    = Vector3.zero;

        player.SetActive(true);
    }
}

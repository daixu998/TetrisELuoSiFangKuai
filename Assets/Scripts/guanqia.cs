using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode][CreateAssetMenu(menuName ="guanq/创建关卡")]
public class guanqia : ScriptableObject
{
     [SerializeField]
    public int[,] staticeGrid = new int[Grid.w,Grid.h];


    [SerializeField]
    public Dictionary<int[,] ,int > staticeGrids = new Dictionary<int[,], int>();


    // Start is called before the first frame update
    void Start()
    {
        staticeGrids = new Dictionary<int[,], int>();
        staticeGrids.Add(staticeGrid,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

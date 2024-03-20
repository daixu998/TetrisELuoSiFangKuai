using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform gridPrat;
    public GameObject[] prefabs;
    public GameObject[] specialPrefabs;
    public bool isGameOver = true;
    public static bool isSpecial = false;

    void Start()
    {
        Instances();
        Debug.Log(Grid.grid.Length);

        //  isGameOver = true;

    }

    // Update is called once per frame
    void Update()
    {


    }
    /// <summary>
    /// 生成新格子的方法
    /// </summary> <summary>
    /// 
    /// </summary>
    public void Instances()
    {
        if (!isGameOver)
        {
            return;
        }
        if (isSpecial)
        {
            int i = Random.Range(0, specialPrefabs.Length);
            GameObject prefab = specialPrefabs[i];
            GameObject instance = Instantiate(prefab,gridPrat);
            instance.transform.position = transform.position;
            instance.transform.SetParent(gridPrat);
            isSpecial = false;
        }
        else
        {
            int i = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[i];
            GameObject instance = Instantiate(prefab,gridPrat);
            
            instance.transform.position = transform.position;
            instance.transform.SetParent(gridPrat);
        }

    }


     public void disGrid(){
        for (int i = 0; i < gridPrat.childCount; i++)
        {
         Destroy (gridPrat.GetChild(i).gameObject);
        }
     }




}

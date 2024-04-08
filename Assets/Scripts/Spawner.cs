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
    public bool isCreatGrid = true;
    // public List<int> removeRows;
    public static Spawner instance;

    public bool isRemove = false;
    public bool isPlayAni = false;
    public int removeCount = 0;
    public int removeRow = 0;
    public Spawner()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {

        Instances();
        Debug.Log(Grid.grid.Length);

        //  isGameOver = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isCreatGrid&&!isPlayAni)
        {
            Instances();
        }


        if (isRemove)
        {
            deleteFullRow();
        }

    }


    public  void deleteFullRow()
    {
        removeCount = 0;

        // int row = 0;
        for (int i = 0; i < Grid.h;)
        {
            if (Grid.isTS(i))
            {
                Grid.deletRow(i);
                
                // MonoBehaviour.Invoke("delayOpen", 1);
                // isPlayAni = true;
                // removeRows.Add(i);

                // removeRow = i;
                //  Grid.decreaseRowAbove(i+1);
                StartCoroutine(PlayAniOver(i+1-removeCount,0.4f));
                removeCount++;
                // Grid.decreaseRowAbove(i + 1);
            }
            else
            if (Grid.isFullRow(i))
            {
                Grid.deletRow(i);
                
                // Grid.decreaseRowAbove(i+1);
                // isPlayAni = true;
                // removeRows.Add(i);
                // removeRow=i;
                StartCoroutine(PlayAniOver(i+1-removeCount,0.4f));
                removeCount++;
                // Grid.decreaseRowAbove(i + 1);
            }
            else
            {
                i++;
            }
        }
        FindObjectOfType<GUIManager>().addGrade(removeCount);
        // if (removeCount>0)
        // {
        //     StartCoroutine(PlayAniOver());
        // }
        if (removeCount >= 2)
        {
            isSpecial = true;
        }
        removeCount = 0;
        isRemove = false;
         
    }
    IEnumerator PlayAniOver(int y ,float time)
    {
        // Grid.deletRow(y);
        yield return new WaitForSeconds(time);
        Grid.decreaseRowAbove(y);
        yield return new WaitForSeconds(0.2f);
        isPlayAni = false;
       
    }

    // IEnumerator PlayAniOver1(int y)
    // {
    //      yield return new WaitForSeconds(y);
    //      isPlayAni = false;
    // }
    // IEnumerator PlayAniOver( )
    // {
    //     yield return new WaitForSeconds(1f);
    //     for (int i = 0; i < removeRows.Count; i++)
    //         {
    //             yield return new WaitForSeconds(0.1f);
    //             Grid.decreaseRowAbove(removeRows[i]+1);
    //         }
    //     removeRows = new List<int>();
    //     isPlayAni = false;
       
    // }
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
            GameObject instance = Instantiate(prefab, gridPrat);
            instance.transform.position = transform.position;
            instance.transform.SetParent(gridPrat);
            isSpecial = false;
        }
        else
        {
            int i = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[i];
            GameObject instance = Instantiate(prefab, gridPrat);

            instance.transform.position = transform.position;
            instance.transform.SetParent(gridPrat);
        }
        isCreatGrid = false;
    }


    public void disGrid()
    {
        for (int i = 0; i < gridPrat.childCount; i++)
        {
            Destroy(gridPrat.GetChild(i).gameObject);
        }
    }




}

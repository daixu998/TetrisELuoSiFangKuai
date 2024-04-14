using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform gridPrat;
    public GameObject[] prefabs;
    public GameObject[] specialPrefabs;

    public GameObject guangquanPrefab;
    // [System.Serializable]
    public StaticGrid[,] staticGrids = new StaticGrid[Grid.w, Grid.h];

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

        instancesGuangQuan();

    }

    // Update is called once per frame
    void Update()
    {
        if (isCreatGrid && !isPlayAni)
        {
            Instances();
        }


        if (isRemove)
        {
            deleteFullRow();
        }

    }
    /// <summary>
    /// 随机生成
    /// </summary> <summary>
    /// 
    /// </summary>
    public void instancesGuangQuan()
    {


        for (int x = 0; x < Grid.w; x++)
        {
            for (int y = 0; y < Grid.h - 10; y++)
            {
                var quan = GameObject.Instantiate(guangquanPrefab, new Vector3(x, y, 0), Quaternion.identity);

                quan.transform.GetComponentInChildren<StaticGrid>().HP = 0;
                

                staticGrids[x, y] = quan.transform.GetComponentInChildren<StaticGrid>();
                quan.transform.SetParent(transform);
            }
        }
    }

        /// <summary>
    /// 随机生成
    /// </summary> <summary>
    public void RandominstancesGuangQuan()
    {


        for (int x = 0; x < Grid.w; x++)
        {
            for (int y = 0; y < Grid.h - 10; y++)
            {
                var quan = GameObject.Instantiate(guangquanPrefab, new Vector3(x, y, 0), Quaternion.identity);
                if (Random.Range(0, 90) > 70)
                {
                    quan.transform.GetComponentInChildren<StaticGrid>().HP = Random.Range(0, 10);
                }
                else
                {
                    quan.transform.GetComponentInChildren<StaticGrid>().HP = 0;
                }

                staticGrids[x, y] = quan.transform.GetComponentInChildren<StaticGrid>();
                quan.transform.SetParent(transform);
            }
        }
    }
    public void SeveGuangQuan()
    {
        for (int x = 0; x < Grid.w; x++)
        {
            for (int y = 0; y < Grid.h; y++)
            {
                if (staticGrids[x, y].HP > 0)
                {
                    // staticGrids[x, y].Save();
                }
            }
        }
    }

    public void LoadGuangQuan()
    {
    }

    public void removeGuangQuan(int y)
    {
        for (int x = 0; x < Grid.w; x++)
        {
            staticGrids[x, y].HP--;
        }
    }
    public void deleteFullRow()
    {
        removeCount = 0;

        // int row = 0;
        for (int i = 0; i < Grid.h;)
        {
            if (Grid.isTS(i))
            {
                Grid.deletRow(i);
                removeGuangQuan(i);
                StartCoroutine(PlayAniOver(i + 1 - removeCount, 0.4f));
                removeCount++;

            }
            else
            if (Grid.isFullRow(i))
            {
                Grid.deletRow(i);
                removeGuangQuan(i);
                StartCoroutine(PlayAniOver(i + 1 - removeCount, 0.4f));
                removeCount++;

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
    IEnumerator PlayAniOver(int y, float time)
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

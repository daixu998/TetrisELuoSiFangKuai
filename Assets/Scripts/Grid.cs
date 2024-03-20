using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int w = 10;
    public static int h = 21;

    public static Transform[,] grid = new Transform[w, h];

    public static Vector2 roundVec2(Vector2 vector2)
    {
        return new Vector2(Mathf.Round(vector2.x),
            Mathf.Round(vector2.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
        (int)pos.x < w &&
        (int)pos.y >= 0);
    }

    public static bool isFullRow(int y)
    {

        //检查每一列是否都为空
        for (int x = 0; x < Grid.w; ++x)
        {
            if (Grid.grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static bool isTS(int y)
    {
        for (int x = 0; x < Grid.w; ++x)
        {
            if (Grid.grid[x, y] != null && Grid.grid[x, y].name == "TX")
            {
                return true;
            }

        }

        return false;
    }
    public static void deletRow(int y)
    {
        // 遍历每一列
        for (int i = 0; i < Grid.w; ++i)
        {
            // 销毁当前列的格子
            if (Grid.grid[i, y] != null)
            {

                Grid.grid[i, y].GetComponent<Animator>().SetBool("SizeWave", true);
                // 将当前列的格子置为null
                Destroy(Grid.grid[i, y].gameObject, 1.0f);



                // 将当前列的格子置为null
                // yield return new WaitForSeconds(1.0f);
                Grid.grid[i, y] = null;
                // StartCoroutine(Grid.StartAni(1f,i,y));
            }



        }
    }
    static IEnumerator StartAni(float time, int x, int y)
    {
        yield return new WaitForSeconds(time);
        Grid.grid[x, y] = null;
    }

    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //向下移动整行
    public static void decreaseRowAbove(int y)
    {

        for (int x = y; x < h; x++)
        {

            decreaseRow(x);

        }
    }
    void delayOpen()
    {
        // decreaseRowAbove(i + 1);
    }
    public static async void deleteFullRow()
    {


        int row = 0;
        for (int i = 0; i < h;)
        {
            if (isTS(i))
            {
                deletRow(i);
                row++;
                // MonoBehaviour.Invoke("delayOpen", 1);
                 decreaseRowAbove(i + 1);
            }
            else
            if (isFullRow(i))
            {
                deletRow(i);
                row++;
                decreaseRowAbove(i + 1);
            }
            else
            {
                i++;
            }
        }
        FindObjectOfType<GUIManager>().addGrade(row);
        if (row >= 2)
        {
            Spawner.isSpecial = true;
        }
    }


    //重置游戏
    public static void resetGrid()
    {
        // for (int x = 0; x < w; ++x)
        // {
        //     for (int y = 0; y < h; ++y)
        //     {
        //         if (grid[x, y]!=null)
        //         {
        //             Destroy(Grid.grid[x, y].gameObject);
        //         }

        //         grid[x,y] =null;

        //     }
        // }
        FindObjectOfType<Spawner>().disGrid();
        grid = new Transform[w, h];
        FindObjectOfType<Spawner>().isGameOver = true;
        FindObjectOfType<Spawner>().Instances();
    }
}

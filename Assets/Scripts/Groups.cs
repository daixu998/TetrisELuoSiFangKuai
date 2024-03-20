using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groups : MonoBehaviour
{
    float lastFall;
    public float Speed = 1f;
    void Start()
    {
        if (isValidGrifPos())
        {
            // Debug.Log("Game staet");
        }
        else
        {
            Debug.Log("Game Over");
            Destroy(gameObject);

            FindObjectOfType<Spawner>().isGameOver = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //向左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidGrifPos())
            {
                updateGrid();

            }
            else
            { transform.position += new Vector3(1, 0, 0); }
        }

        //向右
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGrifPos())
            {
                updateGrid();

            }
            else
            { transform.position -= new Vector3(1, 0, 0); }
        }
        //旋转
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            if (isValidGrifPos())
            {
                updateGrid();

            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }
        //向下

        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - lastFall >= Speed
        )
        {
            transform.position -= new Vector3(0, 1, 0);
            if (isValidGrifPos())
            {
                updateGrid();

            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //已经到位开始删除行
                Grid.deleteFullRow();


                FindObjectOfType<Spawner>().Instances();
                enabled = false;
                // StartCoroutine(stupScript());
            }


            lastFall = Time.time;
            //     if (Input.GetKeyUp(KeyCode.DownArrow))
            // {
            //     Speed = 1f;
            // }

        }



    }

    public bool isValidGrifPos()
    {
        // 遍历transform中的每一个item
        foreach (Transform item in transform)
        {
            // 获取item的位置
            Vector2 v = Grid.roundVec2(item.position);

            // 判断V是否在边界内
            if (!Grid.insideBorder(v))
            {
                // 如果不在，返回false

                return false;
            }
            if (v != null && transform != null)
            {
                int x = (int)v.x;
                int y = (int)v.y;
                if (x >= 0 && x < Grid.grid.GetLength(0) && y >= 0 && y < Grid.grid.GetLength(1))
                {
                    // 在合法的范围内，继续检查parent属性
                    if (Grid.grid[x, y] != null)
                    {
                        if (Grid.grid[x, y].parent != transform)
                        {

                            if (y > 18)
                            {
                                //游戏结束
                                Debug.Log("游戏结束");
                                Destroy(gameObject);
                                FindObjectOfType<Spawner>().isGameOver = false;
                                FindObjectOfType<GUIManager>().gameOverr();
                            }
                            return false;

                        }

                    }
                }

            }


        }
        // 如果全部在，返回true
        return true;
    }

    // 更新网格
    void updateGrid()
    {
        //qinli清理上一次的数据
        for (int y = 0; y < Grid.h; y++)
        {
            for (int x = 0; x < Grid.w; x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    if (Grid.grid[x, y].parent == transform)
                    {
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }


        // 遍历transform中的每一个item
        foreach (Transform item in transform)
        {
            // 将item的位置四舍五入到网格单位
            Vector2 v = Grid.roundVec2(item.position);
            // 将item放入网格中
            int x = (int)v.x;
            int y = (int)v.y;

            if (x >= 0 && x < Grid.grid.GetLength(0) && y >= 0 && y < Grid.grid.GetLength(1))
            {
                Grid.grid[(int)v.x, (int)v.y] = item;
            }
        }
    }
    // IEnumerator stupScript()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     enabled = false;
    // }

}

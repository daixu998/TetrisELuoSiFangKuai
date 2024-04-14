using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Unity.VisualScripting;

[System.Serializable]//注意命名空间
public class Save //这是Save类
{
    public int[] ints0;
    public int[] ints1;
    public int[] ints2;
    public int[] ints3;
    public int[] ints4;
    public int[] ints5;
    public int[] ints6;
    public int[] ints7;
    public int[] ints8;
    public int[] ints9;
    public int[] ints10;
    public int[][] ints;

}


public class EditorScene : MonoBehaviour
{

    List<int[]> listOf2DArrays = new List<int[]>
    {
    new int[Grid.w*Grid.h] ,
     new int[Grid.w*Grid.h],
      new int[Grid.w*Grid.h],
       new int[Grid.w*Grid.h],
        new int[Grid.w*Grid.h],
         new int[Grid.w*Grid.h],
          new int[Grid.w*Grid.h],
     new int[Grid.w*Grid.h],
      new int[Grid.w*Grid.h],
       new int[Grid.w*Grid.h],
        new int[Grid.w*Grid.h],
         new int[Grid.w*Grid.h]
    };
    public EditorGrid[,] editorGrids = new EditorGrid[Grid.w, Grid.h];

    public GameObject guangquanPrefab;

    Save JsonSaveObject = new Save();//声明Save对象，记录当前的游戏状态

    public Button SaveBtn;


    public TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        // 序列化列表为JSON字符串
        // string json = JsonConvert.SerializeObject(listOf2DArrays, Formatting.Indented);

        ///加载数据
        for (int x = 0; x < Grid.w; x++)
        {
            for (int y = 0; y < Grid.h - 10; y++)
            {
                var quan = GameObject.Instantiate(guangquanPrefab, new Vector3(x, y, 0), Quaternion.identity);

                editorGrids[x, y] = quan.transform.GetComponentInChildren<EditorGrid>();
                quan.transform.SetParent(transform);
            }
        }

        string path = Application.streamingAssetsPath + "ByJson.json";//这是想打开的文件路径
        StreamReader reader = new StreamReader(path);//创建读取的文件流

        string ObjectStr = reader.ReadToEnd();//将文本文件中的内容全部读取到字符串中，（json格式其实就算字符串类型）
        reader.Close();//关闭流

        //反序列化
        List<int[]> deserializedListOf2DArrays = JsonConvert.DeserializeObject<List<int[]>>(ObjectStr);

        SaveBtn.onClick.AddListener(Save);
    }



    public void Save()//保存函数
    {
        for (int i = 0; i < Grid.w; i++)
        {
            for (int j = 0; j < Grid.h; j++)
            {
                if (editorGrids[i, j] != null)
                {

                    listOf2DArrays[int.Parse(inputField.text)][j * Grid.w + i] = editorGrids[i, j].HP;

                }

            }
        }
        string json = JsonConvert.SerializeObject(listOf2DArrays, Formatting.Indented);

        string path = Application.streamingAssetsPath + "ByJson.json";//这是保存的路径
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(json);
            writer.Close();
            writer.Dispose();
        }
        Debug.Log("保存成功");

    }


    public void Load()//加载函数
    {
        //最好做下判断文档是否存在，这里省略了
        string path = Application.streamingAssetsPath + "ByJson.json";//这是想打开的文件路径
        StreamReader reader = new StreamReader(path);//创建读取的文件流


        string ObjectStr = reader.ReadToEnd();//将文本文件中的内容全部读取到字符串中，（json格式其实就算字符串类型）
        reader.Close();//关闭流
        //反序列化
        List<int[]> deserializedListOf2DArrays1 = JsonConvert.DeserializeObject<List<int[]>>(ObjectStr);
        for (int i = 0; i < Grid.w; i++)
        {
            for (int j = 0; j < Grid.h; j++)
            {
                if (editorGrids[i, j])
                {
                    editorGrids[i, j].HP = deserializedListOf2DArrays1[int.Parse(inputField.text)][j * Grid.w + i];

                }

            }
        }
    }
}

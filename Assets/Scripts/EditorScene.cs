using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using TMPro;

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

}

public class EditorScene : MonoBehaviour
{
    public EditorGrid[,] editorGrids = new EditorGrid[Grid.w, Grid.h];

    public GameObject guangquanPrefab;

    Save JsonSaveObject = new Save();//声明Save对象，记录当前的游戏状态

    public Button SaveBtn;


    public TMP_InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
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
        JsonSaveObject = JsonMapper.ToObject<Save>(ObjectStr);
        SaveBtn.onClick.AddListener(Save);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void Save()//保存函数
    {


        for (int i = 0; i < Grid.w; i++)
        {
            for (int j = 0; j < Grid.h; j++)
            {
                if (editorGrids[i, j] != null)
                {
                    if (inputField.text == "0")
                    {
                        JsonSaveObject.ints0[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "1")
                    {
                        JsonSaveObject.ints1[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "2")
                    {
                        JsonSaveObject.ints2[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "3")
                    {
                        JsonSaveObject.ints3[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "4")
                    {
                        JsonSaveObject.ints4[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "5")
                    {
                        JsonSaveObject.ints5[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "6")
                    {
                        JsonSaveObject.ints6[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "7")
                    {
                        JsonSaveObject.ints7[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "8")
                    {
                        JsonSaveObject.ints8[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "9")
                    {
                        JsonSaveObject.ints9[j * Grid.w + i] = editorGrids[i, j].HP;
                    }
                    else if (inputField.text == "10")
                    {
                        JsonSaveObject.ints10[j * Grid.w + i] = editorGrids[i, j].HP;
                    }

                }



            }
        }


        //这里一定要注意需要字段为public权限，不然会导致转换不成功！！！
        string ObjectStr = JsonUtility.ToJson(JsonSaveObject, true);//将Save对象序列化为Json类型的字符串
        string path = Application.streamingAssetsPath + "ByJson.json";//这是保存的路径
        // string path =Application.dataPath + "ByJson.json";//这是保存的路径
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine(ObjectStr);
            writer.Close();
            writer.Dispose();
        }
        // AssetDatabase.Refresh();
        Debug.Log("保存成功");

    }


    public void Load()//加载函数
    {
        //最好做下判断文档是否存在，这里省略了
        string path = Application.streamingAssetsPath + "ByJson.json";//这是想打开的文件路径
        StreamReader reader = new StreamReader(path);//创建读取的文件流
        string ObjectStr = reader.ReadToEnd();//将文本文件中的内容全部读取到字符串中，（json格式其实就算字符串类型）
        reader.Close();//关闭流
        Save JsonSaveObject = JsonMapper.ToObject<Save>(ObjectStr);//将json格式转化成对象
                                                                   //注意这里其实也可以不用读取流的方法，可以直接将json文件放在Resources文件夹下，因为文本在unity中时TextAssets类型，可以直接通过Resources来加载json文件


        for (int i = 0; i < Grid.w; i++)
        {
            for (int j = 0; j < Grid.h; j++)
            {
                if (editorGrids[i, j])
                {


                    if (inputField.text == "0")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints0[j * Grid.w + i];
                    }
                    else if (inputField.text == "1")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints1[j * Grid.w + i];
                    }
                    else if (inputField.text == "2")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints2[j * Grid.w + i];
                    }
                    else if (inputField.text == "3")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints3[j * Grid.w + i];
                    }
                    else if (inputField.text == "4")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints4[j * Grid.w + i];
                    }
                    else if (inputField.text == "5")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints5[j * Grid.w + i];
                    }
                    else if (inputField.text == "6")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints6[j * Grid.w + i];
                    }
                    else if (inputField.text == "7")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints7[j * Grid.w + i];
                    }
                    else if (inputField.text == "8")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints8[j * Grid.w + i];
                    }
                    else if (inputField.text == "9")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints9[j * Grid.w + i];
                    }
                    else if (inputField.text == "10")
                    {
                        editorGrids[i, j].HP = JsonSaveObject.ints10[j * Grid.w + i];
                    }

                    // if (Spawner.instance.staticGrids[i, j].HP > 0)
                    // {
                    //     Spawner.instance.staticGrids[i, j].render.SetActive(true);
                    // }

                    // Debug.Log(Spawner.instance.staticGrids[i, j].HP);
                }

            }
        }
    }
}

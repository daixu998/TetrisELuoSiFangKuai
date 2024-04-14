using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class EditorScene : MonoBehaviour
{
    public EditorGrid[,] editorGrids = new EditorGrid[Grid.w, Grid.h];

    public GameObject guangquanPrefab;

    Save JsonSaveObject = new Save();//声明Save对象，记录当前的游戏状态

    public Button SaveBtn;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < Grid.w; x++)
        {
            for (int y = 0; y < Grid.h - 10; y++)
            {
                var quan = GameObject.Instantiate(guangquanPrefab, new Vector3(x, y, 0), Quaternion.identity);

                editorGrids[x, y] = quan.transform.GetComponentInChildren<EditorGrid>();
                quan.transform.SetParent(transform);
            }
        }

        JsonSaveObject.ints = new int[(Grid.h) * (Grid.w)];

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
                if (editorGrids[i, j])
                {

                    JsonSaveObject.ints[j * Grid.w + i] = editorGrids[i, j].HP;
                }
                else
                {
                    JsonSaveObject.ints[j * Grid.w + i] = 0;
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
}

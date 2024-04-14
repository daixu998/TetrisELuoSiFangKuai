using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;
using UnityEditor;
using Unity.VisualScripting;
using TMPro;
using Newtonsoft.Json;




public class JsonSave : MonoBehaviour
{ 
    public TMP_InputField inputField;   
    //  Save JsonSaveObject = new Save();//声明Save对象，记录当前的游戏状态


     void Start() {
        // JsonSaveObject.ints0 = new int[(Grid.h)*(Grid.w)];

    }
    // public void Save()//保存函数
    // {
        
       
    //     for (int i = 0; i < Grid.w; i++) {
    //         for (int j = 0; j < Grid.h; j++) {
    //             if (Spawner.instance.staticGrids[i,j])
    //             {

    //                  JsonSaveObject.ints0[j*Grid.w + i] =Spawner.instance.staticGrids[i,j].HP;
    //             }else
    //             {
    //                   JsonSaveObject.ints0[j*Grid.w + i] =0;
    //             }
                
                
                
    //         }
    //     }
        
 
    //     //这里一定要注意需要字段为public权限，不然会导致转换不成功！！！
    //     string ObjectStr = JsonUtility.ToJson(JsonSaveObject,true);//将Save对象序列化为Json类型的字符串
    //     string path = Application.streamingAssetsPath + "ByJson.json";//这是保存的路径
    //     // string path =Application.dataPath + "ByJson.json";//这是保存的路径
    //     using (StreamWriter writer = new StreamWriter(path))
    //     {
    //         writer.WriteLine(ObjectStr);
    //         writer.Close();
    //         writer.Dispose();
    //     }
    //     // AssetDatabase.Refresh();
    //     Debug.Log("保存成功");
 
    //     // //这里需要将字符串写入到文件中，因为JsonUtility.ToJson()方法是将字符串转换成Json类型的字符串，
    //     // //而JsonUtility.ToJson()方法是将对象转换成Json类型的字符串，因此需要将字符串写入到文件中
 
    //     // //对象被转换成了字符串，这个时候还是需要写进文件中，因此
    //     // StreamWriter writer = new StreamWriter(path);//创建文件流
    //     // writer.Write(ObjectStr);//写入
    //     // writer.Close();//关闭文件流       
    // }
 
    public void Load()//加载函数
    {
        //最好做下判断文档是否存在，这里省略了
        string path =Application.streamingAssetsPath + "ByJson.json";//这是想打开的文件路径
        StreamReader reader = new StreamReader(path);//创建读取的文件流
        string ObjectStr = reader.ReadToEnd();//将文本文件中的内容全部读取到字符串中，（json格式其实就算字符串类型）
        reader.Close();//关闭流
        // Save JsonSaveObject = JsonMapper.ToObject<Save>(ObjectStr);//将json格式转化成对象
        //注意这里其实也可以不用读取流的方法，可以直接将json文件放在Resources文件夹下，因为文本在unity中时TextAssets类型，可以直接通过Resources来加载json文件
        List<int[]> deserializedListOf2DArrays1 = JsonConvert.DeserializeObject<List<int[]>>(ObjectStr);
        
        for (int i = 0; i < Grid.w; i++) {
            for (int j = 0; j < Grid.h; j++) {
                if ( Spawner.instance.staticGrids[i,j])
                {
                   
                     Spawner.instance.staticGrids[i, j].HP = deserializedListOf2DArrays1[int.Parse(inputField.text)][j * Grid.w + i];
                    
                    if (Spawner.instance.staticGrids[i,j].HP>0)
                    {
                        Spawner.instance.staticGrids[i,j].render.SetActive(true);
                    }
                     
                     Debug.Log(Spawner.instance.staticGrids[i,j].HP);
                }
                
            }
        }

       

 
    }

}
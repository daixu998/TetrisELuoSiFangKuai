using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorGrid : MonoBehaviour
{
    
    public int HP = 1;
    public Text text;
    public GameObject render;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        // text = GetComponentInChildren<Text>();
        text.text = HP.ToString();
        button.onClick.AddListener(()=>HP++);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HP.ToString() != text.text)
        {
            text.text = HP.ToString();
        }

    }


}

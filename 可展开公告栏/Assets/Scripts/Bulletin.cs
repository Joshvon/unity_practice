using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Bulletin : MonoBehaviour {
 
    private Button yourButton;
    public Text text;
    private int frame = 20;
 
    // Use this for initialization
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();    //获取脚本搭载的按钮
        btn.onClick.AddListener(TaskOnClick);                   //在按钮的点击操作中添加事件
    }
 
    IEnumerator rotateIn()
    {
        float rx = 0;
        float xy = 120;
        for (int i = 0; i < frame; i++)
        {
            rx -= 90f / frame;
            xy -= 120f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);                                 //旋转相应的Text
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);       //改变Text大小
            if (i == frame - 1)
            {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
 
    IEnumerator rotateOut()
    {
        float rx = -90;
        float xy = 0;
        for (int i = 0; i < frame; i++)
        {
            rx += 90f / frame;
            xy += 120f / frame;
            text.transform.rotation = Quaternion.Euler(rx, 0, 0);                                   //旋转相应的Text
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, xy);         //改变Text大小
            if (i == 0) 
            {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }
 
 
    void TaskOnClick()
    {
        if (text.gameObject.activeSelf)
        {
            StartCoroutine(rotateIn()); //启动协程收起公告
        }
        else
        {
            StartCoroutine(rotateOut()); //启动携程展开公告
        }
        
    }
}
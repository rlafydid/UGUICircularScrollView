using System.Collections;
using System.Collections.Generic;
using CircularScrollView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestOtherScrollTypeDemo : MonoBehaviour {

    public ExpandCircularScrollView expandScroll;
    public ExpandTipsCircularScrollView expandTipsScroll;
    public GameObject expandTips;

    public FlipPageCircularScrollView flipPageScroll;

    public Button btn;

    // Use this for initialization
    void Start()
    {
        btn.onClick.AddListener(OnClickMainDemoScroll);
        StartScrollView();
    }

    void OnClickMainDemoScroll()
    {
        SceneManager.LoadScene("MainDemo");
    }

    public void StartScrollView()
    {
        expandScroll.Init(ExpandCallBack);
        expandScroll.ShowList("3|2|5|8");

        expandTipsScroll.Init(ExpandTipsCallBack, OnClickExpandTipsCallBack);
        expandTipsScroll.ShowList(30);

        flipPageScroll.Init(FlipPageCallBack);
        flipPageScroll.ShowList(10);

    }

    private void ExpandCallBack(GameObject cell, GameObject childCell, int index, int childIndex)
    {
        cell.transform.Find("Text1").GetComponent<Text>().text = "Click Me : " + index.ToString();
        if (childCell != null)
        {
            childCell.transform.Find("Text1").GetComponent<Text>().text = childIndex.ToString();
        }
    }

    private void ExpandTipsCallBack(GameObject cell, int index)
    {
        cell.transform.Find("Text1").GetComponent<Text>().text = "Click Me : " + index.ToString();
    }

    private void OnClickExpandTipsCallBack(GameObject cell, int index)
    {
        expandTips.transform.Find("Text").GetComponent<Text>().text = string.Format("我是{0}号", index);
    }

    private void FlipPageCallBack(GameObject cell, int index)
    {
        cell.transform.Find("Text1").GetComponent<Text>().text = "Drag Me : " + index.ToString();
    }
}

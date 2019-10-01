//*****************************-》 滑动翻页 循环列表 《-****************************
//author: Kim
//初始化:
//      Init(CallBack)
//      Init(CallBack, SlidePageCallBack)   SlidePageCallBack = 返回当前页的回调
//刷新列表:
//      ShowList(int = 数量)
//回调:
// 1: Func(cell, index)  //列表刷新回调
// 2: SlidePageFunc( int = pageIndex ) 翻页回调 param = 当前页

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CircularScrollView
{
    public class FlipPageCircularScrollView : UICircularScrollView
    {
        public int m_OnePageCount = 1;      //每页显示的项目
        public float m_SlideSpeed = 5;       //滑动速度

        public bool m_IsMaxRange = false;

        // * -> 是否开启 导航图标模式
        public bool m_IsOpenNavIcon = false;

        //-> 开启 导航图标模式时 使用以下变量
        public float m_IconSize = 20;
        public float m_IconSpacing = 5.0f;
        public Sprite m_SelectIcon = null;
        public Sprite m_NormalIcon = null;
        public Transform m_ObjNavigation;
        // <-

        private int m_AllPageCount;           //总页数
        private int m_NowIndex = 0;          //当前位置索引
        private int m_LastIndex = -1;        //上一次的索引
        private bool m_IsDrag = false;          //是否拖拽中
        private float m_TargetPos = 0;              //滑动的目标位置  
        private List<float> m_ListPageValue = new List<float> { 0 };  //总页数索引比列 0-1  

        private List<Image> m_NavigationList = new List<Image>();

        protected new Action<int> m_FuncOnClickCallBack;

        public override void ShowList(int num)
        {
            base.ShowList(num);
            ListPageValueInit();
        }

        //翻页到某一页
        public override void SetToPageIndex(int index)
        {
            m_IsDrag = false;
            m_NowIndex = index - 1;
            m_TargetPos = m_ListPageValue[m_NowIndex];

            for (int i = 0; i < m_NavigationList.Count; i++)
            {
                if (i == m_NowIndex)
                {
                    m_NavigationList[m_NowIndex].sprite = m_SelectIcon;
                }
                else
                {
                    m_NavigationList[i].sprite = m_NormalIcon;
                }
            }
            if (m_FuncOnClickCallBack != null)
            {
                Func(m_FuncOnClickCallBack, m_NowIndex);
            }
        }

        //每页比例  
        void ListPageValueInit()
        {
            m_NavigationList.Clear();
            m_AllPageCount = m_MaxCount - m_OnePageCount;
            if (m_MaxCount != 0)
            {
                for (float i = 1; i <= m_AllPageCount; i++)
                {
                    m_ListPageValue.Add((i / m_AllPageCount));
                }
            }
            if (m_IsOpenNavIcon && m_MaxCount > 1)
            {
                if (m_ObjNavigation == null)
                {
                    m_ObjNavigation = transform.Find("m_ObjNavigation");
                }
                float[] posArray = new float[m_MaxCount];
                if (m_MaxCount == 1)
                {
                    posArray[0] = 0;
                }
                else
                {
                    float startX = -m_MaxCount / 2 * m_IconSpacing;
                    for (int i = 0; i < m_MaxCount; i++)
                    {
                        posArray[i] = startX + m_IconSpacing * i;
                    }
                }

                for (int i = 0; i < m_MaxCount; i++)
                {
                    GameObject icon = null;
                    if (m_ObjNavigation.Find(string.Format("icon{0}", i)) != null)
                    {
                        icon = m_ObjNavigation.Find(string.Format("icon{0}", i)).gameObject;
                    }
                    else
                    {
                        icon = new GameObject(string.Format("icon{0}", i));
                    }
                    icon.transform.parent = m_ObjNavigation;
                    Image img = null;
                    if (icon.GetComponent<Image>() == null)
                    {
                        img = icon.AddComponent<Image>();
                    }
                    else
                    {
                        img = icon.GetComponent<Image>();
                    }
                    if (i == 0)
                    {
                        img.sprite = m_SelectIcon;
                    }
                    else
                    {
                        img.sprite = m_NormalIcon;
                    }
                    img.rectTransform.sizeDelta = new Vector2(m_IconSize, m_IconSize);
                    icon.transform.localScale = Vector3.one;
                    icon.transform.localPosition = new Vector3(posArray[i], 0, 0);
                    m_NavigationList.Add(img);
                }
            }
            if (m_FuncOnClickCallBack != null)
            {
                Func(m_FuncOnClickCallBack, m_NowIndex);
            }
        }

        void Update()
        {
                if (!m_IsDrag)
                {
                    if (m_ScrollRect == null) return;

                    if(m_Direction == e_Direction.Vertical)
                    {
                        m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp(m_ScrollRect.verticalNormalizedPosition, m_TargetPos, Time.deltaTime * m_SlideSpeed);
                    }
                    else
                    {
                        m_ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(m_ScrollRect.horizontalNormalizedPosition, m_TargetPos, Time.deltaTime * m_SlideSpeed);
                    }
                }
        }
        /// 拖动开始   
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            m_IsDrag = true;
        }

        /// 拖拽结束   
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            m_IsDrag = false;
            if (m_ListPageValue.Count == 1)
            {
                return;
            }
            float tempPos = 0;
            if(m_Direction == e_Direction.Vertical)
            {
                tempPos = m_ScrollRect.verticalNormalizedPosition;
            }
            else
            {
                tempPos = m_ScrollRect.horizontalNormalizedPosition;
            }

            if (m_IsMaxRange)
            {
                //获取拖动的值  
                var index = 0;
                float offset = Mathf.Abs(m_ListPageValue[index] - tempPos);    //拖动的绝对值  
                for (int i = 1; i < m_ListPageValue.Count; i++)
                {
                    float temp = Mathf.Abs(tempPos - m_ListPageValue[i]);
                    if (temp < offset)
                    {
                        index = i;
                        offset = temp;
                    }
                }
                m_TargetPos = m_ListPageValue[index];
                m_NowIndex = index;
                if (m_NowIndex != m_LastIndex && m_FuncOnClickCallBack != null)
                {
                    Func(m_FuncOnClickCallBack, m_NowIndex);
                }
                m_LastIndex = m_NowIndex;
            }
            else
            {
                float currPos = m_ListPageValue[m_NowIndex];
                if (tempPos > currPos)
                {
                    m_NowIndex++;
                }
                else if (tempPos < currPos)
                {
                    m_NowIndex--;
                }
                if (m_NowIndex < 0)
                {
                    m_NowIndex = 0;
                }
                if (m_NowIndex > m_ListPageValue.Count - 1)
                {
                    m_NowIndex = m_ListPageValue.Count - 1;
                }
                m_TargetPos = m_ListPageValue[m_NowIndex];

                if (m_NowIndex != m_LastIndex && m_FuncOnClickCallBack != null)
                {
                    Func(m_FuncOnClickCallBack, m_NowIndex);
                }
                m_LastIndex = m_NowIndex;
            }

            if (m_IsOpenNavIcon)
            {
                int length = m_NavigationList.Count;
                for (int i = 0; i < length; i++)
                {
                    Image img = m_NavigationList[i];
                    if (i == m_NowIndex)
                    {
                        img.sprite = m_SelectIcon;
                    }
                    else
                    {
                        img.sprite = m_NormalIcon;
                    }
                }
            }
        }

        //翻页时的回调
        private void Func(Action<int> Func, int index)
        {
            if (Func == null)
                return;

            Func(index + 1);
        }
    }
}


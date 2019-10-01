using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CircularScrollView
{

    [CustomEditor(typeof(CircularScrollView.ExpandTipsCircularScrollView))]
    public class ExpandTipsCircularScrollViewEditor : Editor
    {

        ExpandTipsCircularScrollView list;
        public override void OnInspectorGUI()
        {
            list = (CircularScrollView.ExpandTipsCircularScrollView)target;
            list.m_Direction = (e_Direction)EditorGUILayout.EnumPopup("Direction: ", list.m_Direction);

            list.m_Row = EditorGUILayout.IntField("Row Or Column: ", list.m_Row);
            list.m_Spacing = EditorGUILayout.FloatField("Spacing: ", list.m_Spacing);
            list.m_CellGameObject = (GameObject)EditorGUILayout.ObjectField("Cell: ", list.m_CellGameObject, typeof(GameObject), true);
            list.m_ExpandTips = (GameObject)EditorGUILayout.ObjectField("Tips: ", list.m_ExpandTips, typeof(GameObject), true);
            list.m_Arrow = (GameObject)EditorGUILayout.ObjectField("Arrow: ", list.m_Arrow, typeof(GameObject), true);
            //list.m_BackgroundMargin = EditorGUILayout.FloatField("BackgroundScale：", list.m_BackgroundMargin);

            list.m_IsShowArrow = EditorGUILayout.ToggleLeft(" IsShowArrow", list.m_IsShowArrow);
            if (list.m_IsShowArrow)
            {
                list.m_PointingFirstArrow = (GameObject)EditorGUILayout.ObjectField("Up or Left Arrow: ", list.m_PointingFirstArrow, typeof(GameObject), true);
                list.m_PointingEndArrow = (GameObject)EditorGUILayout.ObjectField("Down or Right Arrow: ", list.m_PointingEndArrow, typeof(GameObject), true);
            }
        }
    }
}

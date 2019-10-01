using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CircularScrollView
{

    [CustomEditor(typeof(CircularScrollView.UICircularScrollView))]
    public class UICircularScrollViewEditor : Editor
    {

        UICircularScrollView list;
        public override void OnInspectorGUI()
        {
            list = (CircularScrollView.UICircularScrollView)target;

            list.m_Direction = (e_Direction)EditorGUILayout.EnumPopup("Direction: ", list.m_Direction);

            list.m_Row = EditorGUILayout.IntField("Row Or Column: ", list.m_Row);
            list.m_Spacing = EditorGUILayout.FloatField("Spacing: ", list.m_Spacing);
            list.m_CellGameObject = (GameObject)EditorGUILayout.ObjectField("Cell: ", list.m_CellGameObject, typeof(GameObject), true);
            list.m_IsShowArrow = EditorGUILayout.ToggleLeft(" IsShowArrow", list.m_IsShowArrow);
            if(list.m_IsShowArrow)
            {
                list.m_PointingFirstArrow = (GameObject)EditorGUILayout.ObjectField("Up or Left Arrow: ", list.m_PointingFirstArrow, typeof(GameObject), true);
                list.m_PointingEndArrow = (GameObject)EditorGUILayout.ObjectField("Down or Right Arrow: ", list.m_PointingEndArrow, typeof(GameObject), true);
            }

        }
    }
}

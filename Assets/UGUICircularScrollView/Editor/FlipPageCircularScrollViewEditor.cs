using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CircularScrollView
{

    [CustomEditor(typeof(CircularScrollView.FlipPageCircularScrollView))]
    public class FlipPageCircularScrollViewEditor : Editor
    {
        FlipPageCircularScrollView list;
        public override void OnInspectorGUI()
        {
            list = (CircularScrollView.FlipPageCircularScrollView)target;
            list.m_Direction = (e_Direction)EditorGUILayout.EnumPopup("Direction: ", list.m_Direction);

            list.m_Row = EditorGUILayout.IntField("Row Or Column: ", list.m_Row);
            list.m_Spacing = EditorGUILayout.FloatField("Spacing: ", list.m_Spacing);
            list.m_CellGameObject = (GameObject)EditorGUILayout.ObjectField("Cell: ", list.m_CellGameObject, typeof(GameObject), true);

            list.m_IsMaxRange = EditorGUILayout.ToggleLeft(" isBigRange", list.m_IsMaxRange);

            list.m_IsOpenNavIcon = EditorGUILayout.ToggleLeft(" isOpenNavIcon", list.m_IsOpenNavIcon);

            if (list.m_IsOpenNavIcon)
            {
                list.m_ObjNavigation = (Transform)EditorGUILayout.ObjectField("Sprite: ", list.m_ObjNavigation, typeof(Transform), true);
                list.m_SelectIcon = (Sprite)EditorGUILayout.ObjectField("SelectSprite: ", list.m_SelectIcon, typeof(Sprite), true);
                list.m_NormalIcon = (Sprite)EditorGUILayout.ObjectField("DefaultSprite: ", list.m_NormalIcon, typeof(Sprite), true);
                list.m_IconSpacing = EditorGUILayout.FloatField("SpriteSpacing: ", list.m_IconSpacing);
                list.m_IconSize = EditorGUILayout.FloatField("SpriteSize: ", list.m_IconSize);
            }

            list.m_IsShowArrow = EditorGUILayout.ToggleLeft(" IsShowArrow", list.m_IsShowArrow);
            if (list.m_IsShowArrow)
            {
                list.m_PointingFirstArrow = (GameObject)EditorGUILayout.ObjectField("Up or Left Arrow: ", list.m_PointingFirstArrow, typeof(GameObject), true);
                list.m_PointingEndArrow = (GameObject)EditorGUILayout.ObjectField("Down or Right Arrow: ", list.m_PointingEndArrow, typeof(GameObject), true);
            }
        }
    }
}

// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(MapTrigger))]
// public class MapTriggerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         MapTrigger mapTrigger = (MapTrigger)target;

//         // 绘制下拉框，并赋值给 direction
//         mapTrigger.DirectionValue = (MapTrigger.Direction)EditorGUILayout.EnumPopup("方向选择", mapTrigger.DirectionValue);

//         // 根据方向选择更新提示名称
//         if (mapTrigger.DirectionValue == MapTrigger.Direction.UpDown)
//         {
//             EditorGUILayout.LabelField("提示名称：上");
//             for (int i = 0; i < mapTrigger.L_U_Tip.Length; i++)
//             {
//                 mapTrigger.L_U_Tip[i] = (GameObject)EditorGUILayout.ObjectField("上提示 " + (i + 1), mapTrigger.L_U_Tip[i], typeof(GameObject), true);
//             }
//             for (int i = 0; i < mapTrigger.R_D_Tip.Length; i++)
//             {
//                 mapTrigger.R_D_Tip[i] = (GameObject)EditorGUILayout.ObjectField("下提示 " + (i + 1), mapTrigger.R_D_Tip[i], typeof(GameObject), true);
//             }
//         }
//         else if (mapTrigger.DirectionValue == MapTrigger.Direction.LeftRight)
//         {
//             EditorGUILayout.LabelField("提示名称：左");
//             for (int i = 0; i < mapTrigger.L_U_Tip.Length; i++)
//             {
//                 mapTrigger.L_U_Tip[i] = (GameObject)EditorGUILayout.ObjectField("左提示 " + (i + 1), mapTrigger.L_U_Tip[i], typeof(GameObject), true);
//             }
//             for (int i = 0; i < mapTrigger.R_D_Tip.Length; i++)
//             {
//                 mapTrigger.R_D_Tip[i] = (GameObject)EditorGUILayout.ObjectField("右提示 " + (i + 1), mapTrigger.R_D_Tip[i], typeof(GameObject), true);
//             }
//         }

//         // 继续绘制默认的Inspector内容
//         DrawDefaultInspector();
//     }
// }

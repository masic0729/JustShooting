using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(TestExample))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TestExample example = (TestExample)target;
        
        // BodyType 필드 먼저 그리기
        example.bodyType = (TestExample.BodyType)EditorGUILayout.EnumPopup("Body Type", example.bodyType);

        // 선택된 BodyType에 따라 조건부로 필드 노출
        if (example.bodyType == TestExample.BodyType.Dynamic)
        {
            example.mass = EditorGUILayout.FloatField("Mass", example.mass);
            example.drag = EditorGUILayout.FloatField("Drag", example.drag);
            example.useGravity = EditorGUILayout.Toggle("Use Gravity", example.useGravity);
        }
        else if (example.bodyType == TestExample.BodyType.Kinematic)
        {
            example.useGravity = EditorGUILayout.Toggle("Use Gravity", example.useGravity);
        }

        // 값 변경 사항 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(example);

        }
    }
}

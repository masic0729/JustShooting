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
        
        // BodyType �ʵ� ���� �׸���
        example.bodyType = (TestExample.BodyType)EditorGUILayout.EnumPopup("Body Type", example.bodyType);

        // ���õ� BodyType�� ���� ���Ǻη� �ʵ� ����
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

        // �� ���� ���� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(example);

        }
    }
}
